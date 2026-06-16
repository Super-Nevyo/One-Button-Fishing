using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float timeInLevel;
    private float _startTime;
    void Start()
    {
        _startTime = Time.time;
    }
    void FixedUpdate()
    {
        
    }
}
