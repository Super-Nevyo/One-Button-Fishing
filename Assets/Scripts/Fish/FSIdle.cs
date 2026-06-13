using System.Collections;
using UnityEngine;

public class FSIdle : IState
{
    private BaseFishKoi _fish;
    public void Enter()
    {
        _fish.StartCoroutine(IdleFor(_fish.WaitTime));
        Debug.Log("Starting Idle");
    }

    public void Exit()
    {
        _fish.StopCoroutine(IdleFor(_fish.WaitTime));
    }

    public void Update()
    {
    }

    public FSIdle(BaseFishKoi fish)
    {
        _fish = fish;
    }
    private IEnumerator IdleFor(float idleTime)
    {
        yield return new WaitForSeconds(idleTime);
        _fish.MyStateMachine.ChangeState(_fish.MyStateMachine.WanderState);
    }
}
