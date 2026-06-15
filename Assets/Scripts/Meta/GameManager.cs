using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _score = 0;
    public Action<int> UpdateScore;

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

}
