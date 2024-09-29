using UnityEngine;

[CreateAssetMenu(fileName = "NewBonusCard", menuName = "Card/BonusCard")]
public class BonusCard : Card
{
    public RowType AffectedRow;
    public int BoostAmount;
    public int initialBoost = 0;
    public int boost = 0;

    public int GetCurrentBoost => boost;
    public int ModBoost { get => boost; set => boost = value; }

    public bool ApplyEffect(CardSlot[] rowType)
    {
        foreach (CardSlot item in rowType)
        {
            if (item.IsOccupied && item.CurrentCard is UnitCard unitCard)
            {
                unitCard.power += initialBoost;
            }
        }
        return true;
    }

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           int initialBoost,
                           Effect effect,
                           RowType affectedRow)
    {
        Name = name;
        this.initialBoost = initialBoost;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        boost = initialBoost;
        Effect = effect;
        AffectedRow = affectedRow;
    }

}