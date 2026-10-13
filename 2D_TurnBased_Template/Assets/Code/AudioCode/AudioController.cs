using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public List<AudioSource> AllAudioSources;
    public AudioSource CampfireMusic;
    public bool IsPlayBattleMusic = false;
    public AudioSource BattleMusic;
    public bool CanPlayWinningMusic = false;
    public AudioSource WinningRoundMusic;
    [Header("death sound")]
    public AudioSource DeathSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        AdjustVolume(0.3f);
        WinningRoundMusic.Stop();
    }
    private void Update()
    {
                                                          
    }

    //campfire stuff
    public void PlayCampfireMusic()
    {
        Debug.Log("play campfires sonng");
        CampfireMusic.Play();
    }
    public void StopPlayCampfireMusic()
    {
        Debug.Log("stop playing campfire");
        CampfireMusic.Stop();
    }

    //winning round songs
    public void PlayWinningRoundSong()
    {
        StartCorutineFadeOutBattleMusic();
    }


    //Battle music stuff
    public void PlayBattleMusic()
    {
        IsPlayBattleMusic = true;
        CanPlayWinningMusic = true;
        BattleMusic.Play();
    }
    public void RaiseBattleMusicVolume() => StartCoroutine(RaiseBattleMusic());
    public void StartCorutineFadeOutBattleMusic() => StartCoroutine(FadeOutOfBattleSong(BattleMusic, WinningRoundMusic, 6f));
    IEnumerator RaiseBattleMusic()
    {
        BattleMusic.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.05f);
        BattleMusic.volume = 0.30f;//low volume for not in fight yet
    }
    IEnumerator FadeOutOfBattleSong(AudioSource battleSong, AudioSource winningRoundSong , float lenghtofWinningSong)
    {
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0.3f;//whatever this max is
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0.15f;
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0f;
        winningRoundSong.Play();
        yield return new WaitForSecondsRealtime(lenghtofWinningSong);
        battleSong.volume = 0.10f;
    }


    public void PlayDeathSound()
    {
        BattleMusic.Stop();
        CampfireMusic.Stop();
        WinningRoundMusic.Stop();
        IsPlayBattleMusic = false;
        CanPlayWinningMusic = false;
        DeathSound.Play();
    }

    /// <summary>
    /// Changes all audio volume.
    /// </summary>
    /// <param name="volmueAmount"></param>
    public void AdjustVolume(float volmueAmount)
    {
        foreach (AudioSource audioSource in AllAudioSources)
        {
            audioSource.volume = volmueAmount;
        }
    }
}
