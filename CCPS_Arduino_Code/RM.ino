void resistorChar() {
  float voltage1 = 0;
  float voltage2 = 0;
  float voltageOut = 0;
  float current1 = 0;
  float current2 = 0;
  float currentOut = 0;
  float resistor1 = 0;
  float resistor2 = 0;
  float resistorOut = 0;
  float power1 = 0;
  float power2 = 0;
  int count = 0;


  if (Serial.available()) {
    delay(2);
    s = Serial.readString();
    s1 = s.charAt(0);
    resistorPower = s.toFloat();
    delay(2);

    if (s1 == 'S') {
      resetFunc();
    }
  }
  else {
    if (resistorPower == 0.25 || resistorPower == 0.50 || resistorPower == 1.00) {
      for (int j = 10; j <= 255; j++) {
        analogWrite(vltgCtrlPin, j);
        count = count + 1;
        for (int i = 1; i <= 10; i++) {
          voltage1 = ina219.getBusVoltage_V();
          current1 = ina219.getCurrent_mA();
          voltage2 = voltage2 + voltage1;
          current2 = current2 + current1;
          delay(10);
          stopFun();
        }
        voltageOut = voltage2 / 10;
        currentOut = current2 / 10;
        resistor1 = (voltageOut * 1000) / currentOut;
        resistor2 = resistor2 + resistor1;
        resPower = (voltageOut * currentOut) / 1000;

        stopFun();

        power1 = ina219.getPower_mW();
        power2 = power1 / 1000;
        delay(100);
        Serial.println(voltageOut);
        Serial.println(currentOut);
        Serial.println(power2);
        Serial.println(resistorOut);

        if (resPower >= resistorPower) {
          j = 256;
        }
      }
      resistorOut = resistor2 / count;
      Serial.println(voltageOut,2);
      Serial.println(currentOut,3);
      Serial.println(power2,3);
      Serial.println(resistorOut,2);
      delay(2000);
      //resetFunc();
    }
    resistorPower = 0;
    analogWrite(vltgCtrlPin, 0);
  }
}
