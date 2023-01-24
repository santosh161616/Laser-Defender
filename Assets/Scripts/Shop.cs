using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Shop : MonoBehaviour
{
    [SerializeField] GameObject disableObject;
    [SerializeField] GameObject disableObject1;
    [SerializeField] GameObject enableObject;

    public void disableContainer()
    {
        if(disableObject && disableObject1 && enableObject != null)
        {
            disableObject.SetActive(false);
            disableObject1.SetActive(false);
            enableObject.SetActive(true);
        }        
    }

}
