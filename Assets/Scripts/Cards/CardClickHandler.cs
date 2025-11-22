using UnityEngine;
using Unity.Netcode;

public class CardClickHandler : MonoBehaviour
{
    private CardData card;
    public int CardId => card != null ? card.id : -1;

    public void Setup(CardData data)
    {
        card = data;
    }

    public void OnCardClicked()
    {
        if (card == null) return;

        string playerId =
            NetworkManager.Singleton.LocalClientId == 0 ? "P1" : "P2";

        string json =
            "{\"action\":\"playCard\",\"playerId\":\"" +
            playerId + "\",\"cardId\":" + card.id + "}";

        JsonMessageSender.Instance.Send(json);
    }
}
