using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScean : MonoBehaviour
{
    [SerializeField] float timeDelaySeconds = 2;

    // Helping player to loadStart  menu.
    public void loadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    // This is where we can load first level.
    public void loadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().resetGame();
    }

    // After levelFirst it helps load next level.
    public void loadNextLevel()
    {
        StartCoroutine(waitAndloadNext());
    }

    IEnumerator waitAndloadNext()
    {
        yield return new WaitForSeconds(timeDelaySeconds);
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    // Every game needs a gameover script.
    public void loadGameOver() 
    {
        StartCoroutine(waitAndLoad());
    }
    
    // That littile time before gameOver scean.
    IEnumerator waitAndLoad()
    {
        yield return new WaitForSeconds(timeDelaySeconds);
        SceneManager.LoadScene("Game Over");
    }

    // quit the application.
    public void quitGame()
    {
        Application.Quit();
    }

}
