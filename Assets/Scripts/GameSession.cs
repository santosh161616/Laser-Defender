using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score;
    int highScore;
    int totalCoins;
    string highScoreKey = "High Score";
    string totalCoinKey = "Total Coins";
    Player playerInstance;
    // Start is called before the first frame update
    void Awake()
    {
        setUpSingleton();
    }

    private void Start()
    {
        playerInstance = FindObjectOfType<Player>();
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        totalCoins = PlayerPrefs.GetInt(totalCoinKey, 0);
    }
    /* So finally this is where we're setting up singleton 
     * So that we can pass on the data that we don't want to destroy 
     * probabbly the script which troubled me more */
    private void setUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int getScore()
    {
        return score;
    }

    public void addScore(int scoreValue)
    {
        score += scoreValue;
        getHighScore();
        setUpCoin(scoreValue);
    }

    public int getHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, score);
        }
        return highScore;
    }
    public void setUpCoin(int scoreValue)
    {
        totalCoins += scoreValue;
        PlayerPrefs.SetInt(totalCoinKey, totalCoins);
    }

    public int getTotalCoins()
    {
        return totalCoins;
    }

    public int getPlayerHealth()
    {
        return playerInstance.getPlayerHealth();
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }
}
