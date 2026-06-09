using UnityEngine;
using UnityEngine.InputSystem;

public class CastingManager : MonoBehaviour
{
    [SerializeField] private float reelSpeed;
    [SerializeField] private float yankStrength;
    [SerializeField] private float lineIntegrity;
    [SerializeField] private float lineStrength;
    
    [Space(10)]
    [Header("Action Scripts")]
    [SerializeField] private PlayerAimer playerAimer;
    [SerializeField] private PlayerCasting playerCasting;

    private PlayerInput playerInput;
    
    private FishingState currentState;
    private bool turnedOn;
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
            turnedOn = true;
        }
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
        
        TurnOffActions();
    }
}
