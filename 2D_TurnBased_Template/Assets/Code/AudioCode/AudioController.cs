using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource CampfireMusic;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void PlayCampfireMusic() => CampfireMusic.Play();
    public void StartCorutineFadeCurrentSong() => StartCoroutine(FadeOutOfSong(CampfireMusic));

    IEnumerator FadeOutOfSong(AudioSource currentSong)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        currentSong.volume = 0.75f;
        yield return new WaitForSecondsRealtime(0.25f);
        currentSong.volume = 0.50f;
        yield return new WaitForSecondsRealtime(0.25f);
        currentSong.volume = 0.25f;
        yield return new WaitForSecondsRealtime(0.25f);
        currentSong.volume = 0f;
    }
}
