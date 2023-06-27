using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float hp;
    Vector3 localscale;
    public int scale;
    private Transform start;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
        start = transform.parent.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.transform.GetChild(0) != start)
        {
            Destroy(gameObject);
        }
        else
        {
            hp = transform.parent.GetComponentInChildren<Health>().GetHP();
            localscale.x = hp / scale;
            transform.localScale = localscale;
        }
    }

}
