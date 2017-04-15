using UnityEngine;

public class Lamp : Interactable
{
    public bool isOn;
    public GameObject lamp;
    private SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;

    void Awake()
    {
        spriteRenderer = lamp.GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        // Put interact code here
        ToggleOn();
    }

    private void ToggleOn()
    {
        if (isOn)
        {
            spriteRenderer.sprite = offSprite;
            isOn = false;
        }
        else
        {
            spriteRenderer.sprite = onSprite;
            isOn = true;
        }
    }
}
