using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGroup : MonoBehaviour
{
    public int TeamNum;
    // Start is called before the first frame update
    public int GetTeamNum()
    {
        return TeamNum;
    }
    public void SetTeamNum(int num)
    {
        TeamNum = num;
    }
}
