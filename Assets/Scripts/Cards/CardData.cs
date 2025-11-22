using System.Collections.Generic;

[System.Serializable]
public class CardAbility
{
    public string type;
    public int value;
}

[System.Serializable]
public class CardData
{
    public int id;
    public string name;
    public int cost;
    public int power;
    public CardAbility ability;
}

[System.Serializable]
public class CardDatabase
{
    public List<CardData> cards;
}
