using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float maxDistance = 50f; // The maximum distance the joystick handle can be dragged from its center
    private RectTransform handleRectTransform; // Reference to the RectTransform of the joystick handle
    private Vector2 startPosition; // Store the initial position of the handle
    private Vector2 inputVector; // The calculated input vector that represents the joystick's current state

    void Start()
    {
        handleRectTransform = GetComponent<RectTransform>(); // Get the RectTransform component of the joystick handle
        startPosition = handleRectTransform.anchoredPosition; // Store the starting position of the handle
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        // Convert the screen point of the pointer event to local position within the joystick's parent RectTransform
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            handleRectTransform.parent.GetComponent<RectTransform>(), eventData.position,
            eventData.pressEventCamera, out position))
        {
            float distance = Vector2.Distance(startPosition, position);

            // If the distance from the starting position exceeds the maximum distance, adjust the position
            if (distance > maxDistance)
            {
                // Calculate the new position within the max distance from the starting position
                position = startPosition + (position - startPosition).normalized * maxDistance;
            }

            // Update the anchored position of the handle and calculate the input vector
            handleRectTransform.anchoredPosition = position;
            inputVector = (position - startPosition) / maxDistance;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the anchored position of the handle and reset the input vector
        handleRectTransform.anchoredPosition = startPosition;
        inputVector = Vector2.zero;
    }

    public Vector2 GetInputVector()
    {
        return inputVector; // Return the calculated input vector
    }
}
