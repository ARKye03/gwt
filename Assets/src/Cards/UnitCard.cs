public class UnitCard : Card
{
    public float initialDmg = 0;
    public float dmg = 0;

    public float GetCurrentDMG => dmg;
    public float ModDmg { get => dmg; set => dmg = value; }

    public Rarity Rarity { get; }

    public UnitCard(string name,
                    string description,
                    TypeofCard typeOfCard,
                    Faction faction,
                    Rarity rarity,
                    float initialDmg,
                    Effect effect,
                    bool isVoidCard = false) : base()
    {
        this.initialDmg = initialDmg;
        dmg = initialDmg;
        Rarity = rarity;
    }

}
