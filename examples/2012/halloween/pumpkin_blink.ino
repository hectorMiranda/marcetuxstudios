// Halloween pumpkin: a flickering-candle LED that looks like flame, plus a
// servo that twitches when the photoresistor says someone's casting a shadow.
// Arduino Uno. LED on D9 (PWM), photoresistor on A0, servo on D6.

#include <Servo.h>

const int LED = 9;
const int LDR = A0;
const int SERVO_PIN = 6;
Servo jaw;
int ambient = 0;

void setup() {
  pinMode(LED, OUTPUT);
  jaw.attach(SERVO_PIN);
  jaw.write(0);
  // Sample the resting light level so we can detect a shadow relative to it.
  long acc = 0;
  for (int i = 0; i < 20; i++) { acc += analogRead(LDR); delay(10); }
  ambient = acc / 20;
}

void flicker() {
  // Random brightness around a warm baseline = convincing candle flame.
  int level = random(120, 255);
  analogWrite(LED, level);
  delay(random(40, 120));
}

void loop() {
  flicker();
  // A shadow (hand over the pumpkin) drops the reading well below ambient.
  if (analogRead(LDR) < ambient - 120) {
    jaw.write(40);   // open
    delay(200);
    jaw.write(0);    // snap shut
    delay(500);
  }
}
