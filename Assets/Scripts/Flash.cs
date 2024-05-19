using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Image image;
    [SerializeField] private Color flashingColor;
    [SerializeField] private float flashSpeed = 2f;

    public bool isFlashing = false;
    
    private Color originalColor;
    private float currentValue = 0.0f;
    private float saturateOrDesaturate = 1f; /// -1 for desaturation, +1 for saturation
    private float desaturate = -1.0f;
    private float saturate = 1.0f;
    private float neutral = 0.0f;
    

    private void Awake()
    {
        if (spriteRenderer != null) { originalColor = spriteRenderer.color; }
        else if (image != null) { originalColor = image.color; }
    }
    private void Update()
    {
        if (isFlashing)
        {
            currentValue += Time.deltaTime * saturateOrDesaturate * flashSpeed;

            if (currentValue > saturate)
            {
                saturateOrDesaturate = desaturate;
                currentValue = saturate;
            }
            else if (currentValue < neutral)
            {
                saturateOrDesaturate = saturate;
                currentValue = neutral;
            }

            if (spriteRenderer != null){ spriteRenderer.color = Color.Lerp(originalColor, flashingColor, currentValue);}
            else if (image != null) { image.color = Color.Lerp(originalColor, flashingColor, currentValue); }
        }
    }
}