using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class CastingManager : MonoBehaviour
{
    [SerializeField] private float reelSpeed;
    [SerializeField] private float yankStrength;
    [SerializeField] private float lineIntegrity;
    [SerializeField] private float lineStrength;
    
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineStrengthIdentifier;
    [SerializeField] private LineStrengthDisplay lsd;

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
    private bool aimSwitchCondition;
    private BaseFishKoi fish;

    private float startingIntegrity;

    private void Start()
    {
        TurnOffActions();
        currentState = FishingState.Aim;
        canClick = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.enabled = false;
        startingIntegrity = lineIntegrity;
        aimSwitchCondition = false;
    }

    private void Update()
    {
        if (currentState == FishingState.Aim && !turnedOn)
        {
            lineIntegrity = startingIntegrity;
            playerAimer.enabled = true;
            turnedOn = true;
        }

        if (currentState == FishingState.Cast && !turnedOn)
        {
            playerCasting.enabled = true;
            turnedOn = true;
        }

        if (currentState == FishingState.Waiting)
        {
            ReelInFish();
            CheckHookDistance();
        }

        if (currentState == FishingState.Caught)
        {
            if (pressValue < 0.4f && aimSwitchCondition)
            {
                aimSwitchCondition = false;
                lineRenderer.enabled = false;
                lsd.UpdateOpacity(0);
                lineStrengthIdentifier.SetActive(false);
                fish = null;
                currentState = FishingState.Aim;
                TurnOffActions();
                StartCoroutine(clickTimer(0.2f));
                return;
            }

            if (!aimSwitchCondition)
            {
                if (fish.transform.position.y > 5.5f)
                {
                    aimSwitchCondition = true;
                    return;
                }
                if ((fish.transform.position - hookSpawnPoint).magnitude < 1)
                {
                    aimSwitchCondition = true;
                    fish.OnCaught();
                    return;
                }
            }

            if (fish == null) {lineRenderer.enabled = false; return;}
            
            lineRenderer.SetPosition(1, fish.transform.position);
            BreakingLine(pressValue * (fish.FishDislike + fish.PullStrength) * Time.deltaTime);
        }
    }

    private void ReelInFish()
    {
        //gonna throw some shit here for now
        //can delete it when we have the fish do the reeling 
        direction = (hook.transform.position - transform.position).normalized;
        hook.transform.position -= direction * (reelSpeed * pressValue * Time.deltaTime);
        lineRenderer.SetPosition(1, hook.transform.position);
    }

    private void CheckHookDistance()
    {
        if ((hookSpawnPoint - hook.transform.position).magnitude < 1 && pressValue < 1)
        { 
            currentState = FishingState.Aim; 
            hook.SetActive(false); 
            StartCoroutine(clickTimer(0.2f));
        }
    }

    private void TurnOffActions()
    {
        playerAimer.enabled = false;
        playerCasting.enabled = false;
        turnedOn = false;
    }

    public void CaughtFish(BaseFishKoi currentFish)
    {
        hook.gameObject.SetActive(false);
        fish = currentFish;
        Debug.Log(fish);
        currentState = FishingState.Caught;
        lineStrengthIdentifier.SetActive(true);
        fish.FishDislike += 1;
        fish.OnHooked(this);
    }

    public void BreakingLine(float fishStrength)
    {
        lineIntegrity -= fishStrength;
        lsd.UpdateOpacity(1 - lineIntegrity / startingIntegrity);

        if (lineIntegrity <= 0)
        {
            aimSwitchCondition = true;
        }
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
            hookSpawnPoint = transform.position;
            hook.transform.position = playerCasting.DropHook();
            playerCasting.HideTarget();
            currentState = FishingState.Waiting;
            lineRenderer.enabled = true;
            StartCoroutine(clickTimer(0.2f));
        }
        else if (currentState == FishingState.Waiting)
        {
            pressValue = value.Get<float>();
        }
        else if (currentState == FishingState.Caught)
        {
            pressValue = value.Get<float>();
            if (!canClick)
            {
                fish.OnYank();
            }
            else StartCoroutine(clickTimer(0.2f));
            fish.OnReel(lineStrength * pressValue);
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
