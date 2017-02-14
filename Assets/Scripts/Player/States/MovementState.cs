using UnityEngine;
using System.Collections;

public class MovementState : State<Player> {

    private Player player;

    public MovementState(Player playerInstance, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;
    }

    override public void Enter()
    {
        player.anim.SetBool("Walking", true);
        return;
    }


    float sfxTimer;
    override public void Execute()
    {
        if(sfxTimer < 1)
        {
            sfxTimer += Time.deltaTime;
            if(sfxTimer >= 1)
            {
                GameManager.instance.playSound(SoundType.Environment, "Walk");
                sfxTimer = 0;
            }
        }

        Vector2 movementVector = Controls.getDirection();
        player.direction = Parameters.vectorToDirection(movementVector);

        if (Controls.cancelInputDown())
            player.toggleLockOn();
        player.anim.SetFloat("DirX", Mathf.Ceil(movementVector.x));
        player.anim.SetFloat("DirY", Mathf.Ceil(movementVector.y));
    }

    override public void FixedExecute()
    {
        Vector2 movementVector = Controls.getDirection();
        player.selfBody.velocity = movementVector * player.movementSpeed;
    }

    override public void Exit()
    {
        player.anim.SetBool("Walking", false);
        return;
    }
}
