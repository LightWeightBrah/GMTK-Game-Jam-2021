using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Card Item", menuName = "Cards/Items")]
public class CardItem : ScriptableObject
{
    public string cardSound;

    public Sprite cardIcon;

    public int damage;
}
