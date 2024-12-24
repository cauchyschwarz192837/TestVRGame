using UnityEngine;

public class DefeatEffect : MonoBehaviour
{
    public ParticleSystem particleSystemToPlay;
    public HealthSys healthSys;

    void Start () {
        particleSystemToPlay.Stop();
    }
    void Update ()
    {
        if (healthSys.currentHealth == 0)
        {
            // Start playing the Particle System
            if (particleSystemToPlay != null)
            {
                particleSystemToPlay.Play();
            }
        }
    }
}
