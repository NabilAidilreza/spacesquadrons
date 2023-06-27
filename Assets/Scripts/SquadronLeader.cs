using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadronLeader : MonoBehaviour
{
    public GameObject target;
    public GameObject Ship;
    public int SquadSize;
    public List<GameObject> ShipLst;
    public float speed;
    public GameObject PatrolPoint;
    private int TeamNum;
    public List<RTSUnit> OwnUnits;
    private float currSpeed;

    // Attack Variables //
    private string squadState = "Patrolling...";
    private bool isEnemySquad = false;

    // Waypoint Variables //
    public GameObject SquadWaypoint;
    // Regeneration Variables //
    private float RT;
    private float ReplenishTime = 20f;

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
        // Upon intialization, create a list of all the units in the squad //
        target = gameObject;
        TeamNum = gameObject.GetComponent<RTSGroup>().GetTeamNum();
        ShipLst = new List<GameObject>();
        for (int i = 0; i < SquadSize; i++)
        {
            Vector3 rel_spawn = new Vector3(i % 5,i / 5, 0);
            GameObject temp = Instantiate(Ship, transform.position + (rel_spawn * 6.0f), transform.rotation);
            temp.GetComponent<base_behaviour>().squadron = gameObject;
            temp.GetComponent<base_behaviour>().target = gameObject;
            currSpeed = speed / 2;
            temp.GetComponent<base_behaviour>().SetSpeed(currSpeed);
            temp.GetComponent<RTSUnit>().SetToGroup(TeamNum);
            ShipLst.Add(temp);
        }
        // Add all the units to the squad //
        RTSUnit[] UNITS = GameObject.FindObjectsOfType<RTSUnit>();
        OwnUnits = new List<RTSUnit>();
        foreach (RTSUnit UNIT in UNITS)
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
        // Update respective lsts to remove dead units //
        ShipLst.RemoveAll(GameObject => GameObject == null);
        OwnUnits.RemoveAll(GameObject => GameObject == null);
        
        // Allow regeneration over time //
        //Regen();

        // Update the squad state //
        if(target != null){
            // Patrol upon initialisation //
            if(target.name == gameObject.name){
                // Update the squad state to PATROL//
                target = PatrolPoint;
                currSpeed = speed/2;
                SetSettingsToUnits(2,currSpeed,target);
                squadState = "Patrolling...";
            }
            if (target != PatrolPoint)
            {
                if(target.gameObject.tag == "Enemy")
                {
                    // Check what the enemy target is //
                    Unit UnitComponent = target.GetComponent<Unit>();
                    if(UnitComponent || isEnemySquad == true){
                        if(Vector3.Distance(this.gameObject.transform.position,target.transform.position)<=50f)
                        {
                            // Set target to parent //
                            // Enemy is part of enemy squadron //
                            // Get squadron info //
                            // Set targets from enemy squad to every own units //
                            // Get enemy grp num //
                            int EnemyGrpNum = target.GetComponent<EnemyUnit>().GetGroupNum();
                            List<GameObject> EnemyShipLst = new List<GameObject>();
                            // Find all enemy leaders //
                            EnemySquadron[] Leaders = GameObject.FindObjectsOfType<EnemySquadron>();
                            // For Loop Leaders //
                            foreach (EnemySquadron Leader in Leaders)
                            {
                                // Get Leader Num //
                                int LeaderGroupNum = Leader.GetComponent<EnemyGroup>().GetTeamNum();
                                // Get Leader Component //
                                EnemySquadron LEADER = Leader.GetComponent<EnemySquadron>();
                                if (LeaderGroupNum == EnemyGrpNum)
                                {
                                    EnemyShipLst = LEADER.ShipLst;
                                    target = Leader.gameObject;
                                    isEnemySquad = true;
                                }
                            }
                            EnemySquadron TEST = target.GetComponent<EnemySquadron>();
                            if(TEST){
                                if(TEST.ShipLst.Count <= 0){
                                    target = null;
                                }
                            }
                            foreach (RTSUnit UNIT in OwnUnits)
                            {
                                if (UNIT != null)
                                {
                                    base_behaviour bb = UNIT.GetComponent<base_behaviour>();
                                    bb.enabled = false;
                                    Agent agent = UNIT.GetComponent<Agent>();
                                    if(agent){
                                        agent.enabled = false;
                                    }
                                    BoidCohesion boidcoh = UNIT.GetComponent<BoidCohesion>();
                                    if(boidcoh){
                                        boidcoh.enabled = false;
                                    }
                                    BoidSeparation boidsep = UNIT.GetComponent<BoidSeparation>();
                                    if(boidsep){
                                        boidsep.enabled = false;
                                    }
                                    attack_script attackScriptManager = UNIT.GetComponent<attack_script>();
                                    if(attackScriptManager){
                                        attackScriptManager.enabled = false;
                                    }
                                    Attack attackScript = UNIT.GetComponent<Attack>();
                                    if(attackScript){
                                        attackScript.enabled = false;
                                    }
                                    Player_FighterMode PFM = UNIT.GetComponent<Player_FighterMode>();
                                    PFM.enabled = true;
                                    int RANDOM = Random.Range(0,EnemyShipLst.Count);
                                    GameObject currTarget = EnemyShipLst[RANDOM];
                                    if(currTarget != null){
                                        PFM.SetEnemy(currTarget);
                                    }
                                    
                                }
                            }
                        }
                        else{
                            currSpeed = speed - 3f;
                            SetSettingsToUnits(3,currSpeed,target);
                        }
                    }
                    else{
                        // Enemy is not a enemy squad -> turret / building //
                        if(Vector3.Distance(this.gameObject.transform.position,target.transform.position)<=50f)
                        {
                            // If target in range, switch all units to fighter mode //
                            UnitsSwitchToFighterMode(true);
                        }
                        else{
                            // If target not in range, switch all units to strafe mode //
                            currSpeed = speed - 3f;
                            SetSettingsToUnits(3,currSpeed,target);
                        }
                    }
                    squadState = "Attacking...";
                    
                }
                else
                {
                    // If target is obstacle //
                    if(target.gameObject.tag == "Object"){
                        // Do nothing //
                    }
                    else{
                        // Disable units if still in fighter mode //
                        UnitsSwitchToFighterMode(false);
                        // Update the squad state to SEEK //
                        currSpeed = speed - 2f;
                        SetSettingsToUnits(1,currSpeed,target);
                        // Check if squad at waypoint //
                        if(target==SquadWaypoint){
                            // If waypoint reached //
                            if(Vector3.Distance(this.gameObject.transform.position,SquadWaypoint.transform.position)< 5f){
                                // Switch to Patrol //
                                target = null;
                            }
                        }
                        squadState = "Seeking...";
                    }
                }
            }
            if(target != null){
                // Adjust parent coordinate to game coordinates //
                transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * (currSpeed+2f);
            }
        }
        else
        {
            // Patrol if target == null //
            target = PatrolPoint;
            currSpeed = speed/2;
            // If target was enemy and was destroyed, swap back to normal mode //
            UnitsSwitchToFighterMode(false);
            SetSettingsToUnits(2,currSpeed,target);
            squadState = "Patrolling...";
        }

        // Destroy if no units left //
        if (OwnUnits.Count == 0)
        {
            Destroy(gameObject);
        }

    }
    // Target Functions //
    public void SetTarget(GameObject TARGET)
    {
        target = TARGET;
    }
    public GameObject GetTarget()
    {
        return target;
    }

    IEnumerator Wait(RTSUnit UNIT)
    {
        yield return new WaitForSeconds(0.3f);
    }

    // Fighter Script Function //
    void UnitsSwitchToFighterMode(bool VALUE){
        // Switch to fighter mode //
        // Kill all other scripts //
        if(VALUE)
        {
            foreach (RTSUnit UNIT in OwnUnits)
            {
                if (UNIT != null)
                {
                    base_behaviour bb = UNIT.GetComponent<base_behaviour>();
                    bb.enabled = false;
                    Agent agent = UNIT.GetComponent<Agent>();
                    if(agent){
                        agent.enabled = false;
                    }
                    BoidCohesion boidcoh = UNIT.GetComponent<BoidCohesion>();
                    if(boidcoh){
                        boidcoh.enabled = false;
                    }
                    BoidSeparation boidsep = UNIT.GetComponent<BoidSeparation>();
                    if(boidsep){
                        boidsep.enabled = false;
                    }
                    attack_script attackScriptManager = UNIT.GetComponent<attack_script>();
                    if(attackScriptManager){
                        attackScriptManager.enabled = false;
                    }
                    Attack attackScript = UNIT.GetComponent<Attack>();
                    if(attackScript){
                        attackScript.enabled = false;
                    }
                    patrol_script patrolScriptManager = UNIT.GetComponent<patrol_script>();
                    if(patrolScriptManager){
                        patrolScriptManager.enabled = false;
                    }
                    Patrol patrolScript = UNIT.GetComponent<Patrol>();
                    if(patrolScript){
                        patrolScript.enabled = false;
                    }
                    seek_script seekScriptManager = UNIT.GetComponent<seek_script>();
                    if(seekScriptManager){
                        seekScriptManager.enabled = false;
                    }
                    Seek seekScript = UNIT.GetComponent<Seek>();
                    if(seekScript){
                        seekScript.enabled = false;
                    }
                    Player_FighterMode PFM = UNIT.GetComponent<Player_FighterMode>();
                    PFM.enabled = true;
                    PFM.SetEnemy(target);
                }
            }
        }
        else{
            foreach (RTSUnit UNIT in OwnUnits)
            {
                if(UNIT!= null)
                {
                    if(UNIT.GetComponent<Player_FighterMode>().enabled == true /*& UNIT.GetComponent<Player_FighterMode>().GetEnemy() == true*/)
                    {
                        base_behaviour bb = UNIT.GetComponent<base_behaviour>();
                        bb.enabled = true;
                        Agent agent = UNIT.GetComponent<Agent>();
                        if(agent){
                            agent.enabled = true;
                        }
                        BoidCohesion boidcoh = UNIT.GetComponent<BoidCohesion>();
                        if(boidcoh){
                            boidcoh.enabled = true;
                        }
                        BoidSeparation boidsep = UNIT.GetComponent<BoidSeparation>();
                        if(boidsep){
                            boidsep.enabled = true;
                        }
                        attack_script attackScriptManager = UNIT.GetComponent<attack_script>();
                        if(attackScriptManager){
                            attackScriptManager.enabled = true;
                        }
                        Attack attackScript = UNIT.GetComponent<Attack>();
                        if(attackScript){
                            attackScript.enabled = true;
                        }
                        patrol_script patrolScriptManager = UNIT.GetComponent<patrol_script>();
                        if(patrolScriptManager){
                            patrolScriptManager.enabled = true;
                        }
                        Patrol patrolScript = UNIT.GetComponent<Patrol>();
                        if(patrolScript){
                            patrolScript.enabled = true;
                        }
                        seek_script seekScriptManager = UNIT.GetComponent<seek_script>();
                        if(seekScriptManager){
                            seekScriptManager.enabled = true;
                        }
                        Seek seekScript = UNIT.GetComponent<Seek>();
                        if(seekScript){
                            seekScript.enabled = true;
                        }
                        Player_FighterMode PFM = UNIT.GetComponent<Player_FighterMode>();
                        PFM.enabled = false;
                    }
                }
            }
        }
    }
    // Settings Toggle Function //
    void SetSettingsToUnits(int STATE, float SPEED, GameObject TARGET){
        // Set all ships to correct settings //
        foreach (RTSUnit UNIT in OwnUnits)
        {
            if (UNIT != null)
            {
                base_behaviour BB = UNIT.GetComponent<base_behaviour>();
                if(STATE != -1)
                {
                    if(STATE == 1){
                        squadState = "Seeking...";
                    }
                    else if(STATE == 2){
                        squadState = "Patrolling...";
                    }
                    else if(STATE == 3){
                        squadState = "Attacking...";
                    }
                    BB.changeStateNum(STATE);
                }
                if(SPEED != 0)
                {
                    BB.SetSpeed(SPEED);
                }
                if(TARGET != null)
                {
                    BB.SetTarget(TARGET);
                }
                // Change to Seek //
                if (UNIT != null)
                {
                    StartCoroutine(Wait(UNIT));
                }
            }
        }
    }
    // Regeneration Function //
    void Regen(){
        if (OwnUnits.Count < SquadSize)
        {
            if(RT <= 0)
            {
                Vector3 rel_spawn = new Vector3(0, 0, 0);
                GameObject temp = Instantiate(Ship, transform.position + (rel_spawn * 6.0f), transform.rotation);
                temp.GetComponent<base_behaviour>().target = gameObject;
                temp.GetComponent<base_behaviour>().SetSpeed(speed - 2);
                temp.GetComponent<RTSUnit>().SetToGroup(TeamNum);
                ShipLst.Add(temp);
                OwnUnits.Add(temp.GetComponent<RTSUnit>());
                RT = ReplenishTime;
            }
            else
            {
                RT -= Time.deltaTime;
            }
        }
    }
}
