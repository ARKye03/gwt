using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCard", menuName = "Card/UnitCard")]
public class UnitCard : Card
{
    public float initialDmg = 0;
    public float dmg = 0;

    public float GetCurrentDMG => dmg;
    public float ModDmg { get => dmg; set => dmg = value; }

    public Rarity Rarity { get; private set; }

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Rarity rarity,
                           float initialDmg,
                           Effect effect,
                           bool isVoidCard = false)
    {
        this.name = name;
        this.initialDmg = initialDmg;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        Rarity = rarity;
        dmg = initialDmg;
    }
}