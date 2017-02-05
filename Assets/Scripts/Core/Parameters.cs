using UnityEngine;
using System.Collections;

//Enums for various player things
public enum Symbol
{
    Interested
}

public enum ItemDesignation
{
    WeatherCharm
}

public class Parameters : MonoBehaviour {

    public enum Directions
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        Stop
    };

    //Do we need this?
    public enum PlayerStatus
    {
        Default, //Normal everyday state
        Immovable, //Not affected by forces
        Invulnerable, //Doesn't take damage, can be moved around (reduced knockback?)
        Invincible, //No damageno knockback
        Counter //Can initiate a counter attack
    };

    public enum DamageType
    {
        Basic,
        Blast
    }

    public enum Effect
    {
        None,
        Poison        
    }

    public static bool isOppositeDirection(Directions dir_1, Directions dir_2)
    {
        switch (dir_1)
        {
            case Directions.North: 
                return (dir_2 == Directions.SouthEast || dir_2 == Directions.South || dir_2 == Directions.SouthWest);
            case Directions.NorthEast: 
                return (dir_2 == Directions.West || dir_2 == Directions.South || dir_2 == Directions.SouthWest);
            case Directions.East: 
                return (dir_2 == Directions.NorthWest || dir_2 == Directions.West || dir_2 == Directions.SouthWest);
            case Directions.SouthEast: 
                return (dir_2 == Directions.NorthWest || dir_2 == Directions.West || dir_2 == Directions.North);
            case Directions.South: 
                return (dir_2 == Directions.NorthEast || dir_2 == Directions.North || dir_2 == Directions.NorthWest);
            case Directions.SouthWest: 
                return (dir_2 == Directions.East || dir_2 == Directions.North || dir_2 == Directions.NorthEast);
            case Directions.West: 
                return (dir_2 == Directions.SouthEast || dir_2 == Directions.East || dir_2 == Directions.NorthEast);
            case Directions.NorthWest: 
                return (dir_2 == Directions.SouthEast || dir_2 == Directions.South || dir_2 == Directions.East);
        }
        return false;
    }

    public static Directions getOppositeDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.North:
                return Directions.South;
            case Directions.NorthEast:
                return Directions.SouthWest;
            case Directions.East:
                return Directions.West;
            case Directions.SouthEast:
                return Directions.NorthWest;
            case Directions.South:
                return Directions.North;
            case Directions.SouthWest:
                return Directions.NorthEast;
            case Directions.West:
                return Directions.East;
            case Directions.NorthWest:
                return Directions.SouthEast;
        }
        return Directions.Stop;
    }

    public static Vector2 getVector(Directions dir)
    {
        switch (dir)
        {
            case Parameters.Directions.North:
                return new Vector2(0, 1);
            case Parameters.Directions.NorthEast:
                return new Vector2(Mathf.Sin(Mathf.PI / 2), Mathf.Sin(Mathf.PI / 2));
            case Parameters.Directions.East:
                return new Vector2(1, 0);
            case Parameters.Directions.SouthEast:
                return new Vector2(Mathf.Sin(Mathf.PI / 2), Mathf.Sin(3 * Mathf.PI / 2));
            case Parameters.Directions.South:
                return new Vector2(0, -1);
            case Parameters.Directions.SouthWest:
                return new Vector2(Mathf.Sin(3 * Mathf.PI / 2), Mathf.Sin(3 * Mathf.PI / 2));
            case Parameters.Directions.West:
                return new Vector2(-1, 0);
            case Parameters.Directions.NorthWest:
                return new Vector2(Mathf.Sin(3 * Mathf.PI / 2), Mathf.Sin(Mathf.PI / 2));
        }
        return Vector2.zero;
    }

    public static Directions getTargetDirection(GameObject player, GameObject target)
    {
        Vector2 playerPos = player.transform.position; 
        Vector2 targetPos = target.transform.position;

        Vector2 dir = targetPos - playerPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle >= -22.5 && angle < 22.5)
        {
            return Parameters.Directions.East;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            return Parameters.Directions.NorthEast;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            return Parameters.Directions.North;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            return Parameters.Directions.NorthWest;
        }
        else if (angle >= 157.5 || angle < -157.5)
        {
            return Parameters.Directions.West;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            return Parameters.Directions.SouthWest;
        }
        else if (angle >= -112.5 && angle < -67.5)
        {
            return Parameters.Directions.South;
        }
        else if (angle >= -67.5 && angle < -22.5)
        {
            return Parameters.Directions.SouthEast;
        }


        return Parameters.Directions.Stop;
    }
}
