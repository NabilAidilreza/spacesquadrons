using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public string status;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public string GetStatus()
    {
        return status;
    }
    public void UpdateTurretStatus(string NewStatus)
    {
        status = NewStatus;
    }
}
