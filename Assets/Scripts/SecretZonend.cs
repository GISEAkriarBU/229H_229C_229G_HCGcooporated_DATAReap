using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretZoneend : MonoBehaviour
{
    public float SecretCountDown;

    private void FixedUpdate()
    {

        SecretCountDown += Time.deltaTime;
    }

    private void Update()
    {
        if (SecretCountDown > 125) { SceneManager.LoadScene("SecretPath"); }
    }
}
