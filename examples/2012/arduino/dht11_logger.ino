// DHT11 → serial. The Arduino senses; something smarter remembers.
// Emits "temp_c,humidity" once a second over USB serial at 9600 baud.
// Wiring: DHT11 data pin → D2, with a 10k pull-up to 5V.

#include <dht.h>   // Rob Tillaart's DHT library

dht DHT;
const int DHT_PIN = 2;

void setup() {
  Serial.begin(9600);
}

void loop() {
  int chk = DHT.read11(DHT_PIN);
  if (chk == DHTLIB_OK) {
    Serial.print(DHT.temperature, 1);
    Serial.print(",");
    Serial.println(DHT.humidity, 1);
  } else {
    Serial.println("ERR");
  }
  delay(1000);
}
