using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLayerEnter : MonoBehaviour
{
    public GameObject MainLayer;
    public GameObject FacilitiesLayer;
    public GameObject TurretLayer;
    public void EnterFacility()
    {
        MainLayer.SetActive(false);
        FacilitiesLayer.SetActive(true);
    }
    public void EnterTurret()
    {
        MainLayer.SetActive(false);
        TurretLayer.SetActive(true);
    }

}
