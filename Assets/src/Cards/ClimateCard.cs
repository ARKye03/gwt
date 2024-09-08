using UnityEngine;

[CreateAssetMenu(fileName = "NewClimateCard", menuName = "Card/ClimateCard")]
public class ClimateCard : Card
{
    public RowType AffectedRow;
    public int StrengthReduction;
    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Effect effect,
                           RowType affectedRow)
    {
        this.name = name;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        Effect = effect;
        AffectedRow = affectedRow;
    }

    public void ApplyEffect(Board board)
    {
        switch (AffectedRow)
        {
            case RowType.Melee:
                ApplyEffectToRow(board.allyMeleeSlots);
                ApplyEffectToRow(board.enemyMeleeSlots);
                break;
            case RowType.Ranged:
                ApplyEffectToRow(board.allyRangedSlots);
                ApplyEffectToRow(board.enemyRangedSlots);
                break;
            case RowType.Siege:
                ApplyEffectToRow(board.allySiegeSlots);
                ApplyEffectToRow(board.enemySiegeSlots);
                break;
        }
    }

    private void ApplyEffectToRow(CardSlot[] slots)
    {
        foreach (var slot in slots)
        {
            if (slot.IsOccupied && slot.CurrentCard is UnitCard unitCard)
            {
                unitCard.ModDmg -= StrengthReduction;
            }
        }
    }
}