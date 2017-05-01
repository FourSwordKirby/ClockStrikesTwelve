using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    private float maxSize;
    private float originalPos;

    private Vector3 startPosition;

    void Start()
    {
        originalPos = gameObject.GetComponent<RectTransform>().anchoredPosition[1];
        maxSize = 2976;//gameObject.GetComponent<RectTransform>().rect.height / 2;
        print(maxSize);
        //startPosition = transform.position;
    }

    void Update()
    {
        Vector3 coordinates = gameObject.GetComponent<RectTransform>().anchoredPosition;
        float properYCoordinate = ((coordinates[1] + scrollSpeed - originalPos) % maxSize) + originalPos;
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(coordinates[0], properYCoordinate);
        //gameObject.GetComponent<RectTransform>().position[0] = 1000;
        //float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        //transform.position = startPosition + Vector3.forward * newPosition;
    }
}