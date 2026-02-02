using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Swing : MonoBehaviour
{
    private Animator animator;

    Windmill windmill;

    public int[] Value;

    private int value;
    private int prevalue;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        var current = Keyboard.current;

        var space = current.spaceKey;
        Comparevalue();

        if(space.wasPressedThisFrame)
        {
            PlaySwing();
        }
    }

    private void Comparevalue()
    {
        foreach (int v in Value)
        {
            value = value * 10 + v;
        }


        if (value != prevalue)
        {
            PlaySwing();
        }

        prevalue = value;
    }

    private void PlaySwing()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        animator.SetBool("Swing", true);

        if(state.IsName("Baseball_Swing") && state.normalizedTime >= 1.0f)
        {
            PlayWindmill();
        }
    }

    private void PlayWindmill()
    {
        animator.SetBool("Swing", false);

        windmill.StartRotate();
    }
}
