using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    [SerializeField] private CastingManager castingManager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //reference to the fish state
        if (col.gameObject.CompareTag("Fish"))
        {
            castingManager.CaughtFish(col.GetComponent<BaseFishKoi>());
            Debug.Log("Caught");
        }
    }
}
