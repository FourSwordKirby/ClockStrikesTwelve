using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//This is a generic NPC who can only say 1 line of dialog
public abstract class NPC : Interactable
{
    public SpriteRenderer spriteRenderer;

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        Parameters.InputDirection dir = PositionsToDirection(Player.instance.transform.position);
        if (dir == Parameters.InputDirection.N)
            spriteRenderer.sprite = upSprite;
        if (dir == Parameters.InputDirection.S)
            spriteRenderer.sprite = downSprite;
        if (dir == Parameters.InputDirection.W)
            spriteRenderer.sprite = leftSprite;
        if (dir == Parameters.InputDirection.E)
            spriteRenderer.sprite = rightSprite;
    }

    public Parameters.InputDirection PositionsToDirection(Vector2 speakerPos)
    {
        float xDifference = this.transform.position.x - speakerPos.x;
        float yDifference = this.transform.position.y - speakerPos.y;

        if (xDifference > yDifference)
        {
            if (xDifference < 0)
                return Parameters.InputDirection.E;
            else
                return Parameters.InputDirection.W;
        }
        else
        {
            if (yDifference < 0)
                return Parameters.InputDirection.N;
            else
                return Parameters.InputDirection.S;
        }
    }
}