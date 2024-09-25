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
}