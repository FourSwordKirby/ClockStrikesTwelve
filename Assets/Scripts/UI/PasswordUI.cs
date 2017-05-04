using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordUI : MonoBehaviour {
    public InputField passwordText;

    public void openPassword()
    {
        this.gameObject.SetActive(true);
        passwordText.text = "";
    }

    public void closePassword()
    {
        this.gameObject.SetActive(false);
    }

    public string getAnswer()
    {
        return passwordText.text;
    }
}
