using UnityEngine;

public class Player_Swing : MonoBehaviour
{
    [SerializeField] Animator Player_animator;

    [SerializeField] string Trigger_Name = "Swing";

    private bool Has_Played;
    public void Play_Animation()
    {
        if(!Has_Played)
        {
            Play_once();
        }
    }

    private void Play_once()
    {
        if(Player_animator == null)
        {
            Debug.LogWarning("Animator is not attached!");
            return;
        }

        Player_animator.ResetTrigger(Trigger_Name);
        Player_animator.SetTrigger(Trigger_Name);

        Has_Played = true;
    }

    public void Reset_flag()
    {
        Has_Played = false;
    }
}
