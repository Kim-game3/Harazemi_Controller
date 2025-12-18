// Unity�ŃV���A���ʐM�ő����Ă���f�[�^��f�R�[�h���鐗�`
// 2025_8��Ver.


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
    // ����Ώۂ̃I�u�W�F�N�g�p�ɐ錾���Ă����āAStart�֐���Ŗ��O�Ō���
    GameObject targetObject;

    // ����ΏۂɃA�^�b�`���ꂽ�X�N���v�g
    //Player targetScript; // ���̋L�q�ł̓A�^�b�`����GameObject��Player�X�N���v�g���A�^�b�`����Ă���K�v������

    GameObject harazemichanObject;

    // �V���A���ʐM�̃N���X�A�N���X���͐�������������
    public SerialHandler serialHandler;

    void Start()
    {
        // ����Ώۂ̃I�u�W�F�N�g��擾�A���̃I�u�W�F�N�g��Main.cs���֘A�t�����Ă���
        targetObject = GameObject.Find("PlayerObject"); // ���̋L�q�ł�Unity�̃q�G�����L�[��GameMaster�I�u�W�F�N�g������K�v������B

        // ����ΏۂɃA�^�b�`���ꂽ�X�N���v�g��擾�B
        // �啶���A���������ʂ���̂ŁAplayer.cs�������̂Ȃ�up�vlayer�B
        //targetScript = targetObject.GetComponent<Player>(); // Player�X�N���v�g���A�^�b�`����Ă���K�v������

        harazemichanObject = GameObject.Find("harazemi_chan025_chara");

        // �M����M���ɌĂ΂��֐��Ƃ���OnDataReceived�֐���o�^
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // Unity����Arduino�ɑ���ꍇ�̓R�R�ɋL�q
        //SerialHandler.Write("")��g��
        if (targetScript.jklPress[0])
        {
            targetScript.jklPress[0] = false;
            if (targetScript.jklToggle[0])
            {
                // LED ON
                serialHandler.Write("a");
            }
            else
            {
                // LED OFF
                serialHandler.Write("b");
            }
        }

        if (targetScript.jklPress[1])
        {
            targetScript.jklPress[1] = false;
            if (targetScript.jklToggle[1])
            {
                // LED ON
                serialHandler.Write("c");
            }
            else
            {
                // LED OFF
                serialHandler.Write("d");
            }
        }

        if (targetScript.jklPress[2])
        {
            targetScript.jklPress[2] = false;
            if (targetScript.jklToggle[2])
            {
                // LED ON
                serialHandler.Write("e");
            }
            else
            {
                // LED OFF
                serialHandler.Write("f");
            }
        }

        if (blockDetectorScript.led1on)
        {
            blockDetectorScript.led1on = false;
            serialHandler.Write("a");
        }

        if (blockDetectorScript.led1off)
        {
            blockDetectorScript.led1off = false;
            serialHandler.Write("b");
        }

        if (blockDetectorScript.led2on)
        {
            blockDetectorScript.led2on = false;
            serialHandler.Write("c");
        }

        if (blockDetectorScript.led2off)
        {
            blockDetectorScript.led2off = false;
            serialHandler.Write("d");
        }

        if (blockDetectorScript.led3on)
        {
            blockDetectorScript.led3on = false;
            serialHandler.Write("e");
        }

        if (blockDetectorScript.led3off)
        {
            blockDetectorScript.led3off = false;
            serialHandler.Write("f");
        }

        if (blockDetectorScript.soudnPlay)
        {
            blockDetectorScript.soudnPlay = false;
            serialHandler.Write("g");
        }
    }

    //��M�����M��(message)�ɑ΂��鏈��
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // �����Ńf�R�[�h��������L�q
        string receivedData;
        int t;

        receivedData = message.Substring(1, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[0] = t;

        receivedData = message.Substring(2, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[1] = t;

        receivedData = message.Substring(3, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[2] = t;

        receivedData = message.Substring(4, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[3] = t;

        receivedData = message.Substring(5, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[4] = t;

        
    }
}*/
