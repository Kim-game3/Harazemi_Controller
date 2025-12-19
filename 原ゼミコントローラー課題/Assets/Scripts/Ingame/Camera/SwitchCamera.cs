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

    [Header("アニメーション関連")]
    [SerializeField] Animator WatchAnimator;

    [SerializeField] string WatchStateName;

    [SerializeField] bool WaitForStateEntry = true;

    [SerializeField] float AnimationWaitTimeout = 5f;

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

        if(WatchAnimator != null && !string.IsNullOrEmpty(WatchStateName))
        {
            yield return StartCoroutine(WaitUntilAnimationFinished(WatchAnimator, WatchStateName, WaitForStateEntry, AnimationWaitTimeout));
        }
        //フェードインする所
        yield return StartCoroutine(FadeTo(1f, Fade_duration));

        int nextindex  = (Camera_index + 1) % Cameras.Length;
        for(int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == nextindex);
        }
        Camera_index = nextindex;
        Debug.Log("Camera :" + Camera_index + " is Active");

        //フェードアウトする所
        yield return StartCoroutine(FadeTo(0f, Fade_duration));

        Is_Switching = false;

        if(nextindex == 0)
        {
            yield return StartCoroutine(FadeTo(1f, Fade_duration));
            SceneManager.LoadScene("ResultScene");
        }
    }
    ///<summary>
    /// Animator の指定ステートが再生完了するまで待つコルーチン。
    /// WaitForStateEntry が true の場合はステートに入るまで待ってから完了を待つ。
    /// false の場合は現在のステートに対して完了を待つ（既に別のステートにいる場合は即終了する可能性あり）。
    /// タイムアウトを超えたら処理を続行する。
    /// </summary>
    private IEnumerator WaitUntilAnimationFinished(Animator animator, string stateName, bool waitForentry, float timeout)
    {
        if(animator == null || string.IsNullOrEmpty(stateName))
        {
           yield break;
        }

        float startTime = Time.unscaledTime;

        bool useTimeout = timeout > 0f;

        if(waitForentry)
        {
            while(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                if(useTimeout && Time.unscaledTime - startTime > timeout)
                {
                    Debug.LogWarning($"WaitUntilAnimationFinished: timeout waiting for state entry '{stateName}'.");
                    yield break;
                }
                yield return null;
            }
        }

        while(true)
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if(info.IsName(stateName))
            {
                if(info.normalizedTime >= 1f)
                {
                    yield break;
                }
            }
            else
            {
                yield break;
            }

            if(useTimeout && Time.unscaledTime - startTime > timeout)
            {
                Debug.LogWarning($"WaitUntilAnimationFinished: timeout waiting for state completion '{stateName}'.");
                yield break;
            }

            yield return null;
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
