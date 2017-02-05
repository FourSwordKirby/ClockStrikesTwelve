using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour {

    public List<Image> choices;
    public int currentChoice;

    public bool choiceMade;

	// Update is called once per frame
	void Update () {
        for(int i = 0; i < choices.Count; i++)
        {
            if(i == currentChoice)
                choices[i].color = Color.green;
            else
                choices[i].color = Color.white;
        }
        //Change this
        if(Input.GetKeyDown(KeyCode.A))
        {
            currentChoice--;
            if (currentChoice < 0)
                currentChoice = choices.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentChoice++;
            if (currentChoice > choices.Count - 1)
                currentChoice = 0;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            choiceMade = true;
        }
    }

    public void OpenChoices(List<string> choices)
    {
        this.gameObject.SetActive(true);
        this.choiceMade = false;
    }

    public void CloseChoices()
    {
        this.gameObject.SetActive(false);
    }
}
