using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    [Header("Player Shield")]
    public GameObject firstLevelShield;
    public GameObject SecondLevelShield;
    public GameObject thirdLevelShield;
    public bool disableShield = false;

    /*Using this part of the code to check it shield is active on the player or not, 
    if not then activating the levelFirst Shield. It is woring with the GameObject in the Heirarchy*/

    private void Start()
    {
        disableLoadOnShield();
    }

    private void disableLoadOnShield()
    {
        if (firstLevelShield || SecondLevelShield || thirdLevelShield != null)
        {
            if (disableShield)
            {
                firstLevelShield.SetActive(false);
                SecondLevelShield.SetActive(false);
                thirdLevelShield.SetActive(false);
            }
        }
    }

    public void activateFirstLaser()
    {
        firstLevelShield.SetActive(true);
        SecondLevelShield.SetActive(false);
        thirdLevelShield.SetActive(false);
    }

    public void activateSecondLaser()
    {
        firstLevelShield.SetActive(false);
        SecondLevelShield.SetActive(true);
        thirdLevelShield.SetActive(false);
    }

    public void activateThirdLaser()
    {
        firstLevelShield.SetActive(false);
        SecondLevelShield.SetActive(false);
        thirdLevelShield.SetActive(true);
    }
}
