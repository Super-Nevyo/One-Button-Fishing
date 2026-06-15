using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _score = 0;
    public Action<int> UpdateScore;

    [SerializeField] private GameObject[] fish;
    private GameObject newFish;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }
    public void AddScore(int score)
    {
        _score += score;
        UpdateScore?.Invoke(_score);
    }

    public void SpawnFish()
    {
        if (fish.Length > 1)
            newFish = fish[UnityEngine.Random.Range(0, fish.Length)];
        else
            newFish = fish[0];
        
        Instantiate(newFish, transform.position, newFish.transform.rotation);
    }
}
