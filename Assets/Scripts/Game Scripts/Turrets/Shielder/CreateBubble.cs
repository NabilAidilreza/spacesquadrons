using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBubble : MonoBehaviour
{
    public Shielder SHIELDER;
    public GameObject ShieldPrefab;
    public float Cooldown;
    private float CD;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.childCount == 2)
        {
            if (CD <= 0)
            {
                SHIELDER.SetStatus(false);
                StartCoroutine(CoolDown());
                CD = Cooldown;
            }
            else
            {
                CD -= Time.deltaTime;
            }
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Cooldown);
        GameObject SHIELD = Instantiate(ShieldPrefab, transform.position, Quaternion.identity);
        SHIELD.transform.parent = this.transform;
        SHIELDER.SetStatus(true);
    }
}
