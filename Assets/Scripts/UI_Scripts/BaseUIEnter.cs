using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIEnter : MonoBehaviour
{
    public GameObject MainLayer;
    public GameObject FacilityLayer;
    public GameObject TurretLayer;
    public GameObject Basic_Layer;
    public GameObject Special_Layer;
    public LayerMask Layer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D CheckBase = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f,Layer);
            //Debug.Log(CheckBase);
            if (CheckBase == null)
            {
                MainLayer.SetActive(false);
                FacilityLayer.SetActive(false);
                TurretLayer.SetActive(false);
                Basic_Layer.SetActive(false);
                Special_Layer.SetActive(false);
            }
            else if (CheckBase.GetComponent<RTSBase>() != null)
            {
                if(Basic_Layer.activeSelf || Special_Layer.activeSelf || FacilityLayer.activeSelf )
                {
                    
                }
                else
                {
                    MainLayer.SetActive(true);
                }
                
            }
        }
    }
}
