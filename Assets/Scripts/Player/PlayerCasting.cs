using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float aimMaxTimer;

    [SerializeField] private Transform boundaryPoint1;
    [SerializeField] private Transform boundaryPoint2;

    private float aimTimer;

    private void Update()
    {
        aimTarget.position += playerBody.transform.up * (targetSpeed * Time.deltaTime);

        if (aimTarget.position.x <= boundaryPoint1.position.x || aimTarget.position.x >= boundaryPoint2.position.x)
            targetSpeed *= -1;
        
        if(aimTarget.position.y <= boundaryPoint1.position.y || aimTarget.position.y >= boundaryPoint2.position.y) 
            targetSpeed *= -1;
        
        
    }
}
