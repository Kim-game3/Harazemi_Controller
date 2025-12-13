//XIAOのMacアドレス:10:51:db:1a:c0:fc

#include"ESPNowEz.h"

CESPNowEZ espnow(0);

uint8_t DeviceMacAddr[] = {0x34, 0xb7, 0xda, 0xf2, 0x2a, 0x8c };

ESPNOW_Con2Devdata ControllerData;

int outputFlag;

void onDataReceived(const esp_now_recv_info* info, const uint8_t* data, int data_len)
{
  //受信時の処理を書く
  memcpy(&deviceData, data, data_len);
  outputFlag = 1;
}

void setup()
{
  // put your setup code here, to run once:
  espnow.Initialize(OnDataReceived);

  espnow.SetDeviceMacAddr(DeviceMacAddr);

  outputFlag = 0;

  Serial.begin(115200);
}

void loop()
{
  // put your main code here, to run repeatedly:
    if(Serial.available() > 0)
  {
    inputChar = Serial.read();
    controllerData.cmd = inputChar;
    espnow.Send(1, &controllerData, sizeof(controllerData)); // id:1に送る
  }

  if(outputFlag)
  {
    outputFlag = 0;
    Serial.printf("SE\n", );
  }
}
