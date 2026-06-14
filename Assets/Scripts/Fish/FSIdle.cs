using System.Collections;
using UnityEngine;

public class FSIdle : IFishState
{
    private BaseFishKoi _fish;
    public void Enter()
    {
        _fish.StartCoroutine(IdleFor(_fish.WaitTime));
    }

    public void Exit()
    {
        _fish.StopCoroutine(IdleFor(_fish.WaitTime));
    }

    public void Update()
    {
        if(_fish.IsReeled)
        {
            _fish.RB.angularVelocity += -_fish.ReelStrength * 10 * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            _fish.RB.linearVelocity += _fish.ReelStrength * (_fish.PlayerPosition - new Vector2(_fish.transform.position.x, _fish.transform.position.y)).normalized;
            _fish.FishDislike += _fish.ReelDislike * _fish.IdleMultiplier * Time.fixedDeltaTime;
        }
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

    public void OnYank()
    {
        throw new System.NotImplementedException();
    }
}
