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
    [SerializeField] private Cutscenes cutscenes;
    [SerializeField] private CardPlaceholder cardPlaceholder;
    [SerializeField] private float waitTimeAfterAttack;
    [SerializeField] private int chanceForEnemyToMiss;

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
            bool isDead = enemy.TakeDamage(damage);

            if (isDead)
            {
                battleState = BattleState.Won;
                EndBattle();
            }
            else
            {
                battleState = BattleState.EnemyTurn;

                foreach (CardDrag card in cardPlaceholder.allCards)
                {
                    card.SetCooldown(card.cardCooldown - 1);
                    card.canUse = false;
                }

                StartCoroutine(EnemyTurnEnd());
            }
        }
    }

    public IEnumerator EnemyTurnEnd()
    {
        yield return new WaitForSeconds(waitTimeAfterAttack);

        Debug.Log("Enemy turn");

        if (battleState == BattleState.EnemyTurn)
        {
            int random = Random.Range(0, 100);
            bool isDead;

            if (random > chanceForEnemyToMiss)
            {
                int index = Random.Range(0, cardPlaceholder.possibleCrafts.allCraftingRecepies.Count);
                isDead = player.TakeDamage(cardPlaceholder.possibleCrafts.allCraftingRecepies[index].craftedItem.damage);
                //Enemy damage player
            }
            else
            {
                isDead = player.TakeDamage(0);
                //enemy miss
            }


            Debug.Log("dead " + isDead);

            if (isDead)
            {
                battleState = BattleState.Lost;
                EndBattle();
                Debug.Log("lost");
            }
            else
            {
                Debug.Log("player turn");
                yield return new WaitForSeconds(waitTimeAfterAttack);
                battleState = BattleState.PlayerTurn;

                foreach (CardDrag card in cardPlaceholder.allCards)
                    card.canUse = true;
            }
        }

    }

    private void EndBattle()
    {
        if (battleState == BattleState.Won)
        {
            Debug.Log("winning");
            cutscenes.PlayGame();
        }
        else if (battleState == BattleState.Lost)
        {
            Debug.Log("player has lost");
        }
    }
}
