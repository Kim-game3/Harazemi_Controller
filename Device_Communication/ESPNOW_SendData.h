// 送信するデータ形式（構造体）を定義
// このファイルはESPNowEz.hと同一フォルダにあること
// ArduinoIDEではスケッチファイルより上位の階層にあるファイルはincludeできない制約があるため、ここで定義
// 一意であるべき定義が書かれたファイルが通信対象のスケッチがある場所にコピーされるが、現状ではやむを得ず

// プロジェクトごとに必要な構造体を作る
// Device側が複数存在する場合は、idを含めておくことを推奨

#ifndef __ESPNOW_SEND_DATA_H_
#define __ESPNOW_SEND_DATA_H_

// この例ではデバイスからコントローラーに、
//   uint8_t型のid ※絶対に必要
//   uint8_t型のsw
// の2バイトを送る
typedef struct struct_esp_now_d2c_data {
    uint8_t id; // 必ず被らないこと
    char datas[11];
} ESPNOW_Dev2ConData;

// この例ではコントローラーからデバイスに、
//   uint8_t型のid ※絶対に必要
//   uint8_t型のled[3]
//   int16_t型のscore
// の6バイトを送る
typedef struct struct_esp_now_c2d_data {
    uint8_t id; // 必ず被らないこと
    bool startflag;
    bool buzzerflag;
} ESPNOW_Con2DevData;

#endif
