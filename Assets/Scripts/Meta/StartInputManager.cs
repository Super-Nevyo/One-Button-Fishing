using System.Collections;
using UnityEngine;

public class StartInputManager : MonoBehaviour
{
    private bool _canAccept = false;
    void Start()
    {
        StartCoroutine(MisckickPrevention());
    }
    public void OnAction()
    {
        if (_canAccept)
        GameManager.instance.EndLevel();
    }
    IEnumerator MisckickPrevention()
    {
        yield return new WaitForSeconds(1);
        _canAccept = true;
    }
}
