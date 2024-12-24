using UnityEngine;

public class StayInPlayArea : MonoBehaviour
{
    public GameObject playArea; // Reference to the play area GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the one you want to constrain
        if (other.gameObject == gameObject)
        {
            ConstrainObject();
        }
    }

    private void ConstrainObject()
    {
        // Get the bounds of the play area
        Bounds playAreaBounds = playArea.GetComponent<Collider>().bounds;

        // Clamp the object's position to stay within the play area bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, playAreaBounds.min.x, playAreaBounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, playAreaBounds.min.y, playAreaBounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, playAreaBounds.min.z, playAreaBounds.max.z);

        // Apply the clamped position
        transform.position = clampedPosition;
    }
}
