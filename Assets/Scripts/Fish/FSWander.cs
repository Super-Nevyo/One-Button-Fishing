using System.Collections;
using UnityEngine;

public class FSWander : IFishState
{
    private BaseFishKoi _fish;
    public void Enter()
    {
        _fish.StartCoroutine(StopWanderAfter(_fish.DecisionSpeed));
        _fish.PickWanderLocation();
        _fish.Anim.SetBool("Swim", true);
    }

    public void Exit()
    {
        _fish.StopCoroutine(StopWanderAfter(_fish.DecisionSpeed));
        _fish.Anim.SetBool("Swim", false);
    }

    public void Update()
    {
        _fish.RB.linearVelocity += (_fish.MoveToPoint + new Vector2(_fish.transform.position.x, _fish.transform.position.y)).magnitude * _fish.Speed * new Vector2(_fish.transform.up.x, _fish.transform.up.y);
        _fish.RB.angularVelocity += - _fish.TurnSpeed * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));

        if (_fish.IsReeled)
        {
            _fish.RB.angularVelocity += -_fish.ReelStrength * 10 * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            _fish.RB.linearVelocity += (_fish.ReelStrength / _fish.PullStrength) * (_fish.PlayerPosition - new Vector2(_fish.transform.position.x, _fish.transform.position.y)).normalized;
            _fish.FishDislike += _fish.ReelDislike * Time.fixedDeltaTime;
        }
    }
    public FSWander(BaseFishKoi fish)
    {
        _fish = fish;

    }

    private IEnumerator StopWanderAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime + Random.Range(waitTime/2, -waitTime/2));
        _fish.MyStateMachine.ChangeState(_fish.MyStateMachine.IdleState);
    }

    public void OnYank()
    {
        _fish.FishDislike += _fish.YankDislike;
        _fish.RB.angularVelocity += -_fish.YankStrength * 10 * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        _fish.RB.linearVelocity += (_fish.YankStrength / _fish.PullStrength) * (_fish.PlayerPosition - new Vector2(_fish.transform.position.x, _fish.transform.position.y)).normalized;
        Debug.Log("wander");
    }
}
