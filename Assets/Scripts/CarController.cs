using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<Transform> cwps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetCWP = 0;
    public float dist;
    public Rigidbody rb;
    public bool go = false;
    public float initialDelay;


    public float maxSpeed;
    public float timeToMaxS = 2.5f;
    float acceleration = 0.0f;
    public float forwardSpeed;
    public bool collided = false;
    public bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        acceleration = maxSpeed / timeToMaxS;

        cwps = new List<Transform>();
        GameObject cwp;
        rb = GetComponent<Rigidbody>();

        cwp = GameObject.Find("CWP1"); //0
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP2"); //1
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP3"); //2
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP4");//3
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP5");//4
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP6");//5
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP7");//6
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP8");//7
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP9");//8
        cwps.Add(cwp.transform);
        cwp = GameObject.Find("CWP10");//9
        cwps.Add(cwp.transform);
        SetRoute();
        initialDelay = Random.Range(2.0f, 12.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetCWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (dist < 0.1f)
        {
            targetCWP++;
            if (targetCWP >= route.Count)
            {
                SetRoute();
                forwardSpeed = 0f;
                return;
            }
        }


        //calculate velocity for this frame

        if(collided != true & stop !=true)
        {
            //forwardSpeed += acceleration * Time.deltaTime;
            if (forwardSpeed >= maxSpeed)
            {
                forwardSpeed = maxSpeed;
            }
            else 
            {
                forwardSpeed += acceleration * Time.deltaTime;
            }
            //forwardSpeed = Mathf.Min(forwardSpeed, maxSpeed);
        }
        else
        {
            forwardSpeed = 0f;
        }

        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= forwardSpeed;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity* Time.deltaTime;
        rb.MovePosition(newPosition);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);

    }
    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 9);
        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { cwps[0], cwps[1]};
        else if (routeNumber == 1) route = new List<Transform> { cwps[2], cwps[3]};
        else if (routeNumber == 2) route = new List<Transform> { cwps[2], cwps[3], cwps[0], cwps[1]};
        else if (routeNumber == 3) route = new List<Transform> { cwps[0], cwps[7], cwps[9]};
        else if (routeNumber == 4) route = new List<Transform> { cwps[8], cwps[4], cwps[1]};
        else if (routeNumber == 5) route = new List<Transform> { cwps[2], cwps[6], cwps[9]};
        else if (routeNumber == 6) route = new List<Transform> { cwps[8], cwps[5], cwps[3]};
        else if (routeNumber == 7) route = new List<Transform> { cwps[0], cwps[1], cwps[2], cwps[6], cwps[9]};
        else if (routeNumber == 8) route = new List<Transform> { cwps[0], cwps[7], cwps[9], cwps[8], cwps[4], cwps[1]};

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.5f,route[0].position.z);
        targetCWP = 1;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pedestrian")
        {
            Debug.Log("collided");
            collided = true;
        }
        else if(other.gameObject.tag == "stop")
        {
            stop = true;
        }
        else if (other.gameObject.tag == "go")
        {
            stop = false;
        }
        else if (other.gameObject.tag == "vehicle")
        {
            StartCoroutine(wait());
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        collided = false;
    }

    IEnumerator wait()
    {
        collided = true;
        yield return new WaitForSeconds(5);
        collided = false;
    }
}