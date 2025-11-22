using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;

    public List<CardData> Hand = new List<CardData>();

    private static bool handAlreadyCreated = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (handAlreadyCreated)
        {
            RestoreUI();
            return;
        }

        DrawCardsForTurn(1);   // Start with Turn 1 rules
        handAlreadyCreated = true;
    }

    /// <summary>
    /// Turn based card generation
    /// </summary>
    public void DrawCardsForTurn(int turn)
    {
        Hand.Clear();

        // Unique random per player
        int seed = (int)NetworkManager.Singleton.LocalClientId * 10000
                 + DateTime.Now.Millisecond;

        System.Random rng = new System.Random(seed);

        // ✅ Guarantee at least one playable card (cost <= turn)
        List<CardData> validCards = DeckManager.Instance.AllCards
            .FindAll(c => c.cost <= turn);

        if (validCards.Count > 0)
        {
            Hand.Add(validCards[rng.Next(validCards.Count)]);
        }

        // ✅ Fill rest randomly
        while (Hand.Count < 3)
        {
            CardData card = DeckManager.Instance.AllCards[rng.Next(DeckManager.Instance.AllCards.Count)];
            Hand.Add(card);
        }

        // ✅ Shuffle
        Shuffle(Hand, rng);

        // ✅ Sort by COST low → high
        Hand.Sort((a, b) => a.cost.CompareTo(b.cost));

        RestoreUI();
    }

    /// <summary>
    /// Important shuffle (not skipped)
    /// </summary>
    private void Shuffle<T>(List<T> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void RestoreUI()
    {
        HandUIManager ui = FindObjectOfType<HandUIManager>();
        if (ui != null)
        {
            ui.ShowHand(Hand);
        }
    }
}
