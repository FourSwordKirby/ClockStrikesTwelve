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
        player.direction = Parameters.vectorToDirection(movementVector);

        player.anim.SetFloat("DirX", Mathf.Ceil(movementVector.x));
        player.anim.SetFloat("DirY", Mathf.Ceil(movementVector.y));

        Parameters.InputDirection input_direction = Controls.getInputDirection();
        if (input_direction == Parameters.InputDirection.None)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
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
