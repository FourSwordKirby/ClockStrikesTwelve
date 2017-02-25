using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEndSpook : MonoBehaviour {
    GameObject player;
    public float speed;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindObjectOfType<DayEndEvent>().playerHit();
    }
}
