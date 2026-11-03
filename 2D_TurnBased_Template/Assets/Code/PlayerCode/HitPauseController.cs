using System.Collections;
using UnityEngine;

public class HitPauseController : MonoBehaviour
{
    public static HitPauseController Instance;
    public bool ActivateFreezeTime = false;
    public bool IsFrozen = false;
    public float HitPauseWaitTime;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        if(!IsFrozen && ActivateFreezeTime)
        {
            PlayHitPauseCoroutine();
        }
    }
    public void TriggerFreezeTime() => ActivateFreezeTime = true;
    void PlayHitPauseCoroutine() => StartCoroutine(PlayHitPause());

    public IEnumerator PlayHitPause()
    {
        IsFrozen = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(HitPauseWaitTime);
        Time.timeScale = 1f;
        IsFrozen = false; 
        ActivateFreezeTime = false;
    }
}
