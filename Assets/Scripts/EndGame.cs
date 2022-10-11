using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject youLose;
    public GameObject youWin;
    public Shark[] sharks;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var shark in sharks)
        {
            shark.ChompedYa += OnChomp;
        }
    }

    public void OnChomp()
    {
        youLose.SetActive(true);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
