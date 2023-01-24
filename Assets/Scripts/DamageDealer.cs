using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    //Returing the damage an enemy can cause to player
    public int getDamage()
    {
        return damage;
    }

    //Also hit() is being used to destroy the gameObject and also count dying enemies.
    public void hit()
    {
        Destroy(gameObject);
    }
}
