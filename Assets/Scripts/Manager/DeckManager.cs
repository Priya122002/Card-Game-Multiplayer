using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public List<CardData> AllCards = new List<CardData>();

    private void Awake()
    {
        Instance = this;
        LoadCards();
    }

    private void LoadCards()
    {
        TextAsset json = Resources.Load<TextAsset>("cards");

        if (json == null)
        {
            Debug.LogError("cards.json not found in Resources folder");
            return;
        }

        CardDatabase database = JsonUtility.FromJson<CardDatabase>(json.text);

        if (database == null || database.cards == null)
        {
            Debug.LogError("Card database failed to load.");
            return;
        }

        AllCards = database.cards;
        Debug.Log("Loaded cards: " + AllCards.Count);
    }

    public List<CardData> GetRandomCards(int count)
    {
        List<CardData> result = new List<CardData>();

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, AllCards.Count);
            result.Add(AllCards[rand]);
        }

        return result;
    }

}
