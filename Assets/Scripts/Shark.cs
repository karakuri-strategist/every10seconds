using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public float wanderSpeed = 3.5f;
    public float chaseSpeed = 7f;
    public Transform player;

    private NavMeshAgent agent;
    private float timer;
    private Vector3 destination;
    private bool alerted = false;

    public delegate void Chomp();
    public event Chomp ChompedYa;

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

        if(alerted)
        {
            agent.SetDestination(player.position);
            agent.speed = chaseSpeed;
        } else
        {
            agent.speed = wanderSpeed;
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                destination = newPos;
                timer = 0;
            }
            agent.SetDestination(Vector3.Lerp(agent.destination, destination, 0.5f * Time.deltaTime));
        }
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

    public void OnSpotted(bool spotted)
    {
        alerted = spotted;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ChompedYa.Invoke();
        }
    }
}
