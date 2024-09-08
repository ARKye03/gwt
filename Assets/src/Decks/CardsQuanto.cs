using System.Collections.Generic;
using UnityEngine;

public class CardsQuanto : MonoBehaviour
{
    private static CardsQuanto instance;

    public static CardsQuanto Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardsQuanto>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(CardsQuanto).Name);
                    instance = singleton.AddComponent<CardsQuanto>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    public List<Card> CardsOfIdanai;
    public List<Card> CardsOfCelai;
    public List<Card> CardsOfYudivain;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        InitializeCards();
    }

    private void InitializeCards()
    {
        CardsOfIdanai = new List<Card>
        {
            CreateLeaderCard("Idanai", "Idanai", Faction.Idanai, null),
            CreateUnitCard("Prasalaas", "How ARKye was supposed to be", Faction.Idanai, Rarity.Gold, 10, null),
            CreateUnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", Faction.Idanai, Rarity.Gold, 8, null),
            CreateUnitCard("Aylur", "TSX is not the limit", Faction.Idanai, Rarity.Gold, 8, null),

            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null),

            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null),
            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null),
            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null),

            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Idanai,null, RowType.Melee),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Idanai,null, RowType.Melee),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Idanai,null, RowType.Melee)
        };

        CardsOfCelai = new List<Card>
        {
            CreateLeaderCard("Celai", "Celai", Faction.Celai, null),
            CreateUnitCard("Vinceliuice", "Designer Linuxer 来自中国, 喜欢用linux的设计师!", Faction.Celai, Rarity.Gold, 10, null),
            CreateUnitCard("Aurelius", "The golden warrior", Faction.Celai, Rarity.Gold, 9, null),
            CreateUnitCard("Lysandra", "The fierce protector", Faction.Celai, Rarity.Gold, 8, null),

            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null),
            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null),
            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null),

            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Celai,null, RowType.Melee),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Celai,null, RowType.Melee),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Celai,null, RowType.Melee),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Celai,null, RowType.Melee),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Celai,null, RowType.Melee),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Celai,null, RowType.Melee)
        };

        CardsOfYudivain = new List<Card>
        {
            CreateLeaderCard("Yudivain", "Yudivain", Faction.Yudivain, null),
            CreateUnitCard("Eldor", "The ancient sage", Faction.Yudivain, Rarity.Gold, 10, null),
            CreateUnitCard("Thalor", "The mighty warrior", Faction.Yudivain, Rarity.Gold, 9, null),
            CreateUnitCard("Morgana", "The dark sorceress", Faction.Yudivain, Rarity.Gold, 8, null),

            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null),

            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Yudivain,null, RowType.Melee),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Yudivain,null, RowType.Melee),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Yudivain,null, RowType.Melee),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Yudivain,null, RowType.Melee),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Yudivain,null, RowType.Melee),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Yudivain,null, RowType.Melee)
        };
    }

    private UnitCard CreateUnitCard(string name, string description, Faction faction, Rarity rarity, float initialDmg, Effect effect)
    {
        UnitCard unitCard = ScriptableObject.CreateInstance<UnitCard>();
        unitCard.Initialize(name, description, TypeofCard.Unit, faction, rarity, initialDmg, effect);
        return unitCard;
    }

    private LeaderCard CreateLeaderCard(string name, string description, Faction faction, Effect effect)
    {
        LeaderCard leaderCard = ScriptableObject.CreateInstance<LeaderCard>();
        leaderCard.Initialize(name, description, TypeofCard.Leader, faction, effect);
        return leaderCard;
    }

    private BaitCard CreateBaitCard(string name, string description, Faction faction, float initialDmg, Effect effect)
    {
        BaitCard baitCard = ScriptableObject.CreateInstance<BaitCard>();
        baitCard.Initialize(name, description, TypeofCard.Bait, faction, initialDmg, effect);
        return baitCard;
    }

    private ClimateCard CreateClimateCard(string name, string description, Faction faction, Effect effect, RowType affectedRow)
    {
        ClimateCard climateCard = ScriptableObject.CreateInstance<ClimateCard>();
        climateCard.Initialize(name, description, TypeofCard.Climate, faction, 0, effect, affectedRow);
        return climateCard;
    }

    private BonusCard CreateBonusCard(string name, string description, Faction faction, float initialBoost, Effect effect, RowType targetRow)
    {
        BonusCard bonusCard = ScriptableObject.CreateInstance<BonusCard>();
        bonusCard.Initialize(name, description, TypeofCard.Bonus, faction, initialBoost, effect, targetRow);
        return bonusCard;
    }
}