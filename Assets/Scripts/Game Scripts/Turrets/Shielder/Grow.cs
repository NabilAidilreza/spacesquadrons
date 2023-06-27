using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    private float CD = 10.0f;
    // Update is called once per frame
    void Update()
    {
        if(CD > 0)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }
        if(CD < 0)
        {
            CD = 0;
        }
        else
        {
            CD -= Time.deltaTime;
        }
    }
}
