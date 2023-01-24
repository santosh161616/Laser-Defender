using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPower : MonoBehaviour
{
    [SerializeField] GameObject powerPill;
    [SerializeField] float powerPillSpeed = 10f;

    /*This script is being used for enemies that
     * after they died they instantiate power pill
     * that player can use to increase power */
    public void PowerInstantiate()
    {
        float temp  = Random.Range(0.0f, 1.0f);
        if(temp < 0.25)
        {
            GameObject power = Instantiate(powerPill, transform.position, Quaternion.identity) as GameObject;
            power.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerPillSpeed);
        }
        else
        {
            return;
        }
    }

}
