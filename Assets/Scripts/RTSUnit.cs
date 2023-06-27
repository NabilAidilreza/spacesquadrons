using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnit : MonoBehaviour
{
    private GameObject selectedGameObject;
    private GameObject HPBar;
    private int BelongToGroup;
    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        HPBar = transform.Find("HealthBar").gameObject;
        SetSelectedVisible(false);
        SetHPVisible(false);
    }
    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
    public void SetToGroup(int num)
    {
        BelongToGroup = num;
    }
    public int GetGroupNum()
    {
        return BelongToGroup;
    }

    public void SetHPVisible(bool visible){
        HPBar.SetActive(visible);
    }
}
