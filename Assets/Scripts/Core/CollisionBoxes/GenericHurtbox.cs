using UnityEngine;
using System.Collections;

/*
 * The hurtbox does not respond when it's hit by something, it only provides ways for the hitbox to do different things to the hitboxes
 * The reason for this is because Hurtboxes have more consistent and adaptable behavior compared to other hitboxes
 */
public class GenericHurtbox : Hurtbox {
    override public void TakeDamage(float damage)
    {
    }

    override public void TakeHit(float hitlag, float hitstun, Vector2 knockback, bool knockdown)
    {
    }

    override public void BlockHit(float hitlag, float hitstun, Vector2 knockback, bool knockdown)
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Hitbox hitbox = col.GetComponent<Hitbox>();
        if(hitbox != null && hitbox.owner != this.owner)
        {
            hitbox.Deactivate();

            Vector3 hitLocation = (this.transform.position + col.bounds.ClosestPoint(this.transform.position))/2.0f;
        }
    }
}
