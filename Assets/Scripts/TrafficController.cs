using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;
    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;
    public GameObject stopC1;
    public GameObject stopC2;
    public GameObject stopC3;
    public GameObject goC1;
    public GameObject goC2;
    public GameObject goC3;
    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");
        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;
        stopC1 = t1.Find("stopCube").gameObject;
        stopC2 = t2.Find("stopCube").gameObject;
        stopC3 = t3.Find("stopCube").gameObject;
        goC1 = t1.Find("goCube").gameObject;
        goC2 = t2.Find("goCube").gameObject;
        goC3 = t3.Find("goCube").gameObject;
        stateTimer = 10.0f;
        SetState(1);

    }

    // Update is called once per frame
    void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0.0f)
        {
            if(state == 1)
            {
                SetState(2);
                stateTimer = 10.0f;
            }
            else
            {
                SetState(1);
                stateTimer = 10.0f;
            }
        }

    }
    void SetState(int c)
    {
        state = c;
        if (c == 1)
        {
            t1green.active = true;
            t1red.active = false;
            stopC1.active = false;
            goC1.active = true;
            t2green.active = false;
            t2red.active = true;
            stopC2.active = true;
            goC2.active = false;
            t3green.active = false;
            t3red.active = true;
            stopC3.active = true;
            goC3.active = false;
        }
        else
        {
            t1green.active = false;
            t1red.active = true;
            stopC1.active = true;
            goC1.active = false;
            t2green.active = true;
            t2red.active = false;
            stopC2.active = false;
            goC2.active = true;
            t3green.active = true;
            t3red.active = false;
            stopC3.active = false;
            goC3.active = true;
        }
    }
}
