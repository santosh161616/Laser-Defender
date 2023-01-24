using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreDisplay : MonoBehaviour
{
    Text highScore;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        highScore = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        highScore.text = gameSession.getHighScore().ToString();
    }
}
