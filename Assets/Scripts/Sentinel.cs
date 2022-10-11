using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinel : MonoBehaviour
{
    public float rotateSpeed = 3;
    public float chaseRotateSpeed = 6;
    [SerializeField]
    public Transform player;
    public Light spotlight;
    public Material lightBulbGlow;
    public Material lightBulbOff;
    public Renderer lightBulb;
    private bool on = true;
    private int rotateDirection;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Toggle());
        rotateDirection = Random.Range(-1, 1) < 0 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(on)
        {
            var targetDireciton = player.position - transform.position;
            float angle = Vector3.SignedAngle(targetDireciton, transform.forward, Vector3.up);
            if(Mathf.Abs(angle) < spotlight.spotAngle && targetDireciton.magnitude < spotlight.range)
            {
                Debug.Log(angle);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.position - transform.position, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        transform.Rotate(Vector3.up * Time.deltaTime * -1 * Mathf.Sign(angle) * chaseRotateSpeed, Space.World);
                        Debug.Log("Gotcha!");
                    } else
                    {
                        transform.Rotate(Vector3.up * Time.deltaTime * rotateDirection * rotateSpeed, Space.World);
                    }
                }
            } else
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotateDirection * rotateSpeed, Space.World);

            }
        }
    }

    IEnumerator Toggle()
    {
        yield return new WaitForSeconds(10);
        on = !on;
        spotlight.enabled = on;
        lightBulb.sharedMaterial = on ? lightBulbGlow : lightBulbOff;
        StartCoroutine(Toggle());
    }
}
