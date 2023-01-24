using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    Text playerScore;
   // Player playerSession;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
      //  playerSession = FindObjectOfType<Player>();
    }
    
    // Displaying the player health on screen
    void Update()
    {
        playerScore.text = gameSession.getPlayerHealth().ToString();
    }
}
