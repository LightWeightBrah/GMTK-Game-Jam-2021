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
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Cutscenes cutscenes;
    [SerializeField] private CardPlaceholder cardPlaceholder;
    [SerializeField] private float waitTimeAfterAttack;
    [SerializeField] private int chanceForEnemyToMiss;

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
        battleState = BattleState.PlayerTurn;
    }

    public void PlayerTurnEnd(int damage)
    {
        if (battleState == BattleState.PlayerTurn)
        {
            bool isDead = enemy.TakeDamage(damage);

            if (damage == 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/miss_enemy");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/damage_enemy");
            }

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

        if (battleState == BattleState.EnemyTurn)
        {
            int random = Random.Range(0, 100);
            bool isDead;

            if (random > chanceForEnemyToMiss)
            {
                int index = Random.Range(0, cardPlaceholder.possibleCrafts.allCraftingRecepies.Count);
                isDead = player.TakeDamage(cardPlaceholder.possibleCrafts.allCraftingRecepies[index].craftedItem.damage);
                //Enemy damage player
                FMODUnity.RuntimeManager.PlayOneShot("event:/enemy_tune");
                FMODUnity.RuntimeManager.PlayOneShot("event:/damage_player");
            }
            else
            {
                isDead = player.TakeDamage(0);
                //enemy miss
                FMODUnity.RuntimeManager.PlayOneShot("event:/miss_player");
            }


            if (isDead)
            {
                battleState = BattleState.Lost;
                EndBattle();
            }
            else
            {
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
            //FMODUnity.RuntimeManager.PlayOneShot("event:/win");
            cutscenes.PlayGame();
        }
        else if (battleState == BattleState.Lost)
        {
            Debug.Log("player has lost");
            gameOverScreen.gameObject.SetActive(true);
        }
    }
}
