using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckActiveLaser : MonoBehaviour
{
    /*Using this script to activate the new laser and also deactivating the existing laser */
    public void ActivateLaser()
    {
        FindObjectOfType<Player>().laserFirstCheck = false;
        FindObjectOfType<Player>().laserSecondCheck = true;
    }

    /*This is the common method using to destory the laser when it hits the enemy prefab */
    public void getHit()
    {
        Destroy(gameObject);
    }

}
