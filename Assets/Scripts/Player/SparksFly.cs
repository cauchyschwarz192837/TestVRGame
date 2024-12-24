using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparksFly : MonoBehaviour
{
    public ParticleSystem particleSystemToPlay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Start playing the Particle System for 0.5 seconds
            if (particleSystemToPlay != null)
            {
                StartCoroutine(PlayParticleSystemForDuration(5.0f));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (particleSystemToPlay != null)
        {
            particleSystemToPlay.Stop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Stop playing the Particle System
        if (particleSystemToPlay != null)
        {
            particleSystemToPlay.Stop();
        }
    }

    private IEnumerator PlayParticleSystemForDuration(float duration)
    {
        // Play the Particle System
        particleSystemToPlay.Play();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Stop playing the Particle System after the duration
        particleSystemToPlay.Stop();
    }
}
