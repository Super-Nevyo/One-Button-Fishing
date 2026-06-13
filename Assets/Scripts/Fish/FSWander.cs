using System.Collections;
using UnityEngine;

public class FSWander : IState
{
    private BaseFishKoi _fish;
    public void Enter()
    {
        _fish.StartCoroutine(StopWanderAfter(_fish.DecisionSpeed));
        _fish.PickWanderLocation();
        Debug.Log("starting wander");
    }

    public void Exit()
    {
        _fish.StopCoroutine(StopWanderAfter(_fish.DecisionSpeed));
    }

    public void Update()
    {
        _fish.RB.linearVelocity += (_fish.MoveToPoint + new Vector2(_fish.transform.position.x, _fish.transform.position.y)).magnitude * _fish.Speed * new Vector2(_fish.transform.up.x, _fish.transform.up.y);
        _fish.RB.angularVelocity += - _fish.turnSpeed * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
    }
    public FSWander(BaseFishKoi fish)
    {
        _fish = fish;

    }

    private IEnumerator StopWanderAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _fish.MyStateMachine.ChangeState(_fish.MyStateMachine.IdleState);
    }
}
