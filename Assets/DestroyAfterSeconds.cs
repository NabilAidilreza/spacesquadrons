using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float Seconds;
    private float S;
    // Start is called before the first frame update
    private void Start()
    {
        S = Seconds;
    }
    // Update is called once per frame
    void Update()
    {
        if(S <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            S -= Time.deltaTime;
        }
    }
}
