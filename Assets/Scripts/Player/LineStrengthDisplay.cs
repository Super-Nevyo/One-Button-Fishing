using UnityEngine;
using UnityEngine.UI;

public class LineStrengthDisplay : MonoBehaviour
{
    [SerializeField] private Image image;

    public void UpdateOpacity(float percent)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, percent);
        Debug.Log(percent);
    }
}
