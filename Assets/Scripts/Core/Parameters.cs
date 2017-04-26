using UnityEngine;
using System.Collections;

//Enums for various player things
public enum Symbol
{
    Interested
}

public enum ItemDesignation
{
    WeatherCharm,
    Soda,
    Coin
}

public enum SoundType
{
    Menu,
    Environment,
    Item
}

public class Parameters : MonoBehaviour
{

    public enum InputDirection
    {
        W,
        NW,
        N,
        NE,
        E,
        SE,
        S,
        SW,
        None
    };
    public static InputDirection vectorToDirection(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero)
            return Parameters.InputDirection.None;

        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

        if (angle >= -22.5 && angle < 22.5)
        {
            return Parameters.InputDirection.E;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            return Parameters.InputDirection.NE;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            return Parameters.InputDirection.N;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            return Parameters.InputDirection.NW;
        }
        else if (angle >= 157.5 || angle < -157.5)
        {
            return Parameters.InputDirection.W;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            return Parameters.InputDirection.SW;
        }
        else if (angle >= -112.5 && angle < -67.5)
        {
            return Parameters.InputDirection.S;
        }
        else if (angle >= -67.5 && angle < -22.5)
        {
            return Parameters.InputDirection.SE;
        }
        return Parameters.InputDirection.None;
    }
}
