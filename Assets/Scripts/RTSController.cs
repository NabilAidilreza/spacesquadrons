using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    // Handles all player commands for gameplay //
    public GameObject CursorItem;
    public GameObject AttackObject;

    // RTS Elements //
    private Vector3 startPosition;
    private RTSGroup[] RTSGroups;
    private EnemyGroup[] EnemyGroups;
    public List<RTSUnit> selectedUnitRTSList = new List<RTSUnit>();

    // Controls Variables //
    private bool altToggle = false;

    /// <summary>
    /// Idle: 0
    /// Seek : 1
    /// Patrol : 2
    /// Attack : 3
    /// </summary>

    private void Awake()
    {
        // Indexing for Dev //
        RTSGroups = GameObject.FindObjectsOfType<RTSGroup>();
        for(int i = 0;i < RTSGroups.Length; i++)
        {
            RTSGroups[i].SetTeamNum(i);
        }
        EnemyGroups = GameObject.FindObjectsOfType<EnemyGroup>();
        for (int i = 0; i < EnemyGroups.Length; i++)
        {
            EnemyGroups[i].SetTeamNum(i);
        }
    }
    private void Update()
    {
        // Selection Of Units //
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // Deselect all groups // 
            foreach(RTSUnit unit in selectedUnitRTSList)
            {
                if(unit != null)
                {
                    unit.SetSelectedVisible(false);
                }
            }
            selectedUnitRTSList.Clear();
            // Select all units in selection area //
            foreach (Collider2D collider2D in collider2DArray)
            {
                RTSUnit unit = collider2D.GetComponent<RTSUnit>();
                if(unit != null)
                {
                    int GroupNum = unit.GetGroupNum();
                    RTSUnit[] Units = GameObject.FindObjectsOfType<RTSUnit>();
                    foreach(RTSUnit UNIT in Units)
                    {
                        if(UNIT.GetGroupNum() == GroupNum)
                        {
                            UNIT.SetSelectedVisible(true);
                            selectedUnitRTSList.Add(UNIT);
                        }
                    }
                }
            }
        }
        // If ALT is pressed, display HP Bar //
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            altToggle = !altToggle;
        }
        CheckAlt();

        // Seeking and Attacking System //
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 CheckPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckPoint.z = 0;
            Collider2D COLLIDER = Physics2D.OverlapCircle(CheckPoint, 2f);
            // If space is empty //
            if (selectedUnitRTSList.Count != 0 )
            {
                if(COLLIDER == null)
                {
                    SeekArea();
                }
                else
                {
                    // Check if target is building or unit //
                    // If target is unit, break off for firefight //
                    // If target is building, spread out and hit and run //
                    GameObject EnemyTarget = COLLIDER.GetComponent<Transform>().gameObject;
                    AttackTarget(EnemyTarget);
                }
            }
        }

    }
    private void AttackTarget(GameObject Target)
    {
        // Find all leaders //
        SquadronLeader[] Leaders = GameObject.FindObjectsOfType<SquadronLeader>();
        // For Loop Leaders //
        foreach (SquadronLeader Leader in Leaders)
        {
            // Get Leader Num //
            int LeaderGroupNum = Leader.GetComponent<RTSGroup>().GetTeamNum();
            // Get Leader Component //
            SquadronLeader LEADER = Leader.GetComponent<SquadronLeader>();
            // For Loop Units //
            foreach (RTSUnit unit in selectedUnitRTSList)
            {
                // If  leader num = unit num //
                if (unit != null)
                {
                    if (LeaderGroupNum == unit.GetGroupNum())
                    {
                        // set leader target to target //
                        LEADER.SetTarget(Target);
                    }
                }
            }
        }
    }
    private void SeekArea()
    {
        // spawn object to space //
        Vector3 PosRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PosRelative.z = 0;
        GameObject CURSOR = Instantiate(CursorItem, PosRelative, Quaternion.identity);
        // Find all leaders //
        SquadronLeader[] Leaders = GameObject.FindObjectsOfType<SquadronLeader>();
        // For Loop Leaders //
        foreach (SquadronLeader Leader in Leaders)
        {
            // Get Leader Num //
            int LeaderGroupNum = Leader.GetComponent<RTSGroup>().GetTeamNum();
            // Get Leader Component //
            SquadronLeader LEADER = Leader.GetComponent<SquadronLeader>();
            // For Loop Units //
            foreach (RTSUnit unit in selectedUnitRTSList)
            {
                // If  leader num = unit num //
                if (unit != null)
                {
                    if (LeaderGroupNum == unit.GetGroupNum())
                    {
                        // set leader target to empty space //
                        GameObject parent = LEADER.gameObject.transform.parent.parent.gameObject;
                        RTSMousePoint MP = parent.GetComponentInChildren<RTSMousePoint>();
                        MP.SetReady(true);
                        LEADER.SetTarget(MP.gameObject);
                    }
                }
            }
        }
    }
    // Toggle Alt //
    private void CheckAlt(){
        if(altToggle){
            RTSUnit[] Units = GameObject.FindObjectsOfType<RTSUnit>();
            foreach (RTSUnit Unit in Units)
            {
                Unit.SetHPVisible(true);
            }
        }
        else{
            RTSUnit[] Units = GameObject.FindObjectsOfType<RTSUnit>();
            foreach (RTSUnit Unit in Units)
            {
                Unit.SetHPVisible(false);
            }
        }
    }
}
