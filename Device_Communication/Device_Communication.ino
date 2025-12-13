//Slave側
//M5StampのMacアドレス:34:b7:da:f2:2a:8c

#include"ESPNowEz.h"

CESPNowEZ espnow(1);

ESPNOW_Dev2ConData DeviceData;

uint8_t ControllerAddress[] = {0x10, 0x51, 0xdb, 0x1a, 0xc0, 0xfc};

void OnDataSent(const uint8_t *mac_addr, esp_now_send_status_t status)
{
  // 送信完了時の処理
}

void OnDataReceived(const uint8_t* mac_addr, const uint8_t* data, int data_len)
{
   // 受信時の処理
}

void setup() {
  // put your setup code here, to run once:
  espnow.Initialize(OnDataReceived, OnDataSent);

  espnow.SetControllerMacAddr(ControllerAddress);

  Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:
  espnow.Send(&deviceData, sizeof(deviceData));

  delay(10);
}
