using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_units : MonoBehaviour
{
    public GameObject MainLayer;
    public GameObject swarmer;
    public GameObject quadron;
    public GameObject deviron;
    public GameObject disabler;
    private int i = 0;
    public void spawn_swarmer()
    {
        i += 1;
        GameObject S = Instantiate(swarmer, new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
        S.GetComponent<ProgressBar>().SetIndex(i);
        MainLayer.SetActive(false);
    }
    public void spawn_quadron()
    {
        i += 1;
        GameObject S = Instantiate(quadron, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        S.GetComponent<ProgressBar>().SetIndex(i);
        MainLayer.SetActive(false);
    }
    public void spawn_deviron()
    {
        i += 1;
        GameObject S = Instantiate(deviron, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        S.GetComponent<ProgressBar>().SetIndex(i);
        MainLayer.SetActive(false);
    }
    public void spawn_disabler()
    {
        i += 1;
        GameObject S = Instantiate(disabler, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        S.GetComponent<ProgressBar>().SetIndex(i);
        MainLayer.SetActive(false);
    }
}
