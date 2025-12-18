using UnityEngine;
using System.Collections;
using TMPro;

public class CountDown : MonoBehaviour
{
    [Tooltip("～～しようみたいなテキスト")]
    [SerializeField] TextMeshProUGUI Purpose_Text;

    [Tooltip("カウントダウン時に用いるテキスト")]
    [SerializeField] TextMeshProUGUI Countdown_Text;

    [SerializeField] TextMeshProUGUI Start_Text;
    [SerializeField] float Displaytime;

    [Tooltip("目的のテキストを表示する時間")]
    [SerializeField] float Purpose_Displaytime = 3.5f;

    [Tooltip("カウントダウンの秒数")]
    [SerializeField] int Countdown_Second = 3;

    [SerializeField] GameObject Panel;

    private bool Is_Running;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Purpose_Text != null)
        {
            Purpose_Text.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Purpose_Text is not attachied!");
        }

        if(Countdown_Text != null)
        {
            Countdown_Text.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Countdown_Text is not attachied!");
        }

        if(Start_Text != null)
        {
            Start_Text.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Start_Text is not attachied!");
        }

        if(Panel != null)
        {
            Panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel is not attachied!");
        }

        Start_Countdown();
    }

    public void Start_Countdown()
    {
        if (Is_Running)
        {
            return;
        }
        StartCoroutine(ShowPurposeBeforCountdown());
    }

    private IEnumerator ShowPurposeBeforCountdown()
    {
        if(Purpose_Text == null || Countdown_Text == null)
        {
            yield break;
        }

        Is_Running = true;

        Panel.SetActive(true);
        Purpose_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(Purpose_Displaytime);

        Purpose_Text.gameObject.SetActive(false);

        if(Countdown_Second <= 0)
        {
            Countdown_Text.gameObject.SetActive(false);
            Is_Running = false;
            yield break;
        }

        Countdown_Text.gameObject.SetActive(true);

        for(int i = Countdown_Second; i > 0; i--)
        {
            Countdown_Text.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        Countdown_Text.gameObject.SetActive(false);
        Start_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(Displaytime);

        Start_Text.gameObject.SetActive(false);
        Panel.SetActive(false);

        Is_Running = false;
    }
}
