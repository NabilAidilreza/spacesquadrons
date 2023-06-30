using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Bar : MonoBehaviour
{
    public float Points;
    Vector3 localscale;
    public float scaleFactor;
    public bool IsUnit;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Points -= Time.deltaTime;
        if(Points <= 0)
        {
            Destroy(gameObject);
            
        }
        else
        {
            localscale.x += Time.deltaTime/scaleFactor;
            transform.localScale = localscale;
        }
    }
}
