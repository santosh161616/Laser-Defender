using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBomb : MonoBehaviour
{
    [SerializeField] float rotation = 300f;
    //private GameObject gameObject;
    //Vector2 spin;

    private float getSpin()
    {
        return rotation += 1;
    }
    
    /* Rotating the bombs enemy is throwing on player*/
    void Update()
    {
        transform.Rotate(0, 0, getSpin() * Time.deltaTime);
    }
}
