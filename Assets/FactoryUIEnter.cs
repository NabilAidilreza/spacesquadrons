using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryUIEnter : MonoBehaviour
{
    public GameObject MainLayer;
    public LayerMask Layer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D CheckBase = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f, Layer);
            if (CheckBase == null)
            {
                MainLayer.SetActive(false);
            }
            else if (CheckBase.GetComponent<RTSFacility>() != null)
            {
                MainLayer.SetActive(true);
            }
        }
    }
}
