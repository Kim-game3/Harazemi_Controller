using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SwitchCamera : MonoBehaviour
{
    [Tooltip("カメラを格納する配列")]
    [SerializeField] GameObject[] Cameras;

    [Tooltip("切り替えた時のフェードイン/アウトにかかる時間")]
    [SerializeField] private float Fade_duration;

    private int Camera_index;

    private GameObject Fade_Object;
    private Image Fade_Image;

    private Canvas CurrentCanvas;

    private bool Is_Switching;
    private void Start()
    {
        Camera_index = 0;

        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == Camera_index);
        }

        CurrentCanvas = FindFirstObjectByType<Canvas>();
        Create_FadeObject();
    }

    public void Switch_Camera()
    {
        if(Is_Switching || Cameras == null || Cameras.Length == 0)
        {
            Debug.LogError("ぬるぽ");
            return;
        }
        StartCoroutine(SwitchRoutine());
    }

    private IEnumerator SwitchRoutine()
    {
        Is_Switching = true;

        yield return StartCoroutine(FadeTo(1f));

        int nextindex  = (Camera_index + 1) % Cameras.Length;
        for(int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == nextindex);
        }
        Camera_index = nextindex;
        Debug.Log("Camera :" + Camera_index + " is Active");

        yield return StartCoroutine(FadeTo(0f));

        Is_Switching = false;
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        //ここでImageをCanvasの最前面に移動
        Fade_Image.transform.SetAsLastSibling();

        //ここでAlphaをFade_durationの秒数の時間をかけてtargetAlphaに変更
        Fade_Image.CrossFadeAlpha(targetAlpha, Fade_duration, true);

        yield return new WaitForSeconds(Fade_duration);

        if(Mathf.Approximately(targetAlpha, 0f))
        {
            //フェードが完全に完了したらImageをCanvasの最背面に移動
            Fade_Image.transform.SetAsFirstSibling();
        }
    }

    private void Create_FadeObject()
    {
        Fade_Object = new GameObject("Fade_Object");
        Fade_Object.transform.SetParent(CurrentCanvas.transform, false);

        Fade_Image = Fade_Object.AddComponent<Image>();
        Fade_Image.color = Color.black;

        var rt = Fade_Image.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;

        Fade_Image.canvasRenderer.SetAlpha(0f);

        Fade_Image.transform.SetAsFirstSibling();
    }
    
}
