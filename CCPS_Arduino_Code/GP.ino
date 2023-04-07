void diodeChar() {
  if (Serial.available()) {
    delay(2);
    s = Serial.readString();
    s1 = s.charAt(0);
    maxVol = s.toFloat();
    delay(2);

    if (s1 == 'S') {
      resetFunc();
    }
    else {
      x = maxVol;

      if (x <= 12 && x > 11)
      {
        x = x - 0.6;
        y = x * 15.3846 - 13.46;
      }

      else if (x <= 11 && x > 10)
      {
        x = x - 0.6;
        y = x * 15.3846 - 13.46;
      }

      else if (x <= 10 && x > 9) {
        x = x - 0.5;
        y = x * 15.3846 - 15.46;
      }

      else if (x <= 9 && x > 8)
      {
        x = x - 0.4;
        y = x * 15.3846 - 17.16;
      }

      else if (x <= 8 && x > 7)
      {
        x = x - 0.5;
        y = x * 15.3846 - 15.46;
      }

      else if (x <= 7 && x > 4.7)
      {
        x = x - 0.4;
        y = x * 15.3846 - 17.16;
      }

      else if (x <= 4.7 && x > 2.8)
      {
        x = x - 0.3;
        y = x * 15.3846 - 17.16;
      }

      else if (x <= 2.8 && x >= 2) {
        x = x - 0.2;
        y = x * 15.3846 - 17.16;
      }

      for (int j = 10; j < y; j++) {
        analogWrite(vltgCtrlPin, j);

        float voltage1 = 0;
        float voltage2 = 0;
        float voltageOut = 0;
        float current1 = 0;
        float current2 = 0;
        float currentOut = 0;
        float power1 = 0;
        float powerOut = 0;

        for (int i = 1; i <= 10; i++) {
          voltage1 = ina219.getBusVoltage_V();
          voltage2 = voltage2 + voltage1;
          current1 = ina219.getCurrent_mA();
          current2 = current2 + current1;
          delay(10);
          stopFun();
        }

        voltageOut = voltage2 / 10;
        currentOut = current2 / 10 / 1000;

        power1 = ina219.getPower_mW();
        powerOut = power1 / 1000;

        Serial.println(voltageOut, 2);
        Serial.println(currentOut, 3);
        Serial.println(powerOut, 3);
        Serial.println("");
        delay(100);
      }
    }
  }
}
