using UnityEngine;

[CreateAssetMenu(fileName = "NewLeaderCard", menuName = "Card/LeaderCard")]
public class LeaderCard : Card
{
    public void Initialize(string name,
                           string description,
                           TypeofCard typeOfCard,
                           Faction faction,
                           Effect effect)
    {
        this.name = name;
        Description = description;
        TypeofCard = typeOfCard;
        Faction = faction;
        Effect = effect;
    }
}