using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsQuanto : MonoBehaviour
{
    // needs to be a singleton <= idk wtf is this
    // Edit:
    /*
     * Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.
     */
    private readonly List<Card> CardsOfIdanai = new()
    {
        new LeaderCard("Idanai", "Idanai", TypeofCard.Leader, Faction.Idanai, null),
        new UnitCard("Prasalaas", "How ARKye was supposed to be", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 10, null),
        new UnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 8, null), // Hey if you read this, your dots are awesome mate
        new UnitCard("Aylur", "TSX is not the limit", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 8, null),

        new UnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),
        new UnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),
        new UnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),

    };
    private readonly List<Card> CardsOfCelai = new()
    {
        new LeaderCard("Celai", "Celai", TypeofCard.Leader, Faction.Celai, null),
        new UnitCard("Vinceliuice", "Designer Linuxer 来自中国, 喜欢用linux的设计师!", TypeofCard.Unit, Faction.Celai, Rarity.Gold, 10, null),
        //CREATE RANDOM CARDS
        new UnitCard("Pheralb", "The one who can't be stopped", TypeofCard.Unit, Faction.Celai, Rarity.Silver, 8, null),
        new UnitCard("Farther", "The one who can't be stopped", TypeofCard.Unit, Faction.Celai, Rarity.Silver, 8, null),
    };
    private readonly List<Card> CardsOfYudivain = new()
    {
        new LeaderCard("Yudivain", "Yudivain", TypeofCard.Leader, Faction.Yudivain, null),
    };
}
