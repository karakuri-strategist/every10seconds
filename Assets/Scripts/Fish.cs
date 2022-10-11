using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    private NavMeshAgent agent;
    private float timer;
    private Vector3 destination;

    public delegate void Collected();
    public event Collected FishSnack;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            destination = newPos;
            timer = 0;
        }
        agent.SetDestination(Vector3.Lerp(agent.destination, destination, 0.5f * Time.deltaTime));
        
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized) * Quaternion.AngleAxis(90, Vector3.up);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere.normalized * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FishSnack.Invoke();
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
