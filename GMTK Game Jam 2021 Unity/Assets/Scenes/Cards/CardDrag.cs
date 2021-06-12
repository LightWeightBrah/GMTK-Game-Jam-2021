using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CardDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public CardItem cardItem;
    public int cardCooldown { get; set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI cooldownText;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;

    public bool canUse { get; set; } = true;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void SetCooldown(int cooldown)
    {
        cardCooldown = Mathf.Max(cooldown, 0);
        cooldownText.text = cardCooldown.ToString();
    }

    public void ResetCard()
    {
        rectTransform.anchoredPosition = startPosition;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardCooldown > 0 || !canUse)
            return;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cardCooldown > 0 || !canUse)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardCooldown > 0 || !canUse)
            return;

        ResetCard();
    }
    

}
