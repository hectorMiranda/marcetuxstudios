// Reads fixed-width status packets from a solar inverter via UART2,
// parses power and daily kWh, and publishes to MQTT.
#include <WiFi.h>
#include <PubSubClient.h>

#define UART_BAUD 9600
#define UART_RX   16
#define UART_TX   17

const char* SSID     = "homenet";
const char* WIFI_PWD = "...";
const char* MQTT_HOST = "192.168.1.50";
const char* TOPIC_POWER = "solar/power_w";
const char* TOPIC_DAILY = "solar/daily_kwh";

WiFiClient   wifi;
PubSubClient mqtt(wifi);

void setup() {
  Serial2.begin(UART_BAUD, SERIAL_8N1, UART_RX, UART_TX);
  WiFi.begin(SSID, WIFI_PWD);
  while (WiFi.status() != WL_CONNECTED) delay(500);
  mqtt.setServer(MQTT_HOST, 1883);
}

void loop() {
  if (!mqtt.connected()) mqtt.connect("solar-reader");
  if (Serial2.available() >= 64) {
    char buf[65];
    Serial2.readBytes(buf, 64);
    buf[64] = '\0';
    // Packet positions are inverter-specific; parse the known offsets.
    float watts   = atof(buf + 10);
    float daily   = atof(buf + 25);
    char out[16];
    snprintf(out, sizeof(out), "%.1f", watts);  mqtt.publish(TOPIC_POWER, out);
    snprintf(out, sizeof(out), "%.2f", daily);  mqtt.publish(TOPIC_DAILY, out);
  }
  mqtt.loop();
  delay(5000);
}
