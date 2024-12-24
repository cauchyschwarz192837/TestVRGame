using UnityEngine;

public class DynamicCurveDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100;
    public float curveScale = 1.0f;
    public float kMin = 0.01f;
    public float kMax = 20.0f;
    public float kStep = 0.1f; // Amount to change k when space bar is held down
    public float startX = 0.0f;

    private float currentK = 10.0f;
    private bool increaseK = false;
    private bool spaceBarPressed = false;
    private bool curveUpdated = false;

    private void Start()
    {
        // Initialize the Line Renderer
        lineRenderer.positionCount = numberOfPoints;
        UpdateCurve();
    }

    private void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !curveUpdated)
        {
            spaceBarPressed = true;
        }

        // Check if the space bar is released
        if (Input.GetKeyUp(KeyCode.Space) && !curveUpdated)
        {
            spaceBarPressed = false;

            // Set a flag to indicate that the curve has been updated
            curveUpdated = true;
        }

        // Only update k if the space bar is held down and the curve has not been updated
        if (spaceBarPressed && !curveUpdated)
        {
            // Change the direction of k change when the minimum or maximum value is reached
            if (currentK <= kMin || currentK >= kMax)
            {
                increaseK = !increaseK;
            }

            // Update k based on the direction
            currentK += increaseK ? kStep : -kStep;

            // Clamp k to the specified range
            currentK = Mathf.Clamp(currentK, kMin, kMax);
        }

        // Update the curve with the current value of k
        UpdateCurve(currentK);
    }

    private void UpdateCurve(float k = 10.0f)
    {
        // Check if the space bar is pressed before updating the curve
        if (!spaceBarPressed)
        {
            return; // Skip updating the curve
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = startX + (float)i / (numberOfPoints - 1) * Mathf.PI / k;
            float y = Mathf.Sin(k * x) * curveScale;

            // Update the position of each vertex in the Line Renderer
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}
