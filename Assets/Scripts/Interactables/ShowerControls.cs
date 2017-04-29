using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerControls : MonoBehaviour {

    public GameObject showerCurtain;
    public Transform startPosition;
    public Transform endPosition;

    private Vector3 midPosition;

    public float openTime;

	// Use this for initialization
	void Start () {
        midPosition = (startPosition.position + endPosition.position) / 2.0f;
	}

    public IEnumerator OpenCurtain()
    {
        float timer = 0.0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            if (timer < openTime)
            {
                showerCurtain.transform.position = Vector3.Lerp(midPosition, endPosition.position, timer / openTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
    }

    public IEnumerator CloseCurtain()
    {
        float timer = 0.0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            if (timer < openTime)
            {
                showerCurtain.transform.position = Vector3.Lerp(startPosition.position, midPosition, timer / openTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
    }
}
