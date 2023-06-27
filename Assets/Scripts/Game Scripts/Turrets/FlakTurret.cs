using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakTurret : Turret
{
    public bool IsPlayer;
    public Rigidbody2D Projectile;
    public Rigidbody2D Projectile2;
    public float EngageRange;
    public Transform ShootPoint1;
    public Transform ShootPoint2;
    public Transform ShootPoint3;
    public float CoolDownDuration;
    private float CoolDown;
    public float CoolDownDurationMainCannon;
    private float CoolDownMainCannon;
    private Transform target;
    public float offset;
    private int i;
    private float projectileSpeed = 120f;
    private Animator Flak;

    public float ShootWindowPeriod;
    private float SWP;
    public float OverHeatDuration;
    public GameObject OH_Effect;
    private GameObject OH;
    public GameObject ShockEffect;
    private GameObject Shock;
    // Start is called before the first frame update
    void Start()
    {
        Flak = gameObject.GetComponent<Animator>();
        SWP = ShootWindowPeriod;
        CoolDown = CoolDownDuration;
        CoolDownMainCannon = CoolDownDurationMainCannon;
        i = 0;
        status = "Online";
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "Online")
        {
            MainFunc();
        }
        else if (status == "Offline")
        {
            DisableTurret();
            status = "Null";
        }
    }
    IEnumerator Overheat()
    {
        yield return new WaitForSeconds(OverHeatDuration);
        SWP = ShootWindowPeriod;
    }
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(5f);
        status = "Online";
    }
    public void DisableTurret()
    {
        Shock = Instantiate(ShockEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
        Flak.SetBool("Shooting", false);
        StartCoroutine("Recover");
    }
    public void MainFunc()
    {
        target = FindClosestObject();
        if (target == null)
        {
            Flak.SetBool("Shooting", false);
        }
        else
        {
            Vector3 relativeTarget = (target.transform.position - transform.position).normalized;
            Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relativeTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 3f * Time.deltaTime);
            float Range = Vector2.Distance(transform.position, target.transform.position);
            if (Range < EngageRange)
            {
                if (SWP < 0f)
                {
                    Flak.SetBool("Shooting", false);
                    OH = Instantiate(OH_Effect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
                    StartCoroutine("Overheat");
                    SWP = 0f;
                }
                else if (SWP > 0f)
                {
                    Flak.SetBool("Shooting", true);
                    if (CoolDown <= 0)
                    {
                        if (i % 2 == 0)
                        {
                            Rigidbody2D projectileInstance;
                            projectileInstance = Instantiate(Projectile, ShootPoint1.position, transform.rotation) as Rigidbody2D;
                            projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
                        }
                        else
                        {
                            Rigidbody2D projectileInstance;
                            projectileInstance = Instantiate(Projectile, ShootPoint2.position, transform.rotation) as Rigidbody2D;
                            projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
                        }
                        i++;
                        CoolDown = CoolDownDuration;
                    }
                    else
                    {
                        CoolDown -= Time.deltaTime;
                    }
                    if (CoolDownMainCannon < 0)
                    {
                        Rigidbody2D projectileInstance;
                        projectileInstance = Instantiate(Projectile2, ShootPoint3.position, transform.rotation) as Rigidbody2D;
                        projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
                        CoolDownMainCannon = CoolDownDurationMainCannon;
                    }
                    else
                    {
                        CoolDownMainCannon -= Time.deltaTime;
                    }
                    SWP -= Time.deltaTime;
                }
            }
            else
            {
                Flak.SetBool("Shooting", false);
            }
        }
    }
    public Transform FindClosestObject()
    {
        if (IsPlayer == true)
        {
            float DtoC = Mathf.Infinity;
            Enemy closestEnemy = null;
            Enemy[] lall = GameObject.FindObjectsOfType<Enemy>();
            foreach (Enemy curr in lall)
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestEnemy = curr;
                }
            }
            if (closestEnemy == null)
            {
                return null;
            }
            return closestEnemy.GetComponent<Transform>();
        }
        else
        {
            float DtoC = Mathf.Infinity;
            Player closestPlayer = null;
            Player[] lall = GameObject.FindObjectsOfType<Player>();
            foreach (Player curr in lall)
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestPlayer = curr;
                }
            }
            if (closestPlayer == null)
            {
                return null;
            }
            return closestPlayer.GetComponent<Transform>();
        }
    }
}

