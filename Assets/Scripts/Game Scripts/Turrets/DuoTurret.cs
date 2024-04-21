using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoTurret : Turret
{
    public bool IsPlayer;
    public Rigidbody2D Projectile;
    public float EngageRange;
    public Transform ShootPoint1;
    public Transform ShootPoint2;
    public float CoolDownDuration;
    private float CoolDown;
    private Transform target;
    public float offset;
    private int i;
    private float projectileSpeed = 100f;
    private Animator Duo;
    public float ShootWindowPeriod;
    private float SWP;
    public float OverHeatDuration;
    public GameObject OH_Effect;
    private GameObject OH;
    public GameObject ShockEffect;
    private GameObject Shock;
    [SerializeField] private AudioClip gunSound;

    // Start is called before the first frame update
    void Start()
    {
        Duo = gameObject.GetComponent<Animator>();
        CoolDown = CoolDownDuration;
        SWP = ShootWindowPeriod;
        i = 0;
        status = "Online";
    }

    // Update is called once per frame
    void Update()
    {
        if(status == "Online")
        {
            MainFunc();
        }
        else if(status == "Offline")
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
        Duo.SetBool("Shooting", false);
        StartCoroutine("Recover");
    }
    public void MainFunc()
    {
        target = FindClosestObject();
        if (target == null)
        {
            Duo.SetBool("Shooting", false);
        }
        else
        { 
            Vector3 a = target.transform.up * 5f;
            Vector3 relativeTarget = (target.transform.position - transform.position + a).normalized;
            Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relativeTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 8f * Time.deltaTime);
            float Range = Vector2.Distance(transform.position, target.transform.position);
            if (Range < EngageRange)
            {
                if (SWP < 0f)
                {
                    Duo.SetBool("Shooting", false);
                    OH = Instantiate(OH_Effect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
                    StartCoroutine("Overheat");
                    SWP = 0f;
                }
                else if (SWP > 0f)
                {
                    Duo.SetBool("Shooting", true);
                    if (CoolDown <= 0)
                    {
                        SoundFXManager.instance.PlaySoundFXClip(gunSound, transform, 0.3f);
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
                    SWP -= Time.deltaTime;
                }
            }
            else
            {
                Duo.SetBool("Shooting", false);
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
            if(closestEnemy == null)
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
