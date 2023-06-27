using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_turrets : MonoBehaviour
{
    public GameObject duo_blueprint;
    public GameObject stalker_blueprint;
    public GameObject barrager_blueprint;
    public GameObject flaker_blueprint;
    public void spawn_duo()
    {
        Instantiate(duo_blueprint);
    }
    public void spawn_stalker()
    {
        Instantiate(stalker_blueprint);
    }
    public void spawn_barrager()
    {
        Instantiate(barrager_blueprint);
    }
    public void spawn_flaker()
    {
        Instantiate(flaker_blueprint);
    }
}
