using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private int BelongToGroup;
    public void SetToGroup(int num)
    {
        BelongToGroup = num;
    }
    public int GetGroupNum()
    {
        return BelongToGroup;
    }
}
