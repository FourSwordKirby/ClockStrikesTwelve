using UnityEngine;
using System.Collections;

public class Controls {

    public static Vector2 getDirection()
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Keyboard Horizontal")))
            xAxis = Input.GetAxis("Horizontal");
        else
            xAxis = Input.GetAxis("Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Keyboard Vertical")))
            yAxis = Input.GetAxis("Vertical");
        else
            yAxis = Input.GetAxis("Keyboard Vertical");

        return new Vector2(xAxis, yAxis);
    }

    public static Parameters.InputDirection getInputDirection()
    {
        return Parameters.vectorToDirection(getDirection());
    }
    
    public static bool DirectionDown(Parameters.InputDirection dir)
    {
        //Hacky, probably should fix to be correct later
        Parameters.InputDirection currentInput = getInputDirection();
        return currentInput == dir;
    }

    public static bool confirmInputDown()
    {
        return Input.GetButtonDown("Confirm");
    }

    public static bool cancelInputDown()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool confirmInputHeld()
    {
        return Input.GetButton("Confirm");
    }

    public static bool cancelInputHeld()
    {
        return Input.GetButton("Cancel");
    }

    public static bool pauseInputDown()
    {
        return Input.GetButtonDown("Pause");
    }

    public static bool pauseInputHeld()
    {
        return Input.GetButton("Pause");
    }
}
