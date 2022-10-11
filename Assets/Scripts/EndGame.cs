using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject youLose;
    public GameObject youWin;
    public TMP_Text fishLeft;
    public Shark[] sharks;
    public Fish[] fishes;
    private int fishesLeft;
    // Start is called before the first frame update
    void Start()
    {
        fishesLeft = fishes.Length;
        fishLeft.text = "Fish Left: " + fishesLeft;
        foreach (var shark in sharks)
        {
            shark.ChompedYa += OnChomp;
        }
        foreach (var fish in fishes)
        {
            fish.FishSnack += OnSnack;
        }
    }

    public void OnChomp()
    {
        youLose.SetActive(true);
        StartCoroutine(Reset());
    }

    public void OnSnack()
    {
        fishesLeft -= 1;
        fishLeft.text = "Fish Left: " + fishesLeft;
        if (fishesLeft <= 0)
        {
            youWin.SetActive(true);
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Splash");
    }
}
