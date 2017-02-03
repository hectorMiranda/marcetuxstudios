// ESP32 + DHT22 → MQTT → Home Assistant
// Publishes retained temperature and humidity every 30 s.
#include <WiFi.h>
#include <PubSubClient.h>
#include <DHT.h>

const char* SSID      = "HOME_SSID";
const char* PASS      = "HOME_PASS";
const char* MQTT_HOST = "192.168.1.10";   // Pi running Mosquitto
const char* TOPIC_T   = "home/sensors/office/temp";
const char* TOPIC_H   = "home/sensors/office/humidity";

DHT dht(4, DHT22);       // data pin 4
WiFiClient   wifi;
PubSubClient mqtt(wifi);

void setup() {
  Serial.begin(115200);
  dht.begin();
  WiFi.begin(SSID, PASS);
  while (WiFi.status() != WL_CONNECTED) delay(500);
  mqtt.setServer(MQTT_HOST, 1883);
}

void loop() {
  if (!mqtt.connected()) {
    mqtt.connect("office-sensor");
  }
  float t = dht.readTemperature();
  float h = dht.readHumidity();
  if (!isnan(t) && !isnan(h)) {
    char buf[8];
    snprintf(buf, sizeof(buf), "%.1f", t);
    mqtt.publish(TOPIC_T, buf, /*retained=*/true);
    snprintf(buf, sizeof(buf), "%.1f", h);
    mqtt.publish(TOPIC_H, buf, /*retained=*/true);
  }
  mqtt.loop();
  delay(30000);
}
