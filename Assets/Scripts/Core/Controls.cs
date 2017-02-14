using UnityEngine;
using System.Collections;

public class Controls {

    public static Vector2 getDirection()
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("P1 Horizontal")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Horizontal")))
            xAxis = Input.GetAxis("P1 Horizontal");
        else
            xAxis = Input.GetAxis("P1 Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("P1 Vertical")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Vertical")))
            yAxis = Input.GetAxis("P1 Vertical");
        else
            yAxis = Input.GetAxis("P1 Keyboard Vertical");

        return new Vector2(xAxis, yAxis);
    }

    public static Parameters.InputDirection getInputDirection()
    {
        return Parameters.vectorToDirection(getDirection());
    }

    static float cooldown = 0.1f;
    public static bool DirectionDown(Parameters.InputDirection dir)
    {
        //Hacky, probably should fix to be correct later
        if (cooldown < 0)
        {
            cooldown = 0.2f;
            Parameters.InputDirection currentInput = getInputDirection();
            return currentInput == dir;
        }
        else
        {
            cooldown -= Time.deltaTime;
            return false;
        }
    }

    public static bool confirmInputDown()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }

    public static bool cancelInputDown()
    {
        return Input.GetKeyDown(KeyCode.X);
    }

    public static bool confirmInputHeld()
    {
        return Input.GetKey(KeyCode.Z);
    }

    public static bool cancelInputHeld()
    {
        return Input.GetKey(KeyCode.X);
    }

    public static bool pauseInputDown()
    {
        return Input.GetKeyDown(KeyCode.P);
    }
}
