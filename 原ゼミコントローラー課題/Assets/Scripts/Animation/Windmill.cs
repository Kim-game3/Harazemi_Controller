using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Windmill : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void StartRotate()
    {
        animator.Play("Rotate_Windmill");

        StartCoroutine(Waittime());
    }

    private IEnumerator Waittime()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("ResultScene");
    }
}