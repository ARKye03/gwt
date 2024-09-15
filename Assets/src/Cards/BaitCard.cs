using UnityEngine;

[CreateAssetMenu(fileName = "NewBaitCard", menuName = "Card/BaitCard")]
public class BaitCard : Card
{
    public float initialDmg = 0;
    public float dmg = 0;
    public TypeofUnit typeofUnit;

    public float GetCurrentDMG => dmg;
    public float ModDmg { get => dmg; set => dmg = value; }

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           float initialDmg,
                           Effect effect,
                            TypeofUnit typeofUnit,
                           bool isVoidCard = false)
    {
        this.name = name;
        this.initialDmg = initialDmg;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        dmg = initialDmg;
        Effect = effect;
        this.typeofUnit = typeofUnit;
        IsVoidCard = isVoidCard;
    }
}