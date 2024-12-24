/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandSpecial : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    //-------------------------------------------------------
    public float damageValue;
    public float powerUpExtraDamage;
    private float initialDamage;
    public ParticleSystem firstParticleSystem;
    public ParticleSystem secondParticleSystem;

    //----------
    public float playDuration = 15f;  // Duration in seconds for which particle systems can play
    public float cooldownDuration = 30f;  // Cooldown duration in seconds
    private float lastPlayTime;  // Time when the particle systems were last played
    //----------

    private void Awake() {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context) {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }

    private void Start() {
        initialDamage = damageValue;
    }

    private void Update()
    {
        // Check if the first particle system is playing
        if (firstParticleSystem.isPlaying)
        {
            // Play the second particle system
            PlaySecondParticleSystem();
            damageValue = initialDamage + powerUpExtraDamage;
        }
        else
        {
            // Stop the second particle system if the first one has stopped
            StopSecondParticleSystem();
            damageValue = initialDamage;
        }
    }

    private void PlaySecondParticleSystem()
    {
        // Check if the second particle system is not already playing
        if (!secondParticleSystem.isPlaying)
        {
            // Play the second particle system
            secondParticleSystem.Play();
        }
    }

    private void StopSecondParticleSystem()
    {
        // Check if the second particle system is playing
        if (secondParticleSystem.isPlaying)
        {
            // Stop the second particle system
            secondParticleSystem.Stop();
        }
    }
}
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class WandSpecial : MonoBehaviour
{
    public ParticleSystem firstParticleSystem;
    public ParticleSystem secondParticleSystem;
    public InputActionReference toggleActionReference;
    public float playDuration = 5f;  // Duration in seconds for which particle systems can play
    public float cooldownDuration = 15f;  // Cooldown duration in seconds

    private enum ToggleState
    {
        Idle,
        Playing,
        Cooldown
    }

    private ToggleState currentState = ToggleState.Idle;
    private float toggleStartTime;

    private void OnEnable()
    {
        // Enable the InputAction
        toggleActionReference.action.Enable();
        toggleActionReference.action.performed += OnToggleAction;
    }

    private void OnDisable()
    {
        // Disable the InputAction
        toggleActionReference.action.Disable();
        toggleActionReference.action.performed -= OnToggleAction;
    }

    private void Update()
    {
        // Handle state transitions and timer updates
        switch (currentState)
        {
            case ToggleState.Playing:

                if (Time.time - toggleStartTime >= playDuration)
                {
                    // Transition to cooldown after playDuration
                    currentState = ToggleState.Cooldown;
                    ToggleParticleSystems(false);
                }
                break;

            case ToggleState.Cooldown:
                if (Time.time - toggleStartTime >= cooldownDuration)
                {
                    // Transition to idle after cooldownDuration
                    currentState = ToggleState.Idle;
                }
                break;
        }
    }

    private void OnToggleAction(InputAction.CallbackContext context)
    {
        // Handle button press based on current state
        switch (currentState)
        {
            case ToggleState.Idle:
                // Transition to playing and start the timer
                currentState = ToggleState.Playing;
                toggleStartTime = Time.time;
                ToggleParticleSystems(true);
                break;

            case ToggleState.Playing:
                // Nothing happens during playing state
                break;

            case ToggleState.Cooldown:
                // Nothing happens during cooldown state
                break;
        }
    }

    private void ToggleParticleSystems(bool play)
    {
        // Toggle the state of the first particle system
        ToggleParticleSystem(firstParticleSystem, play);

        // Toggle the state of the second particle system
        ToggleParticleSystem(secondParticleSystem, play);
    }

    private void ToggleParticleSystem(ParticleSystem particleSystem, bool play)
    {
        if (play)
        {
            // If play is true, play the particle system
            particleSystem.Play();
        }
        else
        {
            // If play is false, stop the particle system
            particleSystem.Stop();
        }
    }
}
