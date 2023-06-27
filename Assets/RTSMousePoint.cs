using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSMousePoint : MonoBehaviour
{
    private bool READY;
    // Start is called before the first frame update
    void Start()
    {
        READY = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (READY == true){
            Vector3 PosRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PosRelative.z = 0;
            this.gameObject.transform.position = PosRelative;
            READY = false;
        }
    }
    public void SetReady(bool a){
        READY = a;
    }
}
