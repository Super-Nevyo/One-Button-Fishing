using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float targetSpeed;
    
    [SerializeField] private Transform boundaryPoint1;
    [SerializeField] private Transform boundaryPoint2;

    private void Start()
    {
        if(aimTarget == null)
            aimTarget = GameObject.FindGameObjectWithTag("Target");
        
        aimTarget.SetActive(true);
    }
    
    private void Update()
    {
        aimTarget.transform.position += playerBody.transform.up * (targetSpeed * Time.deltaTime);

        if (aimTarget.transform.position.x <= boundaryPoint1.position.x || aimTarget.transform.position.x >= boundaryPoint2.position.x)
            targetSpeed *= -1;
        
        if(aimTarget.transform.position.y <= boundaryPoint1.position.y || aimTarget.transform.position.y >= boundaryPoint2.position.y) 
            targetSpeed *= -1;
    }

    public Vector3 DropHook()
    {
        Debug.Log(aimTarget.transform.position);
        return aimTarget.transform.position;
    }

    public void HideTarget()
    {
        aimTarget.SetActive(false);
    }
}
