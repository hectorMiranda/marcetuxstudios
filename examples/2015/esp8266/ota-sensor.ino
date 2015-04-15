// ESP8266 with OTA support and non-blocking loop.
// Publishes temperature every 30s without blocking ArduinoOTA.handle().
#include <ESP8266WiFi.h>
#include <ArduinoOTA.h>
#include <PubSubClient.h>
#include <DallasTemperature.h>
#include <OneWire.h>

const char* ssid     = "home-net";
const char* password = "REDACTED";
const char* broker   = "192.168.1.5";
const char* topic    = "home/bathroom/temp";
const unsigned long PUBLISH_INTERVAL = 30000;

OneWire           oneWire(2);
DallasTemperature sensors(&oneWire);
WiFiClient        net;
PubSubClient      mqtt(net);
unsigned long     lastPublish = 0;

void setup() {
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) delay(200);
  ArduinoOTA.setHostname("bathroom-node");
  ArduinoOTA.begin();
  mqtt.setServer(broker, 1883);
  sensors.begin();
}

void loop() {
  ArduinoOTA.handle();   // must run frequently to accept OTA transfers
  if (!mqtt.connected()) mqtt.connect("bathroom-node");

  unsigned long now = millis();
  if (now - lastPublish >= PUBLISH_INTERVAL) {
    lastPublish = now;
    sensors.requestTemperatures();
    float t = sensors.getTempCByIndex(0);
    char buf[8];
    dtostrf(t, 5, 2, buf);
    mqtt.publish(topic, buf);
  }
}
