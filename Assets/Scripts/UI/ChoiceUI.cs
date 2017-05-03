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
                choices[i].color = Color.grey;
            else
                choices[i].color = Color.white;
        }

        //Change this
        if(Controls.DirectionDown(Parameters.InputDirection.N))
        {
            currentChoice--;
            if (currentChoice < 0)
                currentChoice = choices.Count - 1;
            GameManager.instance.playSound(SoundType.Menu, "MenuToggle");
        }
        if (Controls.DirectionDown(Parameters.InputDirection.S))
        {
            currentChoice++;
            if (currentChoice > choices.Count - 1)
                currentChoice = 0;
            GameManager.instance.playSound(SoundType.Menu, "MenuToggle");
        }
        if (Controls.confirmInputDown())
        {
            choiceMade = true;
            GameManager.instance.playSound(SoundType.Menu, "MenuSelect");
        }
    }

    public void OpenChoices()
    {
        this.gameObject.SetActive(true);
        this.choiceMade = false;
    }

    public void CloseChoices()
    {
        this.gameObject.SetActive(false);
    }
}
