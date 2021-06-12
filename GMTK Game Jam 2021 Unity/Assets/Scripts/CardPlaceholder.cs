using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlaceholder : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent<CardDrag>(out CardDrag cardDrag))
            {
                cardDrag.gameObject.SetActive(false);
            }
        }
    }
}
