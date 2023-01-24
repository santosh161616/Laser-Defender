using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoins : MonoBehaviour
{
    Text totalCoins;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        totalCoins = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        totalCoins.text = gameSession.getTotalCoins().ToString();
    }
}
