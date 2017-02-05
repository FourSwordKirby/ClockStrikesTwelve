using UnityEngine;
using System.Collections;

public class IdleState : State<Player> {

    private Player player;

    public IdleState(Player playerInstance, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;
    }

    override public void Enter(){
        player.selfBody.velocity = Vector3.zero;
    }

    override public void Execute()
    {
        Parameters.Directions input_direction = Controls.getDirection();
        if (input_direction != Parameters.Directions.Stop)
        {
            player.direction = input_direction;
            player.ActionFsm.ChangeState(new MovementState(player, player.ActionFsm));
        }

        if (Controls.attackInputDown())
            player.ActionFsm.ChangeState(new AttackState(player, player.ActionFsm));

        if (Controls.lockonInputDown())
            player.toggleLockOn();

        if (player.target != null)
        {
            player.direction = Parameters.getTargetDirection(player.gameObject, player.target.gameObject);
            if (player.direction != Parameters.Directions.Stop)
            {
                player.anim.SetFloat("DirX", Mathf.Ceil(Parameters.getVector(player.direction).x));
                player.anim.SetFloat("DirY", Mathf.Ceil(Parameters.getVector(player.direction).y));
            }
        }
    }

    override public void FixedExecute(){    }

    override public void Exit(){    }
}
