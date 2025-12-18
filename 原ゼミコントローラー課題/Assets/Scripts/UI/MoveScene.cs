using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveScene : MonoBehaviour
{
    [Tooltip("遷移先のシーン名")]
    [SerializeField] string Scene_Name;

    [Tooltip("遷移にかかる時間(ロード時間)")]
    [SerializeField] float Load_Time;

    private GameObject Fade_Object;
    private Image Fade_Image;

    private Canvas CurrentCanvas;

    private void Start()
    {
        var fade = GameObject.Find("Fade_Object");
        
        if(fade == null)
        {
            Initialize_Fade();
        }
    }

    public void Move_Scene()
    {
        StartCoroutine(moveScene());
    }

    private IEnumerator moveScene()
    {
        yield return StartCoroutine(FadeTo(1f, Load_Time));
        SceneManager.LoadScene(Scene_Name);
    }

    public IEnumerator Move_MaintoResult()
    {
        yield return StartCoroutine(FadeTo(1f, Load_Time));
        SceneManager.LoadScene("ResultScene");
    }

    public void Quit_Game()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // エディタの再生モードを停止
#else
            Application.Quit();  // ビルド環境でアプリケーションを終了
#endif
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
