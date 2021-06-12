using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
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
        foreach(GameObject cardGameObject in cardsGameObjects)
        {
            cardGameObject.SetActive(true);
        }

        foreach(Image icon in cardsIcons)
        {
            icon.sprite = defaultCardIcons;
        }

        cardsGameObjects = new List<GameObject>();
        cards = new List<CardItem>();
    }

    public void CraftFinalCard()
    {
        foreach(CardCrafting cardCrafting in possibleCrafts.allCraftingRecepies)
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

        RemoveAllItems();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent<CardDrag>(out CardDrag cardDrag))
            {
                
                AddCardToCrafting(cardDrag);
            }
        }
    }

    private void AddCardToCrafting(CardDrag cardDrag)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/card_deal", transform.position);
        cardsGameObjects.Add(cardDrag.gameObject);
        cards.Add(cardDrag.cardItem);

        cardDrag.gameObject.SetActive(false);

        cardDrag.ResetCard();

        cardsIcons[cards.Count - 1].sprite = cards[cards.Count - 1].cardIcon;
    }
}
