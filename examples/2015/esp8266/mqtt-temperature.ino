// ESP8266 Arduino core + PubSubClient library.
// Publishes a DS18B20 temperature to an MQTT broker every 30 seconds.
#include <ESP8266WiFi.h>
#include <PubSubClient.h>
#include <OneWire.h>
#include <DallasTemperature.h>

const char* ssid     = "home-net";
const char* password = "REDACTED";
const char* broker   = "192.168.1.5";   // Pi 2 MQTT broker
const char* topic    = "home/bench/temp";

OneWire           oneWire(2);           // GPIO2
DallasTemperature sensors(&oneWire);
WiFiClient        net;
PubSubClient      mqtt(net);

void setup() {
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) delay(500);
  mqtt.setServer(broker, 1883);
  sensors.begin();
}

void loop() {
  if (!mqtt.connected()) {
    mqtt.connect("bench-node-01");
  }
  sensors.requestTemperatures();
  float t = sensors.getTempCByIndex(0);
  char buf[8];
  dtostrf(t, 5, 2, buf);
  mqtt.publish(topic, buf);
  delay(30000);
}
