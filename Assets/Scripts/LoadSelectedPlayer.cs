using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelectedPlayer : MonoBehaviour
{
    public GameObject[] loadPlayer;
    public Transform spawnPoint;
    void Start()
    {
        int selectPlayer = PlayerPrefs.GetInt("Acitve Player");
        GameObject prefeb = loadPlayer[selectPlayer];
        GameObject cloan = Instantiate(prefeb, spawnPoint.position, Quaternion.identity);
    }
}
