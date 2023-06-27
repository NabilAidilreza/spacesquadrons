using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    private float hp;
    Vector3 localscale;
    public int scale;
    private Transform start;
    private Vector3 startVector;
    private Vector3 currVector;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
        start = transform.parent.transform;
        startVector = transform.parent.transform.position - this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent){
            hp = transform.parent.GetComponent<Health>().GetHP();
            localscale.x = hp / scale;
            transform.localScale = localscale;

            //Debug.Log(angle);
            //Quaternion rot = Quaternion.AngleAxis (angle, axis);
            //transform.rotation = Quaternion.Inverse(transform.parent.transform.rotation);
            //transform.RotateAround(transform.parent.transform.position, Vector3.forward, 20f * Time.deltaTime);
        }
    }
}
