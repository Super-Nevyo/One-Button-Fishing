using UnityEngine;

public class FishStateMachine
{
    private BaseFishKoi _fish;
    public IFishState currentState { get; private set; }

    public FSWander WanderState;
    public FSIdle IdleState;
    public FSRun RunState;

    public FishStateMachine(BaseFishKoi fish)
    {
        _fish = fish;
        WanderState = new FSWander(fish);
        IdleState = new FSIdle(fish);
        RunState = new FSRun(fish);
    }

    public void Initialize(IFishState state)
    {
        currentState = state;
        state.Enter();
    }

    public void ChangeState(IFishState state)
    {
        currentState.Exit();
        currentState = state;
        state.Enter();
    }

    public void Execute()
    {
        currentState?.Update();
    }
    public void OnYank()
    {
        currentState.OnYank();
    }
}
