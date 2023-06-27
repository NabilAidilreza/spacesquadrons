using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flee_script : MonoBehaviour
{
    base_behaviour bb;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<base_behaviour>();
        target = bb.target;
        if (bb.fleeScript == null)
        {
            bb.fleeScript = gameObject.AddComponent<Flee>();
            bb.fleeScript.target = target;
            bb.fleeScript.weight = 20.0f;
            bb.fleeScript.enabled = true;
        }
    }
    private void OnDestroy()
    {
        Destroy(bb.fleeScript);
    }
}
