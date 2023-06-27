using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public Rigidbody2D Projectile;

    public float DetectionRange; // If enemy detected of squad range
    public float EngageRange; // If enemy in range of firing 
    public float CollisionRange; // If enemy about ot collide
    public LayerMask whatisEnemies;
    private GameObject TARGET;
    public float ShootRate;
    private float SR;
    public Transform ShootPoint1;
    public Transform ShootPoint2;
    private float ProjectileSpeed = 100f;
    private bool StopShooting = true;
    private EnemySquadron LEADER;
    private Enemy_Behaviour BB;
    public bool isHoming;
    private int i = 0;
    private List<Transform> EnemiesInSight;
    private GameObject CURR_TARGET;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        SR = ShootRate;
        EnemySquadron[] Leaders = GameObject.FindObjectsOfType<EnemySquadron>();
        foreach (EnemySquadron Leader in Leaders)
        {
            // Get Leader Num //
            int LeaderGroupNum = Leader.GetComponent<EnemyGroup>().GetTeamNum();
            // Get RTS Unit Num //
            float GroupNum = GetComponent<EnemyUnit>().GetGroupNum();
            // Same Group //
            if (LeaderGroupNum == GroupNum)
            {
                // Get Leader Component //
                LEADER = Leader.GetComponent<EnemySquadron>();
            }
        }
        // List for spotted enemies in range //
        EnemiesInSight = new List<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Priority --> Units Over Turret //
        EnemiesInSight.RemoveAll(GameObject => GameObject == null);
        Collider2D[] EnemiesInDetection = Physics2D.OverlapCircleAll(transform.position, DetectionRange, whatisEnemies);
        Collider2D[] EnemiesInRange = Physics2D.OverlapCircleAll(transform.position, EngageRange, whatisEnemies);
        Collider2D[] EnemiesToAvoid = Physics2D.OverlapCircleAll(transform.position, CollisionRange, whatisEnemies);
        if (EnemiesInDetection.Length > 0)
        {
            // Set Target To Enemy Unit
            StopShooting = false;
            // Add spotted enemies to list //
            foreach (Collider2D Enemycollider in EnemiesInDetection)
            {

                if (Enemycollider != null)
                {
                    Transform ENEMY = Enemycollider.GetComponent<Transform>();
                    EnemiesInSight.Add(ENEMY);
                }
            }
            if (EnemiesInSight.Count > 1)
            {
                        // Allow group to attack random enemy in vicinity //
                LEADER.SetTarget(EnemiesInSight[Random.Range(0, EnemiesInSight.Count)].gameObject);
            }
        }
        if (EnemiesInRange.Length > 0 && StopShooting == false)
        {
            TARGET = LEADER.GetTarget();
            if ((TARGET != null &&  TARGET.transform.tag == "Player"))
            {
                Vector3 relativeTarget = (TARGET.transform.position - transform.position).normalized;
                //Vector3.right if you have a sprite rotated in the right direction
                Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relativeTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 90f * Time.deltaTime);
                // Start Shooting
                if (SR < 0)
                {
                    if (isHoming == true)
                    {
                        Instantiate(Projectile, ShootPoint1.position, transform.rotation);
                        Instantiate(Projectile, ShootPoint2.position, transform.rotation);
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            Rigidbody2D projectileInstance;
                            projectileInstance = Instantiate(Projectile, ShootPoint1.position, transform.rotation) as Rigidbody2D;
                            projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * ProjectileSpeed;
                        }
                        else
                        {
                            Rigidbody2D projectileInstance;
                            projectileInstance = Instantiate(Projectile, ShootPoint2.position, transform.rotation) as Rigidbody2D;
                            projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * ProjectileSpeed;
                        }
                    }
                    i++;
                    SR = ShootRate;
                }
                else
                {
                    SR -= Time.deltaTime;
                }
            }
        }
        if (EnemiesToAvoid.Length > 0)
        {
            // Avoid By Retreating
            LEADER.SetTarget(null);
            StopShooting = true;
            // Attack Run Finished //
            this.enabled = false;
        }
    }
}