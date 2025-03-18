using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthFill ;
    public void SetMaxHealth(int maxHealth)
    {
        //set health into fill function
        HealthFill.fillAmount = 1;
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        //calculate health
        HealthFill.fillAmount = (float)currentHealth / maxHealth;
    }
}
