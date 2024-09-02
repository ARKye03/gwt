using UnityEngine;
using static Lib;
public abstract class Card : MonoBehaviour
{
    public string Name { get; }
    public string Description { get; }
    public bool IsVoidCard { get; }
    public TypeofCard TypeofCard { get; }
    public Faction Faction { get; }

    public delegate bool Effect(Scope scope);

    public Card(string name, string description, TypeofCard typeOfCard, Faction faction, bool isVoidCard = false)
    {
        Name = name;
        Description = description;
        IsVoidCard = isVoidCard;
        TypeofCard = typeOfCard;
        Faction = faction;
    }

}
