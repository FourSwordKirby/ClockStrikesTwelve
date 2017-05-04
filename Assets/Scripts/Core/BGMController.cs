using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

    public AudioSource audioSrc;

	// Update is called once per frame
	public IEnumerator FadeTowards (float targetVolume, float duration=1.0f) {
        float timer = 0.0f;

        float startingVolume = audioSrc.volume;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            audioSrc.volume = Mathf.Lerp(startingVolume, targetVolume, timer / duration);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
