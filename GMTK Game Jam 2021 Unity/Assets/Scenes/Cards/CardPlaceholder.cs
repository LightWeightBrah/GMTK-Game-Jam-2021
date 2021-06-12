using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
    [SerializeField] private TurnBase turnBase;
    [SerializeField] private PossibleCrafts possibleCrafts;

    [SerializeField] private Sprite defaultCardIcons;
    [SerializeField] private Image[] cardsIcons;

    private List<CardItem> cards = new List<CardItem>();
    private List<GameObject> cardsGameObjects = new List<GameObject>();

    public void AddItem(CardItem card)
    {
        cards.Add(card);
    }

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
        cards = new List<CardItem>();
    }

    public void CraftFinalCard()
    {
        foreach (CardCrafting cardCrafting in possibleCrafts.allCraftingRecepies)
        {
            if (cardCrafting.itemsToCraft.Count == cards.Count)
            {
                List<CardItem> tempCraft = new List<CardItem>();
                foreach(CardItem item in cardCrafting.itemsToCraft)
                {
                    tempCraft.Add(item);
                }

                for (int i = 0; i < cards.Count; i++)
                {
                    if (tempCraft.Contains(cards[i]))
                    {
                        tempCraft.Remove(cards[i]);
                    }
                }

                if (tempCraft.Count == 0)
                {
                    //Crafted
                    Debug.Log("Crafted " + cardCrafting.craftedItem);
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
        cardsGameObjects.Add(cardDrag.gameObject);
        cards.Add(cardDrag.cardItem);

        cardDrag.gameObject.SetActive(false);

        cardDrag.ResetCard();

        cardsIcons[cards.Count - 1].sprite = cards[cards.Count - 1].cardIcon;
    }
}
