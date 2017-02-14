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
        Parameters.InputDirection input_direction = Controls.getInputDirection();
        if (input_direction != Parameters.InputDirection.None)
        {
            player.direction = input_direction;
            player.ActionFsm.ChangeState(new MovementState(player, player.ActionFsm));
        }
    }

    override public void FixedExecute(){    }

    override public void Exit(){    }
}
