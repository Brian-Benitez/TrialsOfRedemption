using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public List<AudioSource> AllAudioSources;
    public AudioSource TitleThemeSong;
    public AudioSource CampfireMusic;
    public bool IsPlayBattleMusic = false;
    public AudioSource BattleMusic;
    public bool IsPlayingIldeMusic = false;
    public AudioSource IdleBattleMusic;
    public bool CanPlayWinningMusic = false;
    public AudioSource WinningRoundMusic;
    [Header("SFX")]
    [Header("Attacking SFX")]
    public AudioSource MeleeSlashSound;
    public AudioSource GroundSlamSound;
    public AudioSource ArrowAttackSound;
    [Header("Movement SFX")]
    public AudioSource DashSfx;
    [Header("Parry SFX")]
    public AudioSource ParrySfx;
    public AudioSource EnterRoomSfx;
    public bool IsPlayingTextSFX = false;
    public AudioSource TextSFX;
    [Header("death sound")]
    public AudioSource DeathSound;
    public AudioSource PlayerTakesDamageSound;
    [Header("Enemies Audio")]
    public AudioSource EnemySlashSFX;
    public AudioSource BlobAttackSFX;
    public AudioSource EnemyArrowShotSFX;
    [Header("Mis")]
    public AudioSource AbilityPopUpSFX;
    public AudioSource PickAbilitySFX;
    public AudioSource ItemPickUpSFX;
    [Header("Script Refs")]
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        AdjustVolume(0.3f);
        WinningRoundMusic.Stop();
        PlayMainThemeMusic();
    }
    ///TITLE SONG STUFF
    void PlayMainThemeMusic() => TitleThemeSong.Play();
    public void StartCourtineFadeOutOfTitleThemeSong() => StartCoroutine(FadeOutOfTitleThemeSong());

    IEnumerator FadeOutOfTitleThemeSong()
    {
        yield return new WaitForSecondsRealtime(0.10f);
        TitleThemeSong.volume = 0.3f;//whatever this max is
        yield return new WaitForSecondsRealtime(0.10f);
        TitleThemeSong.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.10f);
        TitleThemeSong.volume = 0.15f;
        yield return new WaitForSecondsRealtime(0.10f);
        TitleThemeSong.volume = 0f;
        TitleThemeSong.Stop();
        PlayCampfireMusic();
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
        IdleBattleMusic.volume = 0f;
        BattleMusic.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.05f);
        BattleMusic.volume = 0.30f;//low volume for not in fight yet
    }
    IEnumerator FadeOutOfBattleSong(AudioSource battleSong, AudioSource winningRoundSong, float lenghtofWinningSong)
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

        if (!IsPlayingIldeMusic)
        {
            IdleBattleMusic.Play();
            IdleBattleMusic.volume = 0.10f;
            IsPlayingIldeMusic = true;
        }
        else
            IdleBattleMusic.volume = 0.10f;


        battleSong.volume = 0f;
    }

    //SFX---------------------------
    //Player SFX--------------------
    public void PlayMeleeSoundEffect()
    {
        if (PlayerMeleeAttackRef.AmountOfAttacks == 1)
            MeleeSlashSound.pitch = 0.9f;
        if (PlayerMeleeAttackRef.AmountOfAttacks == 2)
            MeleeSlashSound.pitch = 1f;
        if (PlayerMeleeAttackRef.AmountOfAttacks >= 3)
            MeleeSlashSound.pitch = 1.1f;

        MeleeSlashSound.Play();
    }

    public void PlayGroundSlamEffect() => GroundSlamSound.Play();
    public void PlayArrowAttackSoundEffect()
    {
        ChangeAudioPitch(ArrowAttackSound);
        ArrowAttackSound.Play();
    }

    public void PlayDashSFX() => DashSfx.Play();
    public void PlayParrySFX()=> ParrySfx.Play();

    public void PlayEnterRoomSFX() => EnterRoomSfx.Play();
    public void PlayTextSFX() => StartCoroutine(CorutineTextSFX());
    public void StopTextSFX() => IsPlayingTextSFX = false;
    public IEnumerator CorutineTextSFX()
    {
        IsPlayingTextSFX = true;
        while(IsPlayingTextSFX)
        {
            yield return new WaitForSecondsRealtime(0.08F);
            TextSFX.Play();
        }       
    }
    public void PlayPlayerTookDamage() => PlayerTakesDamageSound.Play();
    public void PlayDeathSound()
    {
        BattleMusic.Stop();
        CampfireMusic.Stop();
        WinningRoundMusic.Stop();
        IsPlayBattleMusic = false;
        CanPlayWinningMusic = false;
        DeathSound.Play();
    }


    //SFX Enemies---------------------------------------------------------------------SFX Enemies
    public void PlayEnemySlashSound()
    {
        ChangeAudioPitch(EnemySlashSFX);
        EnemySlashSFX.Play();
    }

    public void PlayEnemyBlobSlashSound()
    {
        ChangeAudioPitch(BlobAttackSFX);
        BlobAttackSFX.Play();
    }
    public void PlayEnemyArrowAttack()
    {
        ChangeAudioPitch(EnemyArrowShotSFX);
        EnemyArrowShotSFX.Play();
    }

    void ChangeAudioPitch(AudioSource sound)
    {
        int index = Random.Range(0, 2);
        if (index == 0)
            sound.pitch = 0.9f;
        if (index == 1)
            sound.pitch = 1f;
        if (index == 2)
            sound.pitch = 1.1f;
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
