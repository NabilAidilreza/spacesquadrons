using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ_HealthBar : MonoBehaviour
{
    public Slider HB;
    private float HQ_HP;
    // Start is called before the first frame update
    void Start()
    {
        HQ_HP = GetComponent<Health>().GetHP();
        HB.maxValue = GetComponent<Health>().GetMaxHP();
    }

    // Update is called once per frame
    void Update()
    {
        HQ_HP = GetComponent<Health>().GetHP();
        HB.value = HQ_HP;
    }
}
