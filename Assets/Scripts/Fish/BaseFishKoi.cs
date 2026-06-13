using UnityEngine;

public class BaseFishKoi : MonoBehaviour
{
    [SerializeField] protected float pullStrength;
    [SerializeField] protected float pointAmount;
    [SerializeField] public float Speed;
    [SerializeField] public float DecisionSpeed;
    [SerializeField] public float WaitTime;
    [SerializeField] public float turnSpeed;
    protected Vector2 topLeft;
    protected Vector2 botRight;
    [HideInInspector]
    public Vector2 MoveToPoint;
    [HideInInspector]
    public FishStateMachine MyStateMachine;
    [HideInInspector]
    public Rigidbody2D RB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        MyStateMachine = new FishStateMachine(this);
        MyStateMachine.Initialize(MyStateMachine.WanderState);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MyStateMachine.Execute();
    }

    public virtual void PickWanderLocation()
    {
        topLeft = new Vector2(-8, 4);
        botRight = new Vector2(8, -4);
        MoveToPoint = new Vector2(Random.Range(topLeft.x,botRight.x), Random.Range(topLeft.y,botRight.y));
    }
}
