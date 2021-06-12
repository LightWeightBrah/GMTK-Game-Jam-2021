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
    [SerializeField] private float waitTimeAfterAttack;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerStart;
    [SerializeField] private Transform enemyStart;

    public BattleState battleState = BattleState.PlayerTurn;
    public int turnCounter;

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController enemy;

    private void Start()
    {
        StartBattle();
    }

    private void StartBattle()
    {
        //GameObject player = Instantiate(playerPrefab, playerStart);
        //GameObject enemy = Instantiate(enemyPrefab, enemyStart);

        battleState = BattleState.PlayerTurn;
    }

    public void PlayerTurnEnd(int damage)
    {
        if (battleState == BattleState.PlayerTurn)
        {
            Debug.Log("PlayerEndTurn");

            bool isDead = enemy.TakeDamage(damage);

            if (isDead)
            {
                battleState = BattleState.Won;
                EndBattle();
            }
            else
            {
                battleState = BattleState.EnemyTurn;
                StartCoroutine(EnemyTurnEnd());
            }
        }
    }

    public IEnumerator EnemyTurnEnd()
    {
        yield return new WaitForSeconds(waitTimeAfterAttack);

        if (battleState == BattleState.EnemyTurn)
        {
            bool isDead = player.TakeDamage(Random.Range(5, 15));

            if (isDead)
            {
                battleState = BattleState.Lost;
                EndBattle();
            }
            else
            {
                yield return new WaitForSeconds(waitTimeAfterAttack);
                battleState = BattleState.PlayerTurn;
            }
        }

    }

    void EndBattle()
    {
        if (battleState == BattleState.Won)
        {
            Debug.Log("player has won");
        }
        else if (battleState == BattleState.Lost)
        {
            Debug.Log("player has lost");
        }
    }
}
