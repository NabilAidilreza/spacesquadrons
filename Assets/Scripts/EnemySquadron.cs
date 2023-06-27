using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquadron : MonoBehaviour
{
    public GameObject target;
    public GameObject Ship;
    public int SquadSize;
    public List<GameObject> ShipLst;
    public float speed;
    private float maxSpeed;
    public GameObject PatrolPoint;
    public bool IsPatrol;
    private int TeamNum;
    public List<EnemyUnit> OwnUnits;
    public float ReplenishTime;
    private float RT;
    private bool manualAttack = false;
    private bool AllowIndividual = false;
    /// <summary>
    /// Idle : 0
    /// Patrol : 1
    /// Seek : 2
    /// Flee : 3
    /// Attack : 4
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        RT = ReplenishTime;
        IsPatrol = false;
        TeamNum = gameObject.GetComponent<EnemyGroup>().GetTeamNum();
        ShipLst = new List<GameObject>();
        for (int i = 0; i < SquadSize; i++)
        {
            Vector3 rel_spawn = new Vector3(i % 4, i / 4, 0);
            GameObject temp = Instantiate(Ship, transform.position + (rel_spawn * 6.0f), transform.rotation);
            temp.GetComponent<Enemy_Behaviour>().target = gameObject;
            temp.GetComponent<Enemy_Behaviour>().SetSpeed(speed - 2);
            temp.GetComponent<EnemyUnit>().SetToGroup(TeamNum);
            ShipLst.Add(temp);
        }
        // Add Units to Group //
        EnemyUnit[] UNITS = GameObject.FindObjectsOfType<EnemyUnit>();
        OwnUnits = new List<EnemyUnit>();
        // Find all relative ships
        foreach (EnemyUnit UNIT in UNITS)
        {
            if (UNIT.GetGroupNum() == TeamNum)
            {
                if (UNIT != null)
                {
                    OwnUnits.Add(UNIT);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShipLst.RemoveAll(GameObject => GameObject == null);
        OwnUnits.RemoveAll(GameObject => GameObject == null);
        if (AllowIndividual == false)
        {
            // Set Patrol //
            if (target == null)
            {
                // Find all relative ships
                foreach (EnemyUnit UNIT in OwnUnits)
                {
                    if (UNIT != null)
                    {
                        Enemy_Behaviour BB = UNIT.GetComponent<Enemy_Behaviour>();
                        BB.SetSpeed(10f);
                        BB.changeStateNum(2);
                        // Change to Seek //
                        if (UNIT != null)
                        {
                            StartCoroutine(Wait(UNIT));
                        }
                    }
                }
                target = PatrolPoint;
                manualAttack = false;
            }

            // Approach Non-Enemy //
            else if (target != null && target != PatrolPoint)
            {
                // Find all relative ships
                foreach (EnemyUnit UNIT in OwnUnits)
                {
                    if (UNIT != null)
                    {
                        Enemy_Behaviour BB = UNIT.GetComponent<Enemy_Behaviour>();
                        BB.SetSpeed(speed - 2);
                    }
                }
            }
            // Move to enemy(auto) //
            if (target != null && target.transform.tag == "Player")
            {
                foreach (EnemyUnit UNIT in OwnUnits)
                {
                    if (UNIT != null)
                    {
                        Enemy_Behaviour BB = UNIT.GetComponent<Enemy_Behaviour>();
                        BB.changeStateNum(3);
                    }
                }
            }
            // Move to patrol //
            if (target != null)
            {
                transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * speed;
            }
        }
        else
        {
            // Units break off and reference point stay at current location //
            transform.position = this.transform.position;
        }

        // Destroy if no units left //
        if (OwnUnits.Count == 0)
        {
            Destroy(gameObject);
        }

    }
    public void SetTarget(GameObject TARGET)
    {
        target = TARGET;
    }
    public GameObject GetTarget()
    {
        return target;
    }
    public void SetPatrol(bool choice)
    {
        IsPatrol = choice;
    }
    public void ManualAttack(bool choice)
    {
        manualAttack = choice;
    }
    public bool GetManualAttack()
    {
        return manualAttack;
    }
    IEnumerator Wait(EnemyUnit UNIT)
    {
        yield return new WaitForSeconds(5f);
        if (UNIT != null)
        {
            // AutoShoot On //
            Enemy_Shoot ShootScript = UNIT.GetComponent<Enemy_Shoot>();
            ShootScript.enabled = true;
        }
    }
}