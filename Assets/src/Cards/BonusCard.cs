using UnityEngine;

[CreateAssetMenu(fileName = "NewBonusCard", menuName = "Card/BonusCard")]
public class BonusCard : Card
{
    public RowType AffectedRow;
    public int BoostAmount;
    public float initialBoost = 0;
    public float boost = 0;

    public float GetCurrentBoost => boost;
    public float ModBoost { get => boost; set => boost = value; }

    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           float initialBoost,
                           Effect effect,
                           RowType affectedRow)
    {
        this.name = name;
        this.initialBoost = initialBoost;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        boost = initialBoost;
        Effect = effect;
        AffectedRow = affectedRow;
    }

    public void ApplyEffect(Board board)
    {
        switch (AffectedRow)
        {
            case RowType.Melee:
                ApplyEffectToRow(board.allyMeleeSlots);
                break;
            case RowType.Ranged:
                ApplyEffectToRow(board.allyRangedSlots);
                break;
            case RowType.Siege:
                ApplyEffectToRow(board.allySiegeSlots);
                break;
        }
    }

    private void ApplyEffectToRow(CardSlot[] slots)
    {
        foreach (var slot in slots)
        {
            if (slot.IsOccupied && slot.CurrentCard is UnitCard unitCard)
            {
                unitCard.ModDmg += BoostAmount;
            }
        }
    }
}