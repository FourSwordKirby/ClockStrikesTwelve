using UnityEngine;
using System.Collections;

public class FocalPoint : MonoBehaviour {

    GameObject target1;
    GameObject target2;

    public void setTargets(GameObject target1, GameObject target2)
    {
        this.target1 = target1;
        this.target2 = target2;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(target1 != null && target2 != null)
    	    this.transform.position = (target1.transform.position + target2.transform.position)/2;
	}
}
