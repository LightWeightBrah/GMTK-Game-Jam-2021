using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBase : MonoBehaviour
{

    public int Turn=0;
    public int TurnCounter;

    void Update()
    {
        Turns();
    }


    private void Turns()
    {
        if (Turn==0)
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

        if (Turn==0)
        {
            //PlayerTurnEnd
     
            Turn++;
            Debug.Log("PlayerEndTurn");
        }
    }
    public void EnemyTurnEnd()
    {
        if (Turn==1)
        {
            //EnemyTurnEnd
            Turn--;
            TurnCounter++;
        }

    }
}
