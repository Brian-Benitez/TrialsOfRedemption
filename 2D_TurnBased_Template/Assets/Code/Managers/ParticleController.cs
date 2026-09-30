using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController Instance;
    public ParticleSystem LowHealingParticle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayLowHealingParticleEffect() => LowHealingParticle.Play();
}
