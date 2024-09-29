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