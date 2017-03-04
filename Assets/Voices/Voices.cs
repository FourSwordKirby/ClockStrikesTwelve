using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Voices : MonoBehaviour
{

    public static Voices instance;

    private AudioSource audioSource;
    private Coroutine stopAudioRoutine;
    
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
            PlayClip(Random.Range(0, 32));
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
}
