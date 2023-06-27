using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLayerEnter : MonoBehaviour
{
    public GameObject MainLayer;
    public GameObject TurretLayer;
    public GameObject BasicLayer;
    public GameObject SpecialLayer;
    public void EnterBasic()
    {
        MainLayer.SetActive(false);
        TurretLayer.SetActive(false);
        BasicLayer.SetActive(true);
    }
    public void EnterSpecial()
    {
        MainLayer.SetActive(false);
        TurretLayer.SetActive(false);
        SpecialLayer.SetActive(true);
    }

}