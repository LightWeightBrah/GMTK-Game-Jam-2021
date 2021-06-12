using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBase : MonoBehaviour
{
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn
    }
    public BattleState battleState = BattleState.PlayerTurn;
    public int turnCounter;

    void Update()
    {
        Turns();
    }


    private void Turns()
    {
        if (battleState == BattleState.PlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }
    private void PlayerTurn()
    {
        //PlayerTurn

    }
    private void EnemyTurn()
    {
        //PlayerTurn

    }

    public void PlayerTurnEnd()
    {
        if (battleState == BattleState.PlayerTurn)
        {
            //PlayerTurnEnd

            battleState++;
            Debug.Log("PlayerEndTurn");
        }
    }
    public void EnemyTurnEnd()
    {
        if (battleState == BattleState.EnemyTurn)
        {
            //EnemyTurnEnd
            battleState--;
            turnCounter++;
        }

    }
}
