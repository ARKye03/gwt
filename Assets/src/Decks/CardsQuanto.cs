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
            CreateLeaderCard("Idanai", "Idanai", Faction.Idanai, null, "Idanai.png"),
            CreateUnitCard("Prasalaas", "How ARKye was supposed to be", Faction.Idanai, Rarity.Gold, 10, null, "Prasalaas.png"),
            CreateUnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", Faction.Idanai, Rarity.Gold, 8, null, "Prasanthrangan.png"),
            CreateUnitCard("Aylur", "TSX is not the limit", Faction.Idanai, Rarity.Gold, 8, null, "Aylur.png"),

            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Kuruthi.png"),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Kuruthi.png"),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Kuruthi.png"),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Vikram.png"),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Vikram.png"),
            CreateUnitCard("Vikram", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Vikram.png"),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Arjun.png"),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Arjun.png"),
            CreateUnitCard("Arjun", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Arjun.png"),

            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Ravi.png"),
            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Ravi.png"),
            CreateUnitCard("Ravi", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Ravi.png"),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Suresh.png"),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Suresh.png"),
            CreateUnitCard("Suresh", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Suresh.png"),

            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null, "Decoy.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null, "Decoy.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Idanai, 0, null, "Decoy.png"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Melee, "Boost.png"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Idanai, 2, null, RowType.Ranged, "Boost.png"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Siege, "Boost.png"),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Idanai, null, RowType.Melee, "foo.png"),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Idanai, null, RowType.Melee, "foo.png.png"),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Idanai, null, RowType.Melee, "foo.png")
        };

        CardsOfCelai = new List<Card>
        {
            CreateLeaderCard("Celai", "Celai", Faction.Celai, null, "Celai.png"),
            CreateUnitCard("Vinceliuice", "Designer Linuxer 来自中国, 喜欢用linux的设计师!", Faction.Celai, Rarity.Gold, 10, null, "foo.png"),
            CreateUnitCard("Aurelius", "The golden warrior", Faction.Celai, Rarity.Gold, 9, null, "foo.png"),
            CreateUnitCard("Lysandra", "The fierce protector", Faction.Celai, Rarity.Gold, 8, null, "foo.png"),

            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Pheralb", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Farther", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "foo.png"),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "foo.png"),
            CreateUnitCard("Selene", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "foo.png"),

            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "foo.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "foo.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "foo.png"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Melee, "foo.png"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Celai, 2, null, RowType.Ranged, "foo.png"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Siege, "foo.png"),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Celai,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Celai,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Celai,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Celai,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Celai,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Celai,null, RowType.Melee, "something")
        };

        CardsOfYudivain = new List<Card>
        {
            CreateLeaderCard("Yudivain", "Yudivain", Faction.Yudivain, null, "foo.png"),
            CreateUnitCard("Eldor", "The ancient sage", Faction.Yudivain, Rarity.Gold, 10, null, "foo.png"),
            CreateUnitCard("Thalor", "The mighty warrior", Faction.Yudivain, Rarity.Gold, 9, null, "foo.png"),
            CreateUnitCard("Morgana", "The dark sorceress", Faction.Yudivain, Rarity.Gold, 8, null, "foo.png"),

            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "foo.png"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "foo.png"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "foo.png"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "foo.png"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "foo.png"),

            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "foo.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "foo.png"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "foo.png"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Melee, "foo.png"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Yudivain, 2, null, RowType.Ranged, "foo.png"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Siege, "foo.png"),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Yudivain,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Yudivain,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Yudivain,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Yudivain,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Yudivain,null, RowType.Melee, "foo.png"),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Yudivain,null, RowType.Melee, "foo.png")
        };


    }

    private Card CreateCard(string name, string description, Faction faction, Effect effect, string imageName)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.Name = name;
        card.Description = description;
        card.TypeofCard = TypeofCard.Unit;
        card.Faction = faction;
        card.Effect = effect;
        card.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return card;
    }

    private UnitCard CreateUnitCard(string name, string description, Faction faction, Rarity rarity, float initialDmg, Effect effect, string imageName)
    {
        UnitCard unitCard = ScriptableObject.CreateInstance<UnitCard>();
        unitCard.Initialize(name, description, TypeofCard.Unit, faction, rarity, initialDmg, effect);
        unitCard.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return unitCard;
    }

    private LeaderCard CreateLeaderCard(string name, string description, Faction faction, Effect effect, string imageName)
    {
        LeaderCard leaderCard = ScriptableObject.CreateInstance<LeaderCard>();
        leaderCard.Initialize(name, description, TypeofCard.Leader, faction, effect);
        leaderCard.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return leaderCard;
    }

    private BaitCard CreateBaitCard(string name, string description, Faction faction, float initialDmg, Effect effect, string imageName)
    {
        BaitCard baitCard = ScriptableObject.CreateInstance<BaitCard>();
        baitCard.Initialize(name, description, TypeofCard.Bait, faction, initialDmg, effect);
        baitCard.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return baitCard;
    }

    private ClimateCard CreateClimateCard(string name, string description, Faction faction, Effect effect, RowType affectedRow, string imageName)
    {
        ClimateCard climateCard = ScriptableObject.CreateInstance<ClimateCard>();
        climateCard.Initialize(name, description, TypeofCard.Climate, faction, 0, effect, affectedRow);
        climateCard.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return climateCard;
    }

    private BonusCard CreateBonusCard(string name, string description, Faction faction, float initialBoost, Effect effect, RowType targetRow, string imageName)
    {
        BonusCard bonusCard = ScriptableObject.CreateInstance<BonusCard>();
        bonusCard.Initialize(name, description, TypeofCard.Bonus, faction, initialBoost, effect, targetRow);
        bonusCard.CardImage = Resources.Load<Sprite>($"CardImages/{imageName}");
        return bonusCard;
    }
}