using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreNum;
    [SerializeField] private TMP_Text timeNum;
    [SerializeField] private float timeInLevel;
    private float _startTime;
    private float _currentTime;
    void Start()
    {
        _startTime = Time.time;
    }
    void FixedUpdate()
    {
        _currentTime = Time.time - _startTime;
        timeNum.SetText(((int)_currentTime).ToString());
        if (_currentTime > timeInLevel)
        {
            GameManager.instance.EndLevel();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForStart());
    }
    private void OnDisable()
    {
        GameManager.instance.UpdateScore -= changeScoreNum;
    }


    private void changeScoreNum(int newScore)
    {
        scoreNum.SetText(newScore.ToString());
    }
    private IEnumerator WaitForStart()
    {
        yield return new WaitForEndOfFrame();
        GameManager.instance.UpdateScore += changeScoreNum;
    }
}
