using UnityEngine;
public abstract class Card : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsVoidCard { get; set; }
    public TypeofCard TypeofCard { get; set; }
    public Faction Faction { get; set; }
    public Effect Effect { get; set; }

    public Card(string name, string description, TypeofCard typeOfCard, Faction faction, Effect effect, bool isVoidCard = false)
    {
        Name = name;
        Description = description;
        IsVoidCard = isVoidCard;
        TypeofCard = typeOfCard;
        Faction = faction;
        Effect = effect;
    }

}
