using UnityEngine;
using UnityEngine.InputSystem;

public class CastingManager : MonoBehaviour
{
    [SerializeField] private float reelSpeed;
    [SerializeField] private float yankStrength;
    [SerializeField] private float lineIntegrity;
    [SerializeField] private float lineStrength;

    [SerializeField] private GameObject hook;
    
    [Space(10)]
    [Header("Action Scripts")]
    [SerializeField] private PlayerAimer playerAimer;
    [SerializeField] private PlayerCasting playerCasting;

    [SerializeField] private PlayerInput playerInput;
    
    private FishingState currentState;
    private bool turnedOn;

    private Transform hookPosition;
    private bool pulling;
    private GameObject fih;

    private void Start()
    {
        TurnOffActions();
        currentState = FishingState.Aim;
    }

    private void Update()
    {
        if (currentState == FishingState.Aim && turnedOn == false)
        {
            playerAimer.enabled = true;
            turnedOn = true;
        }

        if (currentState == FishingState.Cast && turnedOn == false)
        {
            Debug.Log("In the backed yard straight up casting it, and by it, hehe, well, lets justr say, my rod");
            playerCasting.enabled = true;
            turnedOn = true;
        }

        if (currentState == FishingState.Waiting && pulling)
        {
            
        }
    }

    private void ReelInFish()
    {
        //gonna throw some shit here for now
        //can delete it when we have the fish do the reeling 
        Vector3 degrees = hook.transform.position - transform.position;
        
        hook.transform.rotation = new Quaternion(degrees.x, degrees.y, 0, 0);
        
        hook.transform.position -= hook.transform.up * (reelSpeed * Time.deltaTime);
    }

    private void TurnOffActions()
    {
        playerAimer.enabled = false;
        playerCasting.enabled = false;
        turnedOn = false;
    }

    public void OnAction(InputValue value)
    {
        if (currentState == FishingState.Aim)
        {
            currentState = FishingState.Cast;
        }
        else if (currentState == FishingState.Cast)
        {
            hook.SetActive(true);
            hook.transform.position = playerCasting.DropHook();
            playerCasting.HideTarget();
            currentState = FishingState.Waiting;
            playerInput.defaultActionMap = "Hold";
        }
        else
        {
            return;
        }
        
        TurnOffActions();
    }

    public void OnHold(InputValue value)
    {
        if (currentState == FishingState.Waiting)
        {
            if (value.Get<float>() >= 0.5f)
            {
                ReelInFish();
            }
        }
    }
}
