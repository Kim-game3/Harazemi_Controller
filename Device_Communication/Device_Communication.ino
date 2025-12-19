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
#define BUZZER_PIN 5
#define SW_PIN 7
#define DELAY_TIME 10
#define LONG_PRESS 1000

const int maN = 50;

short int AccX, AccY, AccZ;
short int Temp;
short int GyroX, GyroY, GyroZ;

CESPNowEZ espnow(1);

ESPNOW_Con2DevData Controllerdata;
ESPNOW_Dev2ConData DeviceData;
MovingAverage ma(maN);

uint8_t ControllerAddress[] = {0x10, 0x51, 0xdb, 0x1a, 0xc0, 0xfc};

int deviceflag;

void OnDataReceived(const esp_now_recv_info* info, const uint8_t* data, int data_len)
{
  memcpy(&Controllerdata, data, data_len);
  deviceflag = 1;

  if(Controllerdata.buzzer)
  {
    Sound_Buzzer(BUZZER_PIN);
  }

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

int Measure_Speed()
{
  Wire.beginTransmission(MPU6050_ADDR);
  Wire.write(MPU6050_AX);
  Wire.endTransmission(); 

  Wire.requestFrom(MPU6050_ADDR, 14);

  AccX = Wire.read() << 8;  AccX |= Wire.read();
  AccY = Wire.read() << 8;  AccY |= Wire.read();
  AccZ = Wire.read() << 8;  AccZ |= Wire.read();
  Temp = Wire.read() << 8;  Temp |= Wire.read();  //  (Temp-12421)/340.0 [degC]
  GyroX = Wire.read() << 8; GyroX |= Wire.read();
  GyroY = Wire.read() << 8; GyroY |= Wire.read();
  GyroZ = Wire.read() << 8; GyroZ |= Wire.read();

  int AccX_ma = ma.Add(AccX);

  return AccX_ma;
}

void Sound_Buzzer(int pin)
{
  int notes[] = {659, 784, 1047};  // ミ → ソ → 高ド
  int times[] = {100, 100, 200};
  
  int count = 0;

  for (int i = 0; i < 3; i++) {
    if(count > 3)
    {
      break;
    }
    tone(pin, notes[i]);
    delay(times[i]);

    count++;
  }
  noTone(pin);
}

void setup() {
  // put your setup code here, to run once:
  espnow.Initialize(OnDataReceived);

  espnow.SetControllerMacAddr(ControllerAddress);

  pinMode(LED_PIN, OUTPUT);
  pinMode(BUZZER_PIN, OUTPUT);
  pinMode(SW_PIN, INPUT_PULLUP);

  Serial.begin(115200);

  Wire.begin();

  Wire.beginTransmission(MPU6050_ADDR);
  Wire.write(0x6B);
  Wire.write(0);
  Wire.endTransmission();
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(LED_PIN, HIGH);
  
  DeviceData.id = espnow.ID();
  
  int Value = Measure_Speed();
  int Button_result = Device_button(SW_PIN);

  sprintf(DeviceData.datas, "S%+06dE%d", Value, Button_result);
  Serial.println(DeviceData.datas);
  espnow.Send(&DeviceData, sizeof(DeviceData));
  
  //if(Button_result > 0)Serial.println(Button_result);

  delay(DELAY_TIME);
}
