using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
    [SerializeField] private TurnBase turnBase;
    public PossibleCrafts possibleCrafts;

    [SerializeField] private Sprite defaultCardIcons;
    [SerializeField] private Image[] cardsIcons;

    public CardDrag[] allCards;

    private List<CardDrag> cards = new List<CardDrag>();
    private List<GameObject> cardsGameObjects = new List<GameObject>();

    public void RemoveAllItems()
    {
        foreach (GameObject cardGameObject in cardsGameObjects)
        {
            cardGameObject.SetActive(true);
        }

        foreach (Image icon in cardsIcons)
        {
            icon.sprite = defaultCardIcons;
        }

        cardsGameObjects = new List<GameObject>();
        cards = new List<CardDrag>();
    }

    public void CraftFinalCard()
    {
        if(cards.Count == 0)
        {
            //Crafting with 0 cards
            //play sound for not being able to craft anything since you craft with 0 cards
            return;
        }

        foreach (CardCrafting cardCrafting in possibleCrafts.allCraftingRecepies)
        {
            if (cardCrafting.itemsToCraft.Count == cards.Count)
            {
                List<CardItem> tempCraft = new List<CardItem>();
                foreach(CardItem item in cardCrafting.itemsToCraft)
                {
                    tempCraft.Add(item);
                }

                List<CardDrag> registeredCards = new List<CardDrag>();

                for (int j = 0; j < cards.Count; j++)
                {
                    if (tempCraft.Contains(cards[j].cardItem))
                    {
                        registeredCards.Add(cards[j]);
                        tempCraft.Remove(cards[j].cardItem);
                    }
                }

                if (tempCraft.Count == 0)
                {
                    //Crafted
                    Debug.Log("Crafted " + cardCrafting.craftedItem);

                    foreach(CardDrag card in registeredCards)
                    {
                        card.SetCooldown(card.cardItem.cooldownAfterUse);
                    }

                    turnBase.PlayerTurnEnd(cardCrafting.craftedItem.damage);
                    RemoveAllItems();
                    return;
                }
            }
        }

        //Wrong craft dont deal damage
        RemoveAllItems();
        turnBase.PlayerTurnEnd(0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<CardDrag>(out CardDrag cardDrag))
            {

                AddCardToCrafting(cardDrag);
            }
        }
    }

    private void AddCardToCrafting(CardDrag cardDrag)
    {
        if (cardDrag.cardCooldown > 0 || !cardDrag.canUse)
            return;

        cardsGameObjects.Add(cardDrag.gameObject);
        cards.Add(cardDrag);

        cardDrag.gameObject.SetActive(false);

        cardDrag.ResetCard();

        cardsIcons[cards.Count - 1].sprite = cards[cards.Count - 1].cardItem.cardIcon;
    }
}
