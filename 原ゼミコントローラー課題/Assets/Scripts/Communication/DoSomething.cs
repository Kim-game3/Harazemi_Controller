// Unityでシリアル通信で送られてくるデータをデコードする雛形
// 2025_8月Ver.


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
    // シリアル通信のクラス、クラス名は正しく書くこと
    public SerialHandler serialHandler;

    // 制御対象のオブジェクト用に宣言しておいて、Start関数内で名前で検索
    GameObject targetObject;

    // 制御対象にアタッチされたスクリプト
    //Player targetScript; // この記述ではアタッチするGameObjectにPlayerスクリプトがアタッチされている必要がある

    GameObject harazemichanObject;

    Player targetScript;

    Player_Swing playerswing;

    SwitchCamera switchcamera;

    [SerializeField] private TextMeshProUGUI StartText;
    [SerializeField] private TextMeshProUGUI DebugText;

    private bool ProcessedStart;

    private bool receivedFlag = false;

    private string beforedata;

    void Start()
    {
        // 制御対象のオブジェクトを取得、このオブジェクトにMain.csが関連付けられている
        targetObject = GameObject.Find("PlayerObject"); // この記述ではUnityのヒエラルキーにGameMasterオブジェクトがいる必要がある。

        // 制御対象にアタッチされたスクリプトを取得。
        // 大文字、小文字を区別するので、player.csを作ったのなら「p」layer。
        //targetScript = targetObject.GetComponent<Player>(); // Playerスクリプトがアタッチされている必要がある

        harazemichanObject = GameObject.Find("harazemi_chan025_chara");

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;

        //playerswing = targetObject.GetComponent<Player_Swing>();
        if(playerswing == null)
        {
            Debug.LogWarning("Player_Swing script is not attached to PlayerObject!");
        }
    }
    void Update()
    {
        // UnityからArduinoに送る場合はココに記述
        //SerialHandler.Write("")を使う

            serialHandler.Write("B");//MPUの計測開

        if(receivedFlag)
        {
            receivedFlag = false;
            serialHandler.Write("A");//計測ストップ＆ブザーを鳴らす
        }

    }
    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述
        //receivedData = message.Substring(1, 1);というようにやると一文字ずつデコードできる。前の数字を増やせば次の文字になる
        string receivedData;

        receivedData = message;

        /*if(!string.Equals(receivedData, beforedata, StringComparison.Ordinal))
        {
            beforedata = receivedData;

            playerswing.Play_Animation();
        }
        else
        {
            return;
        }*/

        DebugText.text = "Datas:" + receivedData;

        Debug.Log(message);

        receivedFlag = true;
    }
}
