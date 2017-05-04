using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{

    public GameObject HourHand;
    public GameObject MinuteHand;

    public Image ClockBase;

    // Update is called once per frame
    void Update()
    {
        float timeElapsedRatio = GameManager.instance.currentTime / GameManager.instance.timeLimit;

        int currentMinute = (int) (timeElapsedRatio * 1080); //18 playable game hours a day, 60 minutes an hour

        float currentHourAngle = ((timeElapsedRatio * 1.5f) % 1);
        float currentMinuteAngle = ((currentMinute % 60) / 60.0f);
        HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * (currentHourAngle * (-360) + 90)),
                                                                Quaternion.Euler(Vector3.forward * ((float) (currentHourAngle * (-360) - 0.25))),
                                                                0.01f);
        MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * (currentMinuteAngle * (-360) - 90)),
                                                                Quaternion.Euler(Vector3.forward * ((float)(currentMinuteAngle * (-360) - 0.25))),
                                                                0.01f);

        if (GameManager.instance.dayPhase > 3)
            ClockBase.color = Color.red;
        else if (GameManager.instance.dayPhase == 2)
            ClockBase.color = Color.grey;
        else
            ClockBase.color = Color.white;
        /*
        HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                timeElapsedRatio / 0.125f);

        HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                    Quaternion.Euler(Vector3.forward * -360),
                                                    timeElapsedRatio);*/
        //MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
        //                                                Quaternion.Euler(Vector3.forward * -90),
        //                                                timeElapsedRatio * 60 / 0.125f);
        /*
        if (timeElapsedRatio < 0.125f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                timeElapsedRatio / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                            Quaternion.Euler(Vector3.forward * -90),
                                                            timeElapsedRatio * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.25f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                    Quaternion.Euler(Vector3.forward * -180),
                                                                    (timeElapsedRatio - 0.125f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                    Quaternion.Euler(Vector3.forward * -180),
                                                                    (timeElapsedRatio - 0.125f) * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.375f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.25f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.25f) * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.5f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.375f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.375f) * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.625f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                (timeElapsedRatio - 0.5f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * 0),
                                                                Quaternion.Euler(Vector3.forward * -90),
                                                                (timeElapsedRatio - 0.5f) * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.75f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                Quaternion.Euler(Vector3.forward * -180),
                                                                (timeElapsedRatio - 0.625f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -90),
                                                                Quaternion.Euler(Vector3.forward * -180),
                                                                (timeElapsedRatio - 0.625f) * 60 / 0.125f);
        }
        else if (timeElapsedRatio < 0.875f)
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.75f) / 0.125f);
            MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -180),
                                                                Quaternion.Euler(Vector3.forward * -270),
                                                                (timeElapsedRatio - 0.75f) * 60 / 0.125f);
        }
        else
        {
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.875f) / 0.125f);
            HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * -270),
                                                                Quaternion.Euler(Vector3.forward * 0),
                                                                (timeElapsedRatio - 0.875f) * 60 / 0.125f);
        }
        */
    }
}
