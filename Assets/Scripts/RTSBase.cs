using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBase : MonoBehaviour
{
    public bool isPlayer;
    // Get Points Earned //
    public CapturePoint A;
    public CapturePoint B;
    public CapturePoint C;
    private bool PlayerHasA;
    private bool PlayerHasB;
    private bool PlayerHasC;
    private int GamePoints;
    private string STRING;
    private float TPE;
    private float TPS;
    private float TPA;
    private float TPB;
    private float TPC;
    // Start is called before the first frame update
    void Start()
    {
        TPE = 1f;
        TPA = TPE;
        TPB = TPE;
        TPC = TPE;
        TPS = TPE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(TPS < 0)
        {
            StartCoroutine(AddPoint());
            TPS = TPE;
        }
        else
        {
            TPS -= Time.deltaTime;
        }
        // Get Point Status //
        PlayerHasA = A.PlayerInPossession();
        PlayerHasB = B.PlayerInPossession();
        PlayerHasC = C.PlayerInPossession();
        if (isPlayer)
        {
            // For Player Base //
            if (PlayerHasA)
            {
                if(TPA < 0)
                {
                    StartCoroutine(AddPoint());
                    TPA = TPE;
                }
                else
                {
                    TPA -= Time.deltaTime;
                }
            }
            if (PlayerHasB)
            {
                if (TPB < 0)
                {
                    StartCoroutine(AddPoint());
                    TPB = TPE;
                }
                else
                {
                    TPB -= Time.deltaTime;
                }
            }
            if (PlayerHasC)
            {
                if (TPC < 0)
                {
                    StartCoroutine(AddPoint());
                    TPC = TPE;
                }
                else
                {
                    TPC -= Time.deltaTime;
                }
            }
            //Debug.Log(gameObject.tag + "" + GamePoints);
        }
        else
        {
            // For Enemy Base //
        }
    }
    public bool GetIsPlayer()
    {
        return isPlayer;
    }
    IEnumerator AddPoint()
    {
        yield return new WaitForSeconds(0.1f);
        GamePoints++;
    }
}
