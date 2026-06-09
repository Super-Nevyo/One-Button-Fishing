using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float aimMaxTimer;

    private float aimTimer;

    private void Update()
    {
        aimTarget.position += playerBody.transform.up * (targetSpeed * Time.deltaTime);
        
        if(aimTimer > aimMaxTimer)
        {
            targetSpeed *= -1;
            aimTimer = 0; 
        }
        
        aimTimer += Time.deltaTime;
    }
}
