using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.GetChild(0).name == "Platform")
        {
            Destroy(gameObject);
        }
    }
}

