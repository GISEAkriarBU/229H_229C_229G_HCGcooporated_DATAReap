using UnityEngine;

public class AudioManage : MonoBehaviour
{
   [SerializeField] AudioSource BackgroundMusic;

    private void Start()
    {
        BackgroundMusic.Play();
    }
}
