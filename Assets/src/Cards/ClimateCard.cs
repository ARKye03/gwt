using UnityEngine;

[CreateAssetMenu(fileName = "NewClimateCard", menuName = "Card/ClimateCard")]
public class ClimateCard : Card, IEffectRow
{
    public RowType AffectedRow;
    public int ClimatePower;
    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Effect effect,
                           RowType affectedRow,
                           int climatePower)
    {
        Name = name;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        Effect = effect;
        AffectedRow = affectedRow;
        ClimatePower = climatePower;
    }

    public bool ApplyEffect()
    {
        Player allyPlayer = Board._instance.allyPlayer;
        Player enemyPlayer = Board._instance.enemyPlayer;

        CardSlot[] allySlots = GetSlotsForRow(AffectedRow, allyPlayer);
        CardSlot[] enemySlots = GetSlotsForRow(AffectedRow, enemyPlayer);

        if (allySlots != null && enemySlots != null)
        {
            ApplyEffectToRow(allySlots);
            ApplyEffectToRow(enemySlots);
            return true;
        }
        return false;
    }

    private CardSlot[] GetSlotsForRow(RowType rowType, Player player)
    {
        return rowType switch
        {
            RowType.Melee => player.MeleeSlots,
            RowType.Ranged => player.RangedSlots,
            RowType.Siege => player.SiegeSlots,
            _ => null,
        };
    }

    public void ApplyEffectToRow(CardSlot[] slots)
    {
        foreach (CardSlot slot in slots)
        {
            if (slot.IsOccupied && slot.CurrentCard is UnitCard unitCard)
            {
                unitCard.power -= ClimatePower;
                if (unitCard.power < 0)
                {
                    unitCard.power = 0;
                }

                Debug.Log($"Decreased power of {unitCard.Name} to {unitCard.power}");
            }
        }
    }
}