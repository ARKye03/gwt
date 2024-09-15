using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCard", menuName = "Card/UnitCard")]
public class UnitCard : Card
{
    public float initialDmg = 0;
    public float dmg = 0;

    public float GetCurrentDMG => dmg;
    public float ModDmg { get => dmg; set => dmg = value; }
    public TypeofUnit typeofUnit { get; private set; }

    public Rarity Rarity { get; private set; }

    public Sprite TypeOfUnitImage { get; private set; } // Add this property

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Rarity rarity,
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
        Rarity = rarity;
        dmg = initialDmg;
        Effect = effect;
        this.typeofUnit = typeofUnit;

        // Load the appropriate TypeOfUnit image
        TypeOfUnitImage = Resources.Load<Sprite>($"TypeOfUnit/{typeofUnit}");
    }
}