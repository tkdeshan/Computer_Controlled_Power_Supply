#include <Wire.h>
#include <Adafruit_INA219.h>

Adafruit_INA219 ina219;

#define vltgCtrlPin 10
#define noModePin 5
#define swithOnCCPin 3

char c;
String SerialData;
int option = 0;
float setVol = 0;
float mVol = 0;
float x = 0;
int y = 0;
int pwmVal = 0;
String s;
char s1;
float resistorPower = 0;
float resPower = 0;
float maxPower = 0;
float maxVol = 0;

void(* resetFunc) (void) = 0;

void setup() {
  Serial.begin(9600);

  pinMode(vltgCtrlPin, OUTPUT);
  pinMode(noModePin, OUTPUT);
  pinMode(swithOnCCPin, OUTPUT);

  digitalWrite(vltgCtrlPin, LOW);
  digitalWrite(swithOnCCPin, LOW);


  if (! ina219.begin()) {
    while (1) {
      delay(10);
    }
  }

  while (Serial.available() == 0) {
    digitalWrite(noModePin, HIGH);
    delay(100);
    digitalWrite(noModePin, LOW);
    delay(100);
  }

  while (Serial.available() > 0) {
    c = Serial.read();
    SerialData += c;
    delay(50);
  }

  if (c == '#') {
    if (SerialData == "VC#") {
      option = 1;
    }
    else if (SerialData == "CC#") {
      option = 2;
    }
    else if (SerialData == "RC#") {
      option = 3;
    }
    else if (SerialData == "DC#") {
      option = 4;
    }
  }
}

void loop() {
  if (option == 1) {
    voltageControl();
  }
  else if (option == 2) {
    currentControl();
  }
  else if (option == 3) {
    resistorChar();
  }
  else if (option == 4) {
    diodeChar();
  }
}


void stopFun() {
  if (Serial.available()) {
    delay(2);
    s = Serial.readString();
    s1 = s.charAt(0);
    analogWrite(vltgCtrlPin, 0);
    
    if (s1 == 'S') {
      resetFunc();
    }
  }
}
