using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idle_script : MonoBehaviour
{
    base_behaviour bb;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<base_behaviour>();
        target = bb.target;
    }
}
