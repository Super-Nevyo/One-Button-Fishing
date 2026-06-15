using UnityEngine;

public class FSRun : IFishState
{
    private BaseFishKoi _fish;
    public void Enter()
    {
        _fish.IsEscaping = true;
        _fish.PickRunLocation();
        _fish.Anim.SetBool("Swim", true);
    }

    public void Exit()
    {
        _fish.IsEscaping=false;
        _fish.Anim.SetBool("Swim", false);
    }

    public void OnYank()
    {
        _fish.FishDislike += _fish.YankDislike;
        _fish.RB.angularVelocity += -_fish.YankStrength * _fish.RunMult * 10 * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        _fish.RB.linearVelocity += _fish.YankStrength / (_fish.PullStrength * _fish.RunMult) * (_fish.PlayerPosition - new Vector2(_fish.transform.position.x, _fish.transform.position.y)).normalized;
        Debug.Log("run");
    }

    public void Update()
    {
        _fish.RB.linearVelocity += (_fish.MoveToPoint + new Vector2(_fish.transform.position.x, _fish.transform.position.y)).magnitude * _fish.RunMult * _fish.Speed * new Vector2(_fish.transform.up.x, _fish.transform.up.y);
        _fish.RB.angularVelocity += -_fish.TurnSpeed * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.MoveToPoint.y - _fish.transform.position.y, _fish.MoveToPoint.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));

        if (_fish.IsReeled)
        {
            _fish.RB.angularVelocity += -(_fish.ReelStrength / _fish.PullStrength) * _fish.RunMult * 10 * Mathf.Atan2(Mathf.Cos(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(_fish.PlayerPosition.y - _fish.transform.position.y, _fish.PlayerPosition.x - _fish.transform.position.x) - _fish.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            _fish.RB.linearVelocity += _fish.ReelStrength / (_fish.PullStrength * _fish.RunMult) * (_fish.PlayerPosition - new Vector2(_fish.transform.position.x, _fish.transform.position.y)).normalized;
            _fish.FishDislike += _fish.ReelDislike;
        }
    }
    public FSRun(BaseFishKoi fish)
    {
        _fish = fish;
    }
}
