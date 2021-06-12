using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState
{
    PlayerTurn,
    PlayerAction,

    EnemyTurn,
    EnemyAction,

    Won,
    Lost
}
public class TurnBase : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerStart;
    public Transform enemyStart;

    public BattleState battleState = BattleState.PlayerTurn;
    public int turnCounter;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    private void Start()
    {
        StartBattle();
    }

    void StartBattle()
    {
        GameObject player = Instantiate(playerPrefab, playerStart);
        //playerUnit=player.GetComponent<>();

        GameObject enemy = Instantiate(enemyPrefab, enemyStart);

        // playerHud.HudSettings();
        // enemyHud.HudSettings();

        battleState = BattleState.PlayerTurn;
        PlayerTurn();
    }
    private void PlayerTurn()
    {
        //Player Add cards
        //bool isDead = Enemy.TakeDamage(player.damgae)
        Debug.Log("PlayerT");

    }


    public void PlayerTurnEnd()
    {
        if (battleState == BattleState.PlayerTurn)
        {
            //PlayerTurnEnd
            //enemyHud.SetHp(enemy.currentHP);

            Debug.Log("PlayerEndTurn");
            /*
            if (isDead)
            {
                battleState = BattleState.Won;
                EndBattle();
            }
            else
            {
                battleState = BattleState.EnemyTurn;
                EnemyTurn();
            }
            */

        }
    }
    private void EnemyTurn()
    {
        //EnemyTurn
        //bool isDead =player.TakeDamage(enemy.damage);
        // playerHud.SetHp(player.currentHP);

    }
    public void EnemyTurnEnd()
    {
        if (battleState == BattleState.EnemyTurn)
        {
            //EnemyTurnEnd
            /*
            if (isDead)
            {
                battleState = BattleState.Lost;
                EndBattle();
            }
            else
            {
                battleState = BattleState.PlayerTurn;
                PlayerTurn();
            }
            */            
        }

    }

    void EndBattle()
    {
        if (battleState == BattleState.Won)
        {
            //win
        }
        else if (battleState == BattleState.Lost)
        {

        }
    }
}
