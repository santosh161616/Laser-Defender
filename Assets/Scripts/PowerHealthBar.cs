using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerHealthBar : MonoBehaviour
{
    Image healthbar;
    string playerHealthToken = "Updated Health";
    float maxHealth = 100f; 
    float temp, fullBar = 0f;
    public static float health;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        healthbar = GetComponent<Image>();
        health = maxHealth;
        temp = PlayerPrefs.GetFloat(playerHealthToken, 1f);
    }

    public void UpdateHealth()
    {
        int coins = gameSession.getTotalCoins();
        if (coins > 100)
        {
            if (temp > 0)
            {
                if (temp < 0)
                {
                    return; 
                }
                else
                {
                    temp -= .20f;                    
                    PlayerPrefs.SetFloat(playerHealthToken, temp);
                    gameSession.setUpCoin(-100);
                    healthbar.fillAmount = temp;
                }
            }
            else
            {
                if(temp<=0)
                {
                    temp = 0;
                    PlayerPrefs.SetFloat(playerHealthToken, temp);
                    healthbar.fillAmount = temp;
                    Debug.Log("Temp Vlaue: " + temp);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       // healthbar.fillAmount = health / maxHealth;
    }
}
