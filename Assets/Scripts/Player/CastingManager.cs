using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CastingManager : MonoBehaviour
{
    [SerializeField] private float reelSpeed;
    [SerializeField] private float yankStrength;
    [SerializeField] private float lineIntegrity;
    [SerializeField] private float lineStrength;

    [SerializeField] private GameObject hook;
    [SerializeField] private GameObject aimer;
    
    [Space(10)]
    [Header("Action Scripts")]
    [SerializeField] private PlayerAimer playerAimer;
    [SerializeField] private PlayerCasting playerCasting;

    [SerializeField] private PlayerInput playerInput;
    
    private FishingState currentState;
    private bool turnedOn;

    private float pressValue;
    private Vector3 direction;
    private Vector3 hookSpawnPoint;
    private Transform hookPosition;
    private bool canClick;
    private GameObject fih;

    private void Start()
    {
        TurnOffActions();
        currentState = FishingState.Aim;
        canClick = true;
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
            playerCasting.enabled = true;
            turnedOn = true;
        }

        if (currentState == FishingState.Waiting)
        {
            ReelInFish();
            CheckHookDistance();
        }
    }

    private void ReelInFish()
    {
        //gonna throw some shit here for now
        //can delete it when we have the fish do the reeling 
        direction = (hook.transform.position - transform.position).normalized;
        hook.transform.position -= direction * (reelSpeed * pressValue * Time.deltaTime);
    }

    private void CheckHookDistance()
    {
        if (hook.transform.position.x < hookSpawnPoint.x + 1 && hook.transform.position.x > hookSpawnPoint.x - 1)
        {
            if (hook.transform.position.y < hookSpawnPoint.y + 1 && hook.transform.position.y > hookSpawnPoint.y - 1 && pressValue < 1)
            {
                currentState = FishingState.Aim;
                hook.SetActive(false);
                StartCoroutine(clickTimer(0.2f));
            }
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
        if (currentState == FishingState.Aim && canClick)
        {
            currentState = FishingState.Cast;
            StartCoroutine(clickTimer(0.2f));
            aimer.SetActive(true);
            aimer.transform.position = transform.position;
        }
        else if (currentState == FishingState.Cast && canClick)
        {
            hook.SetActive(true);
            hookSpawnPoint = hook.transform.position;
            hook.transform.position = playerCasting.DropHook();
            playerCasting.HideTarget();
            currentState = FishingState.Waiting;
            StartCoroutine(clickTimer(0.2f));
        }
        else if (currentState == FishingState.Waiting)
        {
            pressValue = value.Get<float>();
        }
        else
        {
            return;
        }
        
        TurnOffActions();
    }

    private IEnumerator clickTimer(float timer)
    {
        canClick = false;
        yield return new WaitForSeconds(timer);
        canClick = true;
    }
}
