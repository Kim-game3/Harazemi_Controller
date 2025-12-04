using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] GameObject[] Cameras;

    private int Camera_index;
    private void Start()
    {
        Camera_index = 0;

        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == Camera_index)
            {
                Cameras[i].gameObject.SetActive(true);
            }
            else
            {
                Cameras[i].gameObject.SetActive(false);
            }
        }
    }

    public void Switch_Camera()
    {

        for(int i = 0; i < Cameras.Length; i++)
        {
            if(i == Camera_index)
            {
                Cameras[i].gameObject.SetActive(true);
            }
            else
            {
                Cameras[i].gameObject.SetActive(false);
            }
        }

        Debug.Log("Camera " + Camera_index + " is Active.");

        if (Camera_index < Cameras.Length - 1)
        {
            Camera_index++;
        }
        else
        {
            Camera_index = 0;
        }
    }
}
