// ESP32 + RA-02 SX1278 sender. Adjust SPREADING_FACTOR per range/battery trade-off.
// Wiring: NSS->5, RST->14, DIO0->2, MOSI->23, MISO->19, SCK->18
#include <SPI.h>
#include <LoRa.h>

#define SS_PIN    5
#define RST_PIN   14
#define DIO0_PIN  2
#define FREQ      433E6
#define SPREADING_FACTOR 10   // SF7–SF12; higher = more range, less throughput

void setup() {
  Serial.begin(115200);
  LoRa.setPins(SS_PIN, RST_PIN, DIO0_PIN);
  if (!LoRa.begin(FREQ)) {
    Serial.println("LoRa init failed");
    while (true);
  }
  LoRa.setSpreadingFactor(SPREADING_FACTOR);
  LoRa.setTxPower(17);   // dBm — check local regulations
  Serial.println("LoRa ready");
}

void loop() {
  float temperature = 22.4f;  // replace with actual sensor read
  LoRa.beginPacket();
  LoRa.print(temperature);
  LoRa.endPacket();
  Serial.print("Sent: "); Serial.println(temperature);
  delay(10000);  // 10-second interval; adjust for duty-cycle rules
}
