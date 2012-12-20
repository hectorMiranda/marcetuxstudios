// Addressable RGB string (WS2811) with FastLED. Data on D6.
// IMPORTANT: power the strip from a separate 5V supply; share ground only.
#include <FastLED.h>

#define NUM_LEDS 60
#define DATA_PIN 6
CRGB leds[NUM_LEDS];

void setup() {
  FastLED.addLeds<WS2811, DATA_PIN, GRB>(leds, NUM_LEDS);
  FastLED.setBrightness(96);   // also a power-budget safety valve
}

void rainbow(uint8_t hueShift) {
  for (int i = 0; i < NUM_LEDS; i++) leds[i] = CHSV(i * 4 + hueShift, 200, 255);
}

void loop() {
  static uint8_t hue = 0;
  rainbow(hue++);
  // random sparkle on top of the rainbow
  if (random8() < 40) leds[random16(NUM_LEDS)] = CRGB::White;
  FastLED.show();
  delay(40);
}
