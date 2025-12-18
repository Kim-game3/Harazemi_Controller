//XIAOのMacアドレス:10:51:db:1a:c0:fc

#include"ESPNowEz.h"

CESPNowEZ espnow(0);

void setup()
{
  // put your setup code here, to run once:
  Serial.begin(115200);
}

void loop()
{
  // put your main code here, to run repeatedly:
  Serial.println(espnow.GetMacAddrChar());
  delay(1000);
}
