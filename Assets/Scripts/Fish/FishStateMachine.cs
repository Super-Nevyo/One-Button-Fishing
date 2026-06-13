using UnityEngine;

public class FishStateMachine
{
    private BaseFishKoi _fish;
    public IState currentState { get; private set; }

    public FSWander WanderState;
    public FSIdle IdleState;


    public FishStateMachine(BaseFishKoi fish)
    {
        _fish = fish;
        WanderState = new FSWander(fish);
        IdleState = new FSIdle(fish);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void ChangeState(IState state)
    {
        currentState.Exit();
        currentState = state;
        state.Enter();
    }

    public void Execute()
    {
        currentState?.Update();
    }
}
