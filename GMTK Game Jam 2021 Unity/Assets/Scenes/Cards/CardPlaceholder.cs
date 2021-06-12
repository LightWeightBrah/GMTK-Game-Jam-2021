using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<CardCrafting> possibleCrafts;

    private List<CardItem> cards = new List<CardItem>();
    private List<GameObject> cardsGameObjects = new List<GameObject>();

    private void Awake()
    {
        possibleCrafts = FindObjectOfType<PossibleCrafts>().allCraftingRecepies;
    }

    public void AddItem(CardItem card)
    {
        cards.Add(card);
    }

    public void RemoveAllItems()
    {
        foreach(GameObject cardGameObject in cardsGameObjects)
        {
            cardGameObject.SetActive(true);
        }

        cardsGameObjects = new List<GameObject>();
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
                cardsGameObjects.Add(cardDrag.gameObject);
                cards.Add(cardDrag.cardItem);

                cardDrag.gameObject.SetActive(false);
            }
        }
    }
}
