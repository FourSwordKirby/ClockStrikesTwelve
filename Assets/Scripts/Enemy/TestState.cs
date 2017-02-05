using UnityEngine;
using System.Collections;

public class TestState : State<Enemy> {

    private Enemy enemy;

    public TestState(Enemy enemyInstance, StateMachine<Enemy> fsm):base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
    }

    override public void Enter()
    {
        return;
    }

    override public void Execute()
    {
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
