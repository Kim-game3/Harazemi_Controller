using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*カメラを切り替えるスクリプト。現在はボタンで切り替えているが、指定秒数経過したら切り替えるというものにしたい*/
public class SwitchCamera : MonoBehaviour
{
    [Tooltip("カメラを格納する配列")]
    [SerializeField] GameObject[] Cameras;

    [Tooltip("切り替えた時のフェードイン/アウトにかかる時間")]
    [SerializeField] private float Fade_duration;

    private int Camera_index;

    private bool Is_Switching;

    private GameObject Fade_Object;
    private Image Fade_Image;

    private Canvas CurrentCanvas;

    private void Awake()
    {
        Camera_index = 0;

        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == Camera_index);
        }

        Initialize_Fade();
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

        yield return StartCoroutine(FadeTo(1f, Fade_duration));

        int nextindex  = (Camera_index + 1) % Cameras.Length;
        for(int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == nextindex);
        }
        Camera_index = nextindex;
        Debug.Log("Camera :" + Camera_index + " is Active");

        yield return StartCoroutine(FadeTo(0f, Fade_duration));

        Is_Switching = false;

        if(nextindex == 0)
        {
            yield return StartCoroutine(FadeTo(1f, Fade_duration));
            SceneManager.LoadScene("ResultScene");
        }
    }

    //FadeObjectを生成し、最背面に移動するもの
    public void Initialize_Fade()
    {
        CurrentCanvas = FindFirstObjectByType<Canvas>();
        if (CurrentCanvas == null)
        {
            Debug.LogError("Canvasが見つかりません。MakeFadeスクリプトをアタッチしているオブジェクトがCanvasの子オブジェクトであることを確認してください。");
            return;
        }
        Create_FadeObject();
    }

    public IEnumerator FadeTo(float targetAlpha, float Fade_duration)
    {
        //ここでImageをCanvasの最前面に移動
        Fade_Image.transform.SetAsLastSibling();

        //ここでAlphaをFade_durationの秒数の時間をかけてtargetAlphaに変更
        Fade_Image.CrossFadeAlpha(targetAlpha, Fade_duration, true);

        yield return new WaitForSeconds(Fade_duration);

        if (Mathf.Approximately(targetAlpha, 0f))
        {
            //フェードが完全に完了したらImageをCanvasの最背面に移動
            Fade_Image.transform.SetAsFirstSibling();
        }
    }

    public void Create_FadeObject()
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
