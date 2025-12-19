//Slave側
//M5StampのMacアドレス:34:b7:da:f2:2a:8c

#include"ESPNowEz.h"
#include "MovingAverage.h"
#include <Wire.h>

#define MPU6050_ADDR 0x68
#define MPU6050_AX  0x3B
#define MPU6050_AY  0x3D
#define MPU6050_AZ  0x3F
#define MPU6050_TP  0x41    //  data not used
#define MPU6050_GX  0x43
#define MPU6050_GY  0x45
#define MPU6050_GZ  0x47
#define LED_PIN 4
#define SW_PIN 7
#define DELAY_TIME 10
#define LONG_PRESS 1000

CESPNowEZ espnow(1);

ESPNOW_Dev2ConData DeviceData;

uint8_t ControllerAddress[] = {0x10, 0x51, 0xdb, 0x1a, 0xc0, 0xfc};

void OnDataReceived(const esp_now_recv_info* info, const uint8_t* data, int data_len)
{
  
}

//ボタンが長押しされたかどうかを判別する関数
int Device_button(int pin)
{
  static bool Was_pressed = false;
  static unsigned long Press_time = 0;

  bool Is_pressed = (digitalRead(pin) == HIGH);

  if(Is_pressed && !Was_pressed)
  {
    Press_time = millis();
    Was_pressed = true;

    return 0;
  }

  if(Is_pressed && Was_pressed)
  {
    return 0;
  }

  if(!Is_pressed && Was_pressed)
  {
    unsigned long Press_duration = millis() - Press_time;
    Was_pressed = false;

    if(Press_duration >= LONG_PRESS)
    {
      return 2;
    }
    else
    {
      return 1;
    }
  }

  return 0;
  
}

void Measure_Speed()
{

}

void setup() {
  // put your setup code here, to run once:
  espnow.Initialize(OnDataReceived);

  espnow.SetControllerMacAddr(ControllerAddress);

  pinMode(LED_PIN, OUTPUT);
  pinMode(SW_PIN, INPUT_PULLUP);

  Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:

  DeviceData.id = espnow.ID();

  int Button_result = Device_button(SW_PIN);

  //sprintf(DeviceData.datas,);
  espnow.Send(&DeviceData, sizeof(DeviceData));
  
  if(Button_result > 0)Serial.println(Button_result);

  delay(DELAY_TIME);
}
