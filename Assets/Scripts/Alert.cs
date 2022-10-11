using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    // A hub for alerts from the sentinels to the sharks.
    public delegate void Spotted(bool spotted);
    public event Spotted PlayerSpotted;
    public Sentinel[] sentinels;
    public Shark[] sharks;

    private HashSet<Sentinel> spottedSentinels = new HashSet<Sentinel>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var shark in sharks)
        {
            PlayerSpotted += shark.OnSpotted;
        }
        foreach (var sentinel in sentinels)
        {
            sentinel.PlayerSpotted += SentinelWarning;
        }
    }

    public void SentinelWarning(bool spotted, Sentinel sentinel)
    {
        if (spotted)
            spottedSentinels.Add(sentinel);
        else
            spottedSentinels.Remove(sentinel);
        Debug.Log("Eh? " + spottedSentinels.Count);
        PlayerSpotted.Invoke(spottedSentinels.Count > 0);
    }
}
