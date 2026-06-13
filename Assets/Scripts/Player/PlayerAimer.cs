using UnityEngine;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float degrees;

    private void Update()
    {
        degrees += rotationSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(degrees, -45, 45));

        if (degrees > 44 || degrees < -44)
        {
            rotationSpeed *= -1;
        }
    }
}
