using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_idle : MonoBehaviour
{
    Enemy_Behaviour bb;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<Enemy_Behaviour>();
        target = bb.target;
    }
}
