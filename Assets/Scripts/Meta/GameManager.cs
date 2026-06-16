using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _score = 0;
    public Action<int> UpdateScore;

    [SerializeField] private GameObject[] fish;
    [SerializeField] private GameObject koi;
    [SerializeField] private GameObject shark;
    [SerializeField] private GameObject octopus;

    [SerializeField] private int[] scoreToWin;
    private GameObject newFish;
    private int _lvlWeOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void AddScore(int score)
    {
        _score += score;
        UpdateScore?.Invoke(_score);
    }
    public void EndLevel()
    {
        if(_lvlWeOn == 0)
        {
            StartLevel1();
        }
        else if(_lvlWeOn == 1)
        {
            if (_score >= scoreToWin[0])
            StartLevel2();
            else StartScene();
        }
        else if(_lvlWeOn == 2)
        {
            if (_score >= scoreToWin[1])
                StartWinScene();
            else StartScene();
        }
        else if (_lvlWeOn == -1)
        {
            StartScene();
        }
    }

    public void SpawnFish()
    {
        if (fish.Length > 1)
            newFish = fish[UnityEngine.Random.Range(0, fish.Length)];
        else
            newFish = fish[0];
        
        Instantiate(newFish, transform.position, newFish.transform.rotation);
    }

    public void StartScene()
    {
        fish = new GameObject[0];
        _lvlWeOn = 0;
        SceneManager.LoadScene("StartScene");
    }
    public void StartLevel1()
    {
        fish = new GameObject[1] { koi };
        _score = 0;
        _lvlWeOn = 1;
        SceneManager.LoadScene("LVL1_Backyard");
    }
    public void StartLevel2()
    {
        fish = new GameObject[2] { shark, octopus };
        _lvlWeOn = 2;
        SceneManager.LoadScene("LVL2_Ocean");
    }
    public void StartWinScene()
    {
        fish = new GameObject[0];
        _lvlWeOn = -1;
        SceneManager.LoadScene("WinScene");
    }

}
