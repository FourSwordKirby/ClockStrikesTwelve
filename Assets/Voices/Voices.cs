using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Voices : MonoBehaviour
{

    public static Voices instance;

    private AudioSource audioSource;
    private Coroutine stopAudioRoutine;
    private Coroutine voiceRoutine;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartVoice(0, "");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopVoice();
        }
    }

    private IEnumerator StopAudioSource(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }

    private void playClip(int clipId)
    {
        if (stopAudioRoutine != null)
        {
            StopCoroutine(stopAudioRoutine);
        }

        audioSource.time = 0.5f * clipId;
        audioSource.Play();
        stopAudioRoutine = StartCoroutine(StopAudioSource(0.45f));
    }

    /// <summary>
    /// Plays a clip from the voices sound file. Long clips last for 0.25 seconds. Short clips last for 0.125 seconds.
    /// <para/>Clips 0 - 2:   Low voice (long)
    /// <para/>Clips 3 - 7:   Low voice(short)
    /// <para/>Clips 8 - 10:  Medium voice(long)
    /// <para/>Clips 11 - 15: Medium voice(short)
    /// <para/>Clips 16 - 19: High voice(long)
    /// <para/>Clips 20 - 32: High voice(short)
    /// </summary>
    /// <param name="clipId">The index of the clip to play</param>
    public static void PlayClip(int clipId)
    {
        if (instance != null)
        {
            instance.playClip(clipId);
        }
        else
        {
            Debug.LogError("There is no Voices prefab in the scene!");
        }
    }

    private IEnumerator VoiceRoutine(int speaker, string s)
    {
        Random.InitState(s.GetHashCode()); // Same strings should produce same sounding dialogue

        if (speaker == 0)
        {
            while (true)
            {
                int clip = Random.Range(0, 8);
                PlayClip(clip);
                yield return new WaitForSeconds(clip < 3 ? 0.26f : 0.135f);
            }
        }
        else if (speaker == 1)
        {
            while (true)
            {
                int clip = Random.Range(8, 16);
                PlayClip(clip);
                yield return new WaitForSeconds(clip < 11 ? 0.26f : 0.135f);
            }
        }
        else if (speaker == 2)
        {
            while (true)
            {
                int clip = Random.Range(16, 32);
                PlayClip(clip);
                yield return new WaitForSeconds(clip < 20 ? 0.26f : 0.135f);
            }
        }
    }

    /// <summary>
    /// Starts the speaking sound.
    /// </summary>
    /// <param name="speaker">0 - low<para/>1 - medium<para/>2 - high;</param>
    /// <param name="s">The dialogue that is being spoken</param>
    public static void StartVoice(int speaker, string s)
    {
        StopVoice();

        if (instance != null)
        {
            instance.voiceRoutine = instance.StartCoroutine(instance.VoiceRoutine(speaker, s));
        }
        else
        {
            Debug.LogError("There is no Voices prefab in the scene!");
        }
    }

    /// <summary>
    /// Stops the speaking sound.
    /// </summary>
    public static void StopVoice()
    {
        if (instance != null)
        {
            if (instance.voiceRoutine != null)
            {
                instance.StopCoroutine(instance.voiceRoutine);
            }
        }
        else
        {
            Debug.LogError("There is no Voices prefab in the scene!");
        }
    }
}
