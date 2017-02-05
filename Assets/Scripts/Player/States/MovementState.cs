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

    override public void Execute()
    {
        Parameters.Directions potential_direction = Controls.getDirection();
        if (Parameters.isOppositeDirection(potential_direction, player.direction) || potential_direction == Parameters.Directions.Stop)
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
        else
            player.direction = potential_direction;

        if (Controls.lockonInputDown())
            player.toggleLockOn();

        player.anim.SetFloat("DirX", Mathf.Ceil(Parameters.getVector(player.direction).x));
        player.anim.SetFloat("DirY", Mathf.Ceil(Parameters.getVector(player.direction).y));
    }

    override public void FixedExecute()
    {
        player.selfBody.velocity = Parameters.getVector(Controls.getDirection()) * player.movementSpeed;
    }

    override public void Exit()
    {
        player.anim.SetBool("Walking", false);
        return;
    }
}
