using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBackTomainScene : MonoBehaviour
{
    public void RetryGame ()
    {
        SceneManager.LoadSceneAsync (0); 
        
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

   

}
