using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<CardCrafting> possibleCrafts = new List<CardCrafting>();

    private List<CardItem> cards = new List<CardItem>();

    public void AddItem(CardItem card)
    {
        cards.Add(card);
    }

    public void RemoveAllItems()
    {
        cards = new List<CardItem>();
    }

    public void CraftFinalCard()
    {
        foreach(CardCrafting cardCrafting in possibleCrafts)
        {
            if(cardCrafting.itemsToCraft.Count == cards.Count)
            {
                int itemsCount = 0;

                for (int i = 0; i < cards.Count; i++)
                {
                    if(cardCrafting.itemsToCraft.Contains(cards[i]))
                    {
                        itemsCount++;
                    }
                }

                if(itemsCount == cards.Count)
                {
                    //Crafted
                    Debug.Log("Crafted " + cardCrafting.craftedItem);
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent<CardDrag>(out CardDrag cardDrag))
            {
                cardDrag.gameObject.SetActive(false);
                cards.Add(cardDrag.cardItem);
            }
        }
    }
}
