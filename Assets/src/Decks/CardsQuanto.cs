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
            CreateLeaderCard("Idanai", "Idanai", Faction.Idanai, null, "Idanai/Idanai"),
            CreateUnitCard("Prasalaas", "How ARKye was supposed to be", Faction.Neutral, Rarity.Gold, 15, null, "Neutral/Prasalaas"),
            CreateUnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", Faction.Idanai, Rarity.Gold, 8, null, "Idanai/ScarTheScorched"),
            CreateUnitCard("Aylur", "TSX is not the limit", Faction.Idanai, Rarity.Gold, 8, null, "Idanai/ColourlessSonic"),

            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Idanai/OtterSoldier"),
            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Idanai/OtterSoldier"),
            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null, "Idanai/OtterSoldier"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Idanai/PrimePeacock"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Idanai/PrimePeacock"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null, "Idanai/PrimePeacock"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Idanai/DracoBlue"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Idanai/DracoBlue"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null, "Idanai/DracoBlue"),

            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Idanai/Figures"),
            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Idanai/Figures"),
            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null, "Idanai/Figures"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Idanai/SpartanRaptor"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Idanai/SpartanRaptor"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null, "Idanai/SpartanRaptor"),

            CreateBaitCard("BaitFish", "A simple bait", Faction.Idanai, 0, null, "Idanai/BaitFish"),
            CreateBaitCard("BaitSe", "A simple bait", Faction.Idanai, 0, null, "Idanai/BaitSe"),
            CreateBaitCard("BaitSoldier", "A simple bait", Faction.Idanai, 0, null, "Idanai/BaitSoldier"),

            CreateBonusCard("Melee Boost", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Melee, "Idanai/Boost"),
            CreateBonusCard("Ranged Boost" , "Aumenta la fuerza de todas las unidades", Faction.Idanai, 2, null, RowType.Ranged, "Idanai/Boost"),
            CreateBonusCard("Siege Boost", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Siege, "Idanai/Boost"),

            CreateClimateCard("EndOfLight", "Reduces strength of melee units", Faction.Idanai, null, RowType.Melee, "Idanai/EndOfLight"),
            CreateClimateCard("AgainstTheCurrent", "Reduces strength of ranged units", Faction.Idanai, null, RowType.Ranged, "Idanai/AgainstTheCurrent"),
            CreateClimateCard("StormOfSiege", "Reduces strength of siege units", Faction.Idanai, null, RowType.Siege, "Idanai/StormOfSiege")
        };

        CardsOfCelai = new List<Card>
        {
            CreateLeaderCard("Celai", "Celai", Faction.Celai, null, "Celai/Celai"),
            CreateUnitCard("Vinceliuice", "Designer Linuxer", Faction.Celai, Rarity.Gold, 10, null, "Celai/Beauttyfly"), // It used to be "Designer Linuxer 来自中国, 喜欢用linux的设计师!" but yk this shit won't render unless I import this and man fr I need to find her :(
            CreateUnitCard("Aurelius", "The golden warrior", Faction.Celai, Rarity.Gold, 9, null, "Celai/Dratini"),
            CreateUnitCard("Lysandra", "The fierce protector", Faction.Celai, Rarity.Gold, 8, null, "Celai/GeometricalKitty"),

            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "Celai/GoldenGriffin"),
            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "Celai/GoldenGriffin"),
            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null, "Celai/GoldenGriffin"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "Celai/idk"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "Celai/idk"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null, "Celai/idk"),
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "Celai/LSDCaiman"), // Feeling skibidi atm
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "Celai/LSDCaiman"),
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null, "Celai/LSDCaiman"),

            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "Celai/"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "Celai/"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Celai, 0, null, "Celai/"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Melee, "Celai/"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Celai, 2, null, RowType.Ranged, "Celai/"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Siege, "Celai/"),

            CreateClimateCard("Zoore", "Reduces strength of melee units", Faction.Celai,null, RowType.Melee, "Celai/Zoore"),
            CreateClimateCard("DownTheArchers", "Reduces strength of ranged units", Faction.Celai,null, RowType.Ranged, "Celai/DownTheArchers"),
            CreateClimateCard("Weatherstorm", "Reduces strength of siege units", Faction.Celai,null, RowType.Siege, "Celai/Weatherstorm"),
        };

        CardsOfYudivain = new List<Card>
        {
            CreateLeaderCard("Yudivain", "Yudivain", Faction.Yudivain, null, "Yudivain/Yudivain"),
            CreateUnitCard("Eldor", "The ancient sage", Faction.Yudivain, Rarity.Gold, 10, null, "Yudivain/"),
            CreateUnitCard("Thalor", "The mighty warrior", Faction.Yudivain, Rarity.Gold, 9, null, "Yudivain/"),
            CreateUnitCard("Morgana", "The dark sorceress", Faction.Yudivain, Rarity.Gold, 8, null, "Yudivain/"),

            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "Yudivain/"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "Yudivain/"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null, "Yudivain/"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "Yudivain/"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "Yudivain/"),
            CreateUnitCard("Gareth", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null, "Yudivain/"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "Yudivain/"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "Yudivain/"),
            CreateUnitCard("Elara", "The healer", Faction.Yudivain, Rarity.Silver, 6, null, "Yudivain/"),

            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "Yudivain/"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "Yudivain/"),
            CreateBaitCard("Decoy", "A simple bait", Faction.Yudivain, 0, null, "Yudivain/"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Melee, "Yudivain/"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Yudivain, 2, null, RowType.Ranged, "Yudivain/"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Siege, "Yudivain/"),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Yudivain,null, RowType.Melee, "Yudivain/"),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Yudivain,null, RowType.Melee, "Yudivain/"),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Yudivain,null, RowType.Melee, "Yudivain/"),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Yudivain,null, RowType.Melee, "Yudivain/"),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Yudivain,null, RowType.Melee, "Yudivain/"),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Yudivain,null, RowType.Melee, "Yudivain/")
        };


    }

    private UnitCard CreateUnitCard(string name, string description, Faction faction, Rarity rarity, float initialDmg, Effect effect, string imageName)
    {
        UnitCard unitCard = ScriptableObject.CreateInstance<UnitCard>();
        unitCard.Initialize(name, description, TypeofCard.Unit, faction, rarity, initialDmg, effect);
        unitCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return unitCard;
    }

    private LeaderCard CreateLeaderCard(string name, string description, Faction faction, Effect effect, string imageName)
    {
        LeaderCard leaderCard = ScriptableObject.CreateInstance<LeaderCard>();
        leaderCard.Initialize(name, description, TypeofCard.Leader, faction, effect);
        leaderCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return leaderCard;
    }

    private BaitCard CreateBaitCard(string name, string description, Faction faction, float initialDmg, Effect effect, string imageName)
    {
        BaitCard baitCard = ScriptableObject.CreateInstance<BaitCard>();
        baitCard.Initialize(name, description, TypeofCard.Bait, faction, initialDmg, effect);
        baitCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return baitCard;
    }

    private ClimateCard CreateClimateCard(string name, string description, Faction faction, Effect effect, RowType affectedRow, string imageName)
    {
        ClimateCard climateCard = ScriptableObject.CreateInstance<ClimateCard>();
        climateCard.Initialize(name, description, TypeofCard.Climate, faction, effect, affectedRow);
        climateCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return climateCard;
    }

    private BonusCard CreateBonusCard(string name, string description, Faction faction, float initialBoost, Effect effect, RowType targetRow, string imageName)
    {
        BonusCard bonusCard = ScriptableObject.CreateInstance<BonusCard>();
        bonusCard.Initialize(name, description, TypeofCard.Bonus, faction, initialBoost, effect, targetRow);
        bonusCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return bonusCard;
    }
}