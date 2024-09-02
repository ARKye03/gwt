using static Lib;
public class BaitCard : Card
{
    public BaitCard(string name,
                    string description,
                    TypeofCard typeOfCard,
                    Faction faction,
                    Effect effect,
                    bool isVoidCard = false) : base(name, description, typeOfCard, faction, effect, isVoidCard) { }
}