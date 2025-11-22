using UnityEngine;
using Unity.Netcode;

public class CardMoveManager : MonoBehaviour
{
    public static CardMoveManager Instance;

    public Transform player1PlayedContent;
    public Transform player2PlayedContent;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveCardForBothPlayers(string actorPlayerId, int cardId)
    {
        string localPlayerId =
            NetworkManager.Singleton.LocalClientId == 0 ? "P1" : "P2";

        // ✅ Always move card into the ACTOR's played area
        Transform targetParent =
            actorPlayerId == "P1" ? player1PlayedContent : player2PlayedContent;

        if (actorPlayerId == localPlayerId)
        {
            // LOCAL PLAYER → Move real card
            MoveLocalCard(cardId, targetParent);
        }
        else
        {
            // REMOTE PLAYER → Create visual card
            SpawnRemoteCard(cardId, targetParent);
        }
    }

    private void MoveLocalCard(int cardId, Transform targetParent)
    {
        CardClickHandler[] cards = FindObjectsOfType<CardClickHandler>();

        foreach (var card in cards)
        {
            if (card.CardId == cardId)
            {
                card.transform.SetParent(targetParent, false);
                return;
            }
        }

        Debug.LogWarning("Local card not found to move!");
    }

    private void SpawnRemoteCard(int cardId, Transform targetParent)
    {
        CardData data =
            DeckManager.Instance.AllCards.Find(c => c.id == cardId);
        if (data == null) return;

        PlayedCardsUIManager.Instance.AddPlayedCardPreview(targetParent, data);
    }
}
