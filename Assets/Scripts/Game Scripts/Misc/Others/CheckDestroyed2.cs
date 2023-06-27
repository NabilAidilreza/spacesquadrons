using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDestroyed2 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.GetChild(0).GetComponent<EnemySquadron>() == null)
        {
            Destroy(gameObject);
        }
    }
}
