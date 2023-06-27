using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCheckDestroyed : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.GetChild(0).name == "Waypoint")
        {
            Destroy(gameObject);
        }
    }
}
