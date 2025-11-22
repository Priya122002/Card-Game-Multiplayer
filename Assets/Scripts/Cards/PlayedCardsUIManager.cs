using UnityEngine;
using TMPro;

public class PlayedCardsUIManager : MonoBehaviour
{
    public static PlayedCardsUIManager Instance;

    [Header("Player Areas")]
    public Transform player1PlayedContent;
    public Transform player2PlayedContent;

    [Header("Card Prefab")]
    public GameObject cardPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayedCardPreview(Transform parent, CardData card)
    {
        GameObject cardObj = Instantiate(cardPrefab, parent);
        var texts = cardObj.GetComponentsInChildren<TMPro.TMP_Text>();

        foreach (var t in texts)
        {
            if (t.name.Contains("Name")) t.text = card.name;
            if (t.name.Contains("Cost")) t.text = "Cost: " + card.cost;
            if (t.name.Contains("Power")) t.text = "Power: " + card.power;
        }
    }

}
