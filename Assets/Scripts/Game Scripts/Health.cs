using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    public float currhp;
    public GameObject Explosion;
    public GameObject EscapePod;
    // Start is called before the first frame update
    void Start()
    {
        currhp = hp;
    }
    void Update()
    {
        if(currhp > hp)
        {
            currhp = hp;
        }
    }
    public void TakeDmg(float dmg)
    {
        currhp -= dmg;
        if (currhp <= 0)
        {
            Die();
        }
    }
    public void Regenerate()
    {
        currhp += 0.1f;
        if (currhp >= hp)
        {
            currhp = hp;
        }
    }
    public void AddHP(float value)
    {
        currhp += value;
    }
    public float GetHP()
    {
        return currhp;
    }
    public float GetMaxHP()
    {
        return hp;
    }
    private void Die()
    {
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Instantiate(EscapePod, transform.position, Quaternion.identity);
    }
}
