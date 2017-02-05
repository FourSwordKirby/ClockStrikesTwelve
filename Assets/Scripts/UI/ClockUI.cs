using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour {

    public GameObject Hand;

    // Update is called once per frame
    void Update () {
        float timeElapsedRatio = GameManager.instance.currentTime / GameManager.instance.timeLimit;
        if (timeElapsedRatio < 0.125f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                timeElapsedRatio/0.125f);
        else if (timeElapsedRatio < 0.25f)
                Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                    Quaternion.Euler(Vector3.forward * -180), 
                                                                    (timeElapsedRatio- 0.125f) / 0.125f);
        else if (timeElapsedRatio < 0.375f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.25f) / 0.125f);
        else if (timeElapsedRatio < 0.5f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.375f) / 0.125f);
        else if (timeElapsedRatio < 0.625f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                (timeElapsedRatio - 0.5f) / 0.125f);
        else if (timeElapsedRatio < 0.75f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                Quaternion.Euler(Vector3.forward * -180),
                                                                (timeElapsedRatio - 0.625f) / 0.125f);
        else if (timeElapsedRatio < 0.875f)
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.75f) / 0.125f);
        else
            Hand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.875f) / 0.125f);
    }
}
