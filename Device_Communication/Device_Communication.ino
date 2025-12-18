//Slave側
//M5StampのMacアドレス:34:b7:da:f2:2a:8c

#include"ESPNowEz.h"

CESPNowEZ espnow(1);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:
  Serial.println(espnow.GetMacAddrChar());
  delay(1000);
}
