using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameMusicManager : MonoBehaviour
{
    public static GameMusicManager Instance = null;
    public static float EndTimerCountdownLength = 0;

    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip timerStartCountdown;
    [SerializeField] private AudioClip timerStart;
    [SerializeField] private AudioClip timerEndCountdown;
    [SerializeField] private AudioClip timerEnd;
    [SerializeField] private AudioClip carScreech;
    [SerializeField] private AudioClip ghostFly;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            EndTimerCountdownLength = timerEndCountdown.length;
        }
    }

    # region Public Functions
    
    // pauses the audio source
    public void PauseAudio()
    {
        audioPlayer.Pause();
    }

    // resumes the audio source
    public void ResumeAudio()
    {
        audioPlayer.UnPause();
    }

    // stops the audio source
    public void StopAudio()
    {
        audioPlayer.Stop();
    }
    
    // plays timer start countdown then timer start
    // returns length (seconds) of the clip as a float
    // should be played at the start of the round
    public float PlayCountdownAndStart()
    {
        StartCoroutine(PlaySound_StartCountdown_Start());
        return timerStartCountdown.length;
    }

    // plays timer end countdown
    // returns length (seconds) of the clip as a float
    // should be played when the remaining time == EndTimerCountdownLength
    public float PlayTimerEndCountdown()
    {
        PlayClip(timerEndCountdown);
        return timerEndCountdown.length;
    }

    // plays timer end and then car screech sound
    // returns total time (seconds) of timer end and car screech sounds as a float
    // should be played when the timer runs out (human wins)
    public float PlayTimerEndAndCarScreech()
    {
        StartCoroutine(PlaySound_TimerEnd_CarScreech());
        return timerEnd.length + carScreech.length;
    }
    
    // plays ghost fly sound
    // returns total time (seconds) of the clip as a float
    // should be played when flies win
    public float PlayGhostFly()
    {
        PlayClip(ghostFly);
        return ghostFly.length;
    }
    
    #endregion

    private IEnumerator PlaySound_StartCountdown_Start()
    {
        PlayClip(timerStartCountdown);
        yield return new WaitForSeconds(timerStartCountdown.length);
        PlayClip(timerStart);
    }
    
    private IEnumerator PlaySound_TimerEnd_CarScreech()
    {
        PlayClip(timerEnd);
        yield return new WaitForSeconds(timerEnd.length);
        PlayClip(carScreech);
    }

    private void PlayClip(AudioClip clip, bool loop = false)
    {
        audioPlayer.clip = clip;
        audioPlayer.loop = loop;
        audioPlayer.Play();
    }

}
