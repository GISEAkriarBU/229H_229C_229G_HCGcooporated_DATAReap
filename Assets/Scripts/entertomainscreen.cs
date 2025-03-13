using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class entertomainscreen : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown) // Detect any key press
        {
            SceneManager.LoadScene("maingame"); // Load next scene
        }
    }
}
