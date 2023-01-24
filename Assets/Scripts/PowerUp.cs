using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] int plusPower = 100;
    
    /* If player is being hit by powerUp then
     * Increase the power of player */
    public int getPower()
    {
        return plusPower;
    }
    // destroy the powerUp gameObject after powerUp
    public void getHit()
    {
        Destroy(gameObject);
    }
}
