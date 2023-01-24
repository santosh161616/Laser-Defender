using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public GameObject[] selectPlayer;
    public int activePlayer = 0;

    void Start()
    {
        for (int i = 0; i < selectPlayer.Length; i++)
        {
            if(selectPlayer[i] != null)
            {
                selectPlayer[i].SetActive(false);
            }
        }
        selectPlayer[activePlayer].SetActive(true);
    }
    public void Next()
    {
        selectPlayer[activePlayer].SetActive(false);
        activePlayer = (activePlayer + 1) % selectPlayer.Length;
        selectPlayer[activePlayer].SetActive(true);
    }

    public void Previous()
    {
        selectPlayer[activePlayer].SetActive(false);
        activePlayer--;
        if(activePlayer < 0)
        {
            activePlayer += selectPlayer.Length;
        }
        selectPlayer[activePlayer].SetActive(true);
    }

    public void Select()
    {
        PlayerPrefs.SetInt("Acitve Player", activePlayer);
    }

}
