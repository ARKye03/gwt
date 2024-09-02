public class LeaderCard : Card
{
    public LeaderCard(string name,
                      string description,
                      TypeofCard typeOfCard,
                      Faction faction,
                      Effect effect) : base(name, description, TypeofCard.Leader, faction, effect, false)
    {
    }
}
