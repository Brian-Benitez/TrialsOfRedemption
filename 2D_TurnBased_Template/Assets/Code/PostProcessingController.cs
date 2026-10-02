using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingController : MonoBehaviour
{
    public static PostProcessingController Instance;
    public float WaitTimeToDismissEffect;
    public Volume VolumeProfileRef;
    public Volume DeathTunnelVision;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayCorutineHitEffect() => StartCoroutine(PlayHitPostProcessing());
    IEnumerator PlayHitPostProcessing()
    {
        VolumeProfileRef.weight = 1.0f;
        yield return new WaitForSecondsRealtime(WaitTimeToDismissEffect);
        VolumeProfileRef.weight = 0f;
    }

    public void PlayCorutineDeathTunnelVision() => StartCoroutine(PlayDeathTunnelVision());
    public void StopDeathTunnelVison() => DeathTunnelVision.weight = 0;
    IEnumerator PlayDeathTunnelVision()
    {
        DeathTunnelVision.weight = 0f;
        yield return new WaitForSecondsRealtime(.25f);
        DeathTunnelVision.weight = 0.25f;
        yield return new WaitForSecondsRealtime(.25f);
        DeathTunnelVision.weight = 0.50f;
        yield return new WaitForSecondsRealtime(.25f);
        DeathTunnelVision.weight = 1f;
    }

}
