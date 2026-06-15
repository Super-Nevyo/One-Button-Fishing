using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreNum;

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
