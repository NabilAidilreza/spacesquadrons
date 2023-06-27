using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROGRESS_CD : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.GetChild(0).name == "Sprites")
        {
            Destroy(gameObject);
        }
    }
}

