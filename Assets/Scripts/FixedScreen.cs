using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedScreen : MonoBehaviour
{
    public float SecretCountDown;

    private void FixedUpdate()
    {

        SecretCountDown += Time.deltaTime;
    }

    private void Update()
    {
        if (SecretCountDown > 5) { SceneManager.LoadScene("Mainenter"); }
    }


}
