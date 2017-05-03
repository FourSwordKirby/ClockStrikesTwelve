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
        GameManager.instance.startLoop("Walk");
        return;
    }


    float sfxTimer;
    override public void Execute()
    {
        Vector2 movementVector = Controls.getDirection();

        Parameters.InputDirection input_direction = Controls.getInputDirection();
        if (input_direction == Parameters.InputDirection.None)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
        }
        else
        {
            player.direction = Parameters.vectorToDirection(movementVector);

            if(movementVector.x != 0)
                player.anim.SetFloat("DirX", Mathf.Sign(movementVector.x));
            else
                player.anim.SetFloat("DirX", 0);

            if (movementVector.y != 0)
                player.anim.SetFloat("DirY", Mathf.Sign(movementVector.y));
            else
                player.anim.SetFloat("DirY", 0);

        }
    }

    override public void FixedExecute()
    {
        Vector2 movementVector = Controls.getDirection();
        player.selfBody.velocity = movementVector * player.movementSpeed;
    }

    override public void Exit()
    {
        player.anim.SetBool("Walking", false);
        GameManager.instance.stopLoop("Walk");
        return;
    }
}
