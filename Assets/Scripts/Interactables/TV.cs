using UnityEngine;

public class TV : Interactable
{
    public bool isOn;
    public GameObject tv;
    private SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;

    void Awake()
    {
        spriteRenderer = tv.GetComponent<SpriteRenderer>();
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
