using UnityEditor;
using UnityEngine;

public class BaseFishKoi : MonoBehaviour
{
    [SerializeField] public Animator Anim;
    [SerializeField] public float PullStrength;
    [SerializeField] protected int pointAmount;
    [SerializeField] public float Speed;
    [SerializeField] public float DecisionSpeed;
    [SerializeField] public float WaitTime;
    [SerializeField] public float TurnSpeed;
    [SerializeField] public float RunMult;
    [SerializeField] protected float maxFishDislike;
    [SerializeField] public float YankDislike;
    [SerializeField] public float ReelDislike;
    [SerializeField] public float IdleMultiplier;
    [SerializeField] protected float dislikeDrain;
    [SerializeField] public Vector2 PlayerPosition;
    [SerializeField] public float YankStrength;
    [SerializeField] public float ReelStrength;
    protected Vector2 topLeft;
    protected Vector2 botRight;
    [HideInInspector]
    public float FishDislike = 0;
    [HideInInspector]
    public Vector2 MoveToPoint;
    [HideInInspector]
    public FishStateMachine MyStateMachine;
    [HideInInspector]
    public Rigidbody2D RB;
    public bool IsHooked = false;
    public bool IsYanked;
    public bool IsReeled;
    [HideInInspector]
    public bool IsEscaping = false;
    private CastingManager _player;


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        MyStateMachine = new FishStateMachine(this);
        MyStateMachine.Initialize(MyStateMachine.WanderState);
    }



    protected virtual void FixedUpdate()
    {
        MyStateMachine.Execute();
        if (FishDislike > 0)
        {
            FishDislike -= dislikeDrain * Time.fixedDeltaTime;
            //Debug.Log(FishDislike);
        }
        else if (FishDislike < 0) { FishDislike = 0; }
        if (FishDislike > maxFishDislike && !IsEscaping) { MyStateMachine.ChangeState(MyStateMachine.RunState); }
        if (transform.position.y > 6 && IsEscaping) GameObject.Destroy(this.gameObject);
        if (IsYanked)
        {
            OnYank();
        }
    }

    
    public void OnHooked(CastingManager player)
    {
        IsHooked = true;
        _player = player;
    }
    public void OnCaught()
    {
        GameManager.instance.AddScore(pointAmount);
        Destroy(this.gameObject);
    }
    public void OnYank()
    {
        MyStateMachine.OnYank();
        IsYanked = false;
    }
    public void OnReel(float ReelStrength)
    {
        this.ReelStrength = ReelStrength;
        if (ReelStrength != 0) IsReeled = true;
        else IsReeled = false;
    }


    public virtual void PickWanderLocation()
    {
        topLeft = new Vector2(-8, 4);
        if (IsHooked) botRight = new Vector2(8, 0);
        else botRight = new Vector2(8, -2);
        MoveToPoint = new Vector2(Random.Range(topLeft.x, botRight.x), Random.Range(topLeft.y, botRight.y));
    }
    public virtual void PickRunLocation()
    {
        topLeft = new Vector2(-8, 8);
        botRight = new Vector2(8, 6);
        MoveToPoint = new Vector2(Random.Range(topLeft.x, botRight.x), Random.Range(topLeft.y, botRight.y));
    }



    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        if (MoveToPoint != null)
        {
            Handles.DrawWireDisc(new Vector3(MoveToPoint.x, MoveToPoint.y, 0), Vector3.forward, 0.1f);
        }
    }
    
}
