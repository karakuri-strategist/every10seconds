using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinel : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Toggle());
    }

    // Update is called once per frame
    void Update()
    {
        if(on)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, player.position - transform.position, out hit))
            {
                if(hit.collider.tag == "Player")
                {
                    Debug.Log("Gotcha!");
                }
            }
        }
    }

    IEnumerator Toggle()
    {
        on = !on;
        yield return new WaitForSeconds(10);
        StartCoroutine(Toggle());
    }
}
