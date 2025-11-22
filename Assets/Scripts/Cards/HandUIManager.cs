using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HandUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform handParent;
    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        // ✅ When scene reloads (e.g., when Player2 joins),
        //    HandManager still has the cards, so just rebuild the UI.
        if (HandManager.Instance != null && HandManager.Instance.Hand.Count > 0)
        {
            ShowHand(HandManager.Instance.Hand);
        }
    }

    public void ShowHand(List<CardData> cards)
    {
        ClearHandUI();

        foreach (CardData card in cards)
        {
            GameObject cardObj = Instantiate(cardPrefab, handParent);
            SetupCardUI(cardObj, card);
        }
    }

    private void SetupCardUI(GameObject cardObj, CardData card)
    {
        TMP_Text[] texts = cardObj.GetComponentsInChildren<TMP_Text>();

        foreach (var t in texts)
        {
            if (t.name.Contains("Name"))
                t.text = card.name;

            if (t.name.Contains("Cost"))
                t.text = "Cost: " + card.cost;

            if (t.name.Contains("Power"))
                t.text = "Power: " + card.power;
        }
    }

    private void ClearHandUI()
    {
        for (int i = handParent.childCount - 1; i >= 0; i--)
        {
            Destroy(handParent.GetChild(i).gameObject);
        }
    }
}
