using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCard", menuName = "Card/UnitCard")]
public class UnitCard : Card
{
    public int initialPower = 0;
    public int power = 0;

    public int GetCurrentDMG => power;
    public int ModDmg { get => power; set => power = value; }
    public TypeofUnit TypeofUnit { get; private set; }

    public Rarity Rarity { get; private set; }

    public Sprite TypeOfUnitImage { get; private set; }

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Rarity rarity,
                           int initialPower,
                           Effect effect,
                           TypeofUnit typeofUnit,
                           bool isVoidCard = false)
    {
        this.name = name;
        this.initialPower = initialPower;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        Rarity = rarity;
        power = initialPower;
        Effect = effect;
        TypeofUnit = typeofUnit;
        TypeOfUnitImage = Resources.Load<Sprite>($"TypeOfUnit/{typeofUnit}");
    }
}