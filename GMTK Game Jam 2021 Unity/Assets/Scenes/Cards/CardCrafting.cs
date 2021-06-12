using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Card crafting", menuName = "Cards/Crafting")]
public class CardCrafting : ScriptableObject
{
    public List<CardItem> itemsToCraft = new List<CardItem>();
    public CardItem craftedItem;
}
