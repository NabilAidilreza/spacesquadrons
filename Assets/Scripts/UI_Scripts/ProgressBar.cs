using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float Points;
    Vector3 localscale;
    public float scaleFactor;
    public GameObject Prefab;
    public bool IsUnit;
    private int i = 0;
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
            if(IsUnit == true)
            {
                GameObject Unit = Instantiate(Prefab, transform.position, Quaternion.identity);
                Unit.GetComponentInChildren<RTSGroup>().SetTeamNum(i); 
                Destroy(gameObject);
            }
            else
            {
                Instantiate(Prefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            localscale.x += Time.deltaTime/scaleFactor;
            transform.localScale = localscale;
        }
    }
    public void SetIndex(int Index)
    {
        i = Index;
    }
}
