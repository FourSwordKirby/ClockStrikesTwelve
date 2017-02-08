using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

    public AudioSource audio;

	// Update is called once per frame
	public IEnumerator FadeTowards (float targetVolume, float duration=1.0f) {
        float timer = 0;

        float startingVolume = audio.volume;
        while(timer < duration)
        {
            audio.volume = Mathf.Lerp(startingVolume, targetVolume, timer / duration);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
