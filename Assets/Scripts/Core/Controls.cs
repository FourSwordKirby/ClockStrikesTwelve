using UnityEngine;
using System.Collections;

public class Controls {

    public static Parameters.Directions getDirection()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return Parameters.Directions.NorthWest;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            return Parameters.Directions.SouthWest;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            return Parameters.Directions.SouthEast;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            return Parameters.Directions.NorthEast;
        }
        else if (Input.GetKey(KeyCode.W)  && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return Parameters.Directions.North;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            return Parameters.Directions.West;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            return Parameters.Directions.South;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            return Parameters.Directions.East;
        }
        else
            return Parameters.Directions.Stop;
    }

    public static bool actionInputDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public static bool lockonInputDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public static bool attackInputDown()
    {
        return Input.GetMouseButtonDown(1);
    }


    public static bool actionInputHeld()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public static bool lockonInputHeld()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public static bool attackInputHeld()
    {
        return Input.GetMouseButton(1);
    }
}
