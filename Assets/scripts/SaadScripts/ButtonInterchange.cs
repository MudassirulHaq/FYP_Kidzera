using UnityEngine;

public class ButtonInterchange : MonoBehaviour
{
    public RectTransform lionButtonRectTransform;
    public RectTransform tigerButtonRectTransform;
    private bool isLionButtonUp = true; // Track whether Lion button is currently up or down
    private float timer = 0f; // Timer to control button interchange interval
    private float interchangeInterval = 2f; // Interchange interval in seconds

    void Start()
    {
        // Interchange button positions initially
        InterchangeButtonPositions();
    }

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if interchange interval has passed
        if (timer >= interchangeInterval)
        {
            // Interchange button positions
            InterchangeButtonPositions();

            // Reset timer
            timer = 0f;
        }
    }

    void InterchangeButtonPositions()
    {
        // Swap the button positions
        Vector3 lionButtonPosition = lionButtonRectTransform.anchoredPosition3D;
        Vector3 tigerButtonPosition = tigerButtonRectTransform.anchoredPosition3D;

        lionButtonRectTransform.anchoredPosition3D = tigerButtonPosition;
        tigerButtonRectTransform.anchoredPosition3D = lionButtonPosition;

        // Toggle the isLionButtonUp flag
        isLionButtonUp = !isLionButtonUp;
    }
}
