using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleToggle : MonoBehaviour
{
    public ParticleSystem particleSystemToToggle;
    public InputActionReference toggleActionReference;

    private void OnEnable()
    {
        // Subscribe to the action's performed event
        toggleActionReference.action.performed += OnToggleAction;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action's performed event
        toggleActionReference.action.performed -= OnToggleAction;
    }

    private void OnToggleAction(InputAction.CallbackContext context)
    {
        // Toggle the Particle System on and off
        if (particleSystemToToggle != null)
        {
            if (particleSystemToToggle.isPlaying)
            {
                // If the Particle System is playing, stop it
                particleSystemToToggle.Stop();
            }
            else
            {
                // If the Particle System is not playing, start it
                particleSystemToToggle.Play();
            }
        }
    }
}

