using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [Range(0f, 1f)]
    public float MasterVolume;
    public List<AudioSource> AllAudioSources;
    public AudioSource TitleThemeSong;
    public AudioSource CampfireMusic;
    public bool IsPlayBattleMusic = false;
    public AudioSource BattleMusic;
    public bool IsPlayingIldeMusic = false;
    public AudioSource IdleBattleMusic;
    public bool CanPlayWinningMusic = false;
    public bool IsPlayingWinningMusic = false;
    public bool DidSkip = false;
    public AudioSource WinningRoundMusic;
    [Header("SFX")]
    [Header("Attacking SFX")]
    public AudioSource MeleeSlashSound;
    public AudioSource OtherIterationMeleeSlashSound;
    public AudioSource BlockSound;
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
    public AudioSource ClickOnButtonSFX;
    [Header("Script Refs")]
    public PlayerMeleeAttack PlayerMeleeAttackRef;
    IEnumerator _fadeOutBattleMusic;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        WinningRoundMusic.Stop();
        PlayMainThemeMusic();
        _fadeOutBattleMusic = FadeOutOfBattleSong(BattleMusic, WinningRoundMusic, 6f);
    }

    private void Update()
    {
        AdjustVolume(MasterVolume);
    }
    ///TITLE SONG STUFF
    void PlayMainThemeMusic() => TitleThemeSong.Play();
    public void StartCourtineFadeOutOfTitleThemeSong() => StartCoroutine(FadeOutOfTitleThemeSong());

    IEnumerator FadeOutOfTitleThemeSong()
    {
        yield return new WaitForSecondsRealtime(0.10f);
        TitleThemeSong.volume = MasterVolume;//whatever this max is
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
    public void StopPlayingWinningRoundSong()
    {
        StartCoroutine(SkipWinningMusic());
    }


    //Battle music stuff
    public void PlayBattleMusic()
    {
        IsPlayingIldeMusic = false;
        IsPlayBattleMusic = true;
        CanPlayWinningMusic = true;
        BattleMusic.volume = MasterVolume;
        BattleMusic.Play();
    }
    public void RaiseBattleMusicVolume() => StartCoroutine(RaiseBattleMusic());
    public void StartCorutineFadeOutBattleMusic() => StartCoroutine(FadeOutOfBattleSong(BattleMusic, WinningRoundMusic, 6f));
    IEnumerator RaiseBattleMusic()
    {
        IdleBattleMusic.volume = 0f;
        BattleMusic.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.05f);
        BattleMusic.volume = MasterVolume;
    }
    IEnumerator FadeOutOfBattleSong(AudioSource battleSong, AudioSource winningRoundSong, float lenghtofWinningSong)
    {
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = MasterVolume;//whatever this max is
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0.20f;
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0.15f;
        yield return new WaitForSecondsRealtime(0.10f);
        battleSong.volume = 0f;
        winningRoundSong.Play();
        IsPlayingWinningMusic = true;
        yield return new WaitForSecondsRealtime(lenghtofWinningSong);
        IsPlayingWinningMusic = false;
        if (!IsPlayingIldeMusic && !DidSkip)
        {
            Debug.Log("do i plauy");
            IdleBattleMusic.Play();
            IdleBattleMusic.volume = 0.10f;
            IsPlayingIldeMusic = true;
        }
        else if(IsPlayingIldeMusic && !DidSkip)
        {
            IdleBattleMusic.volume = 0.10f;
        }
        else if (DidSkip)
        {
            Debug.Log("do iiiii play");
            DidSkip = false;
        }
    }
    IEnumerator SkipWinningMusic()
    {
        DidSkip = true;
        WinningRoundMusic.Stop();
        yield return new WaitForSecondsRealtime(0.05f);
        BattleMusic.volume = MasterVolume;
    }

    //SFX---------------------------
    //Player SFX--------------------

    public void PlayBlockSound() => BlockSound.Play();
    public void PlayMeleeSoundEffect()
    {
        if (PlayerMeleeAttackRef.AmountOfAttacks == 1)
        {
            int index = Random.Range(0, 2);
            if(index == 0)
            {
                MeleeSlashSound.pitch = 0.9f;
                MeleeSlashSound.Play();
            }
            if(index >= 1)
                OtherIterationMeleeSlashSound.Play();
        }
        if (PlayerMeleeAttackRef.AmountOfAttacks == 2)
        {
            MeleeSlashSound.pitch = 1f;
            MeleeSlashSound.Play();
        }
        if (PlayerMeleeAttackRef.AmountOfAttacks >= 3)
        {
            MeleeSlashSound.pitch = 1.1f;
            MeleeSlashSound.Play();
        }
            

        
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


    ///MIS
   
    public IEnumerator PlayAbiliyPopUpSound()
    {
        WinningRoundMusic.volume = 0.10f;
        IdleBattleMusic.volume = 0f;
        BattleMusic.volume = 0f;
        AbilityPopUpSFX.Play();
        yield return new WaitForSecondsRealtime(2.5f);
        WinningRoundMusic.volume = MasterVolume;
        IdleBattleMusic.volume = 0.10f;
    }
    public void PlayAbilityPickedSound() => PickAbilitySFX.Play();
    public void PlayItemPickUpSound() => ItemPickUpSFX.Play();

    public void PlayClickedOnButtonSFX() => ClickOnButtonSFX.Play();
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
