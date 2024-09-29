using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All cards in this singleton class
/// </summary>
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

    public Stack<Card> CardsOfIdanai;
    public Stack<Card> CardsOfCelai;
    public Stack<Card> CardsOfYudivain;

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
        CardsOfIdanai = new Stack<Card>(new List<Card>
        {

            CreateUnitCard("Prasalaas", "How ARKye was supposed to be", Faction.Neutral, Rarity.Gold, 15, null,TypeofUnit.Melee, "Neutral/Prasalaas"),
            CreateUnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", Faction.Idanai, Rarity.Gold, 8, null,TypeofUnit.Melee, "Idanai/ScarTheScorched"),
            CreateUnitCard("Aylur", "TSX is not the limit", Faction.Idanai, Rarity.Gold, 8, null,TypeofUnit.Melee, "Idanai/ColourlessSonic"),

            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Idanai/OtterSoldier"),
            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Idanai/OtterSoldier"),
            CreateUnitCard("OtterSoldier", "The one who can't be stopped", Faction.Idanai, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Idanai/OtterSoldier"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null,TypeofUnit.Melee, "Idanai/PrimePeacock"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null,TypeofUnit.Melee, "Idanai/PrimePeacock"),
            CreateUnitCard("PrimePeacock", "The strategist", Faction.Idanai, Rarity.Silver, 7, null,TypeofUnit.Melee, "Idanai/PrimePeacock"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Idanai/DracoBlue"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Idanai/DracoBlue"),
            CreateUnitCard("DracoBlue", "The silent warrior", Faction.Idanai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Idanai/DracoBlue"),

            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null,TypeofUnit.Ranged, "Idanai/Figures"),
            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null,TypeofUnit.Ranged, "Idanai/Figures"),
            CreateUnitCard("Figures", "The quick thinker", Faction.Idanai, Rarity.Silver, 5, null,TypeofUnit.Ranged, "Idanai/Figures"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null,TypeofUnit.Melee, "Idanai/SpartanRaptor"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null,TypeofUnit.Melee, "Idanai/SpartanRaptor"),
            CreateUnitCard("SpartanRaptor", "The shield", Faction.Idanai, Rarity.Silver, 4, null,TypeofUnit.Melee, "Idanai/SpartanRaptor"),

            CreateBaitCard("BaitFish", "A simple bait", Faction.Idanai, 0, null,TypeofUnit.Melee, "Idanai/BaitFish"),
            CreateBaitCard("BaitSe", "A simple bait", Faction.Idanai, 0, null,TypeofUnit.Melee, "Idanai/BaitSe"),
            CreateBaitCard("BaitSoldier", "A simple bait", Faction.Idanai, 0, null,TypeofUnit.Melee, "Idanai/BaitSoldier"),

            CreateBonusCard("Melee Boost", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Melee, "Idanai/Boost"),
            CreateBonusCard("Ranged Boost" , "Aumenta la fuerza de todas las unidades", Faction.Idanai, 2, null, RowType.Ranged, "Idanai/Boost"),
            CreateBonusCard("Siege Boost", "Boosts strength of all units", Faction.Idanai, 2, null, RowType.Siege, "Idanai/Boost"),

            CreateClimateCard("EndOfLight", "Reduces strength of melee units by 7", Faction.Idanai, null, RowType.Melee,7, "Idanai/EndOfLight"),
            CreateClimateCard("AgainstTheCurrent", "Reduces strength of ranged units by 5", Faction.Idanai, null, RowType.Ranged,5, "Idanai/AgainstTheCurrent"),
            CreateClimateCard("StormOfSiege", "Reduces strength of siege units by 9", Faction.Idanai, null, RowType.Siege,9, "Idanai/StormOfSiege"),

            CreateLeaderCard("Idanai", "Drop one random card and draw another from the deck", Faction.Idanai, Effects.DropAndDrawCard, "Idanai/Idanai"),
        });

        CardsOfCelai = new Stack<Card>(new List<Card>
        {

            CreateUnitCard("Vinceliuice", "Designer Linuxer", Faction.Celai, Rarity.Gold, 10, null,TypeofUnit.Ranged, "Celai/Beauttyfly"), // It used to be "Designer Linuxer 来自中国, 喜欢用linux的设计师!" but yk this shit won't render unless I import this and man fr I need to find her :(
            CreateUnitCard("Aurelius", "The golden warrior", Faction.Celai, Rarity.Gold, 9, null,TypeofUnit.Ranged, "Celai/Dratini"),
            CreateUnitCard("Lysandra", "The fierce protector", Faction.Celai, Rarity.Gold, 8, null,TypeofUnit.Ranged, "Celai/GeometricalKitty"),

            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null,TypeofUnit.Melee, "Celai/GoldenGriffin"),
            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null,TypeofUnit.Melee, "Celai/GoldenGriffin"),
            CreateUnitCard("GoldenGriffin", "The one who can't be stopped", Faction.Celai, Rarity.Silver, 8, null,TypeofUnit.Melee, "Celai/GoldenGriffin"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Celai/idk"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Celai/idk"),
            CreateUnitCard("SwampThing", "The wise elder", Faction.Celai, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Celai/idk"),
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Celai/LSDCaiman"), // Feeling skibidi atm
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Celai/LSDCaiman"),
            CreateUnitCard("LSDCaiman", "The moon guardian", Faction.Celai, Rarity.Silver, 6, null,TypeofUnit.Siege, "Celai/LSDCaiman"),

            CreateBaitCard("BaitCute", "A simple bait", Faction.Celai, 0, null,TypeofUnit.Melee, "Celai/BaitCute"),
            CreateBaitCard("BaitNature", "A simple bait", Faction.Celai, 0, null,TypeofUnit.Melee, "Celai/BaitNature"),
            CreateBaitCard("BaitThing", "A simple bait", Faction.Celai, 0, null,TypeofUnit.Melee, "Celai/BaitThing"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Melee, "Celai/"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Celai, 2, null, RowType.Ranged, "Celai/"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Celai, 2, null, RowType.Siege, "Celai/"),

            CreateClimateCard("Zoore", "Reduces strength of melee units", Faction.Celai,null, RowType.Melee,4, "Celai/Zoore"),
            CreateClimateCard("DownTheArchers", "Reduces strength of ranged units", Faction.Celai,null, RowType.Ranged,4, "Celai/DownTheArchers"),
            CreateClimateCard("Weatherstorm", "Reduces strength of siege units", Faction.Celai,null, RowType.Siege,4, "Celai/Weatherstorm"),

            CreateLeaderCard("Celai", "Drop all your hand and draw 10 more!", Faction.Celai, Effects.ReloadHand, "Celai/Celai"),
        });

        CardsOfYudivain = new Stack<Card>(new List<Card>
        {

            CreateUnitCard("Eldor", "The ancient sage", Faction.Yudivain, Rarity.Gold, 15, null,TypeofUnit.Siege, "Yudivain/StrongestGodzillaOfAllTimes"),
            CreateUnitCard("Thalor", "The mighty warrior", Faction.Yudivain, Rarity.Gold, 9, null,TypeofUnit.Siege, "Yudivain/WarAxolotl"),
            CreateUnitCard("Morgana", "The dark sorceress", Faction.Yudivain, Rarity.Gold, 8, null,TypeofUnit.Siege, "Yudivain/ShenLong"),

            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Yudivain/Lorian"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Yudivain/Lorian"),
            CreateUnitCard("Lorian", "The swift archer", Faction.Yudivain, Rarity.Silver, 8, null,TypeofUnit.Ranged, "Yudivain/Lorian"),
            CreateUnitCard("WarAxolotl", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Yudivain/WarAxolotl"),
            CreateUnitCard("WarAxolotl", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Yudivain/WarAxolotl"),
            CreateUnitCard("WarAxolotl", "The brave knight", Faction.Yudivain, Rarity.Silver, 7, null,TypeofUnit.Ranged, "Yudivain/WarAxolotl"),
            CreateUnitCard("ShenLong", "The healer", Faction.Yudivain, Rarity.Silver, 6, null,TypeofUnit.Melee, "Yudivain/ShenLong"),
            CreateUnitCard("ShenLong", "The healer", Faction.Yudivain, Rarity.Silver, 6, null,TypeofUnit.Melee, "Yudivain/ShenLong"),
            CreateUnitCard("ShenLong", "The healer", Faction.Yudivain, Rarity.Silver, 6, null,TypeofUnit.Melee, "Yudivain/ShenLong"),

            CreateBaitCard("BaitTurtwig", "A simple bait", Faction.Yudivain, 0, null,TypeofUnit.Melee, "Yudivain/BaitTurtwig"),
            CreateBaitCard("BaitTinkerbell", "A simple bait", Faction.Yudivain, 0, null,TypeofUnit.Melee, "Yudivain/BaitTinkerbell"),
            CreateBaitCard("BaitPuppy", "A simple bait", Faction.Yudivain, 0, null,TypeofUnit.Melee, "Yudivain/BaitPuppy"),

            CreateBonusCard("Boost", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Melee, "Yudivain/"),
            CreateBonusCard("AUMENTO" , "Aumenta la fuerza de todas las unidades", Faction.Yudivain, 2, null, RowType.Ranged, "Yudivain/"),
            CreateBonusCard("Boost Siege", "Boosts strength of all units", Faction.Yudivain, 2, null, RowType.Siege, "Yudivain/"),

            CreateClimateCard("Frost", "Reduces strength of melee units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),
            CreateClimateCard("Fog", "Reduces strength of ranged units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),
            CreateClimateCard("Rain", "Reduces strength of siege units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),
            CreateClimateCard("Storm", "Reduces strength of all units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),
            CreateClimateCard("Blizzard", "Freezes all units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),
            CreateClimateCard("Heatwave", "Burns all units", Faction.Yudivain,null, RowType.Melee,5, "Yudivain/"),

            CreateLeaderCard("Yudivain", "Yudivain", Faction.Yudivain, null, "Yudivain/Yudivain"),
        });


    }

    private UnitCard CreateUnitCard(string name, string description, Faction faction, Rarity rarity, int initialPower, Effect effect, TypeofUnit typeofUnit, string imageName)
    {
        UnitCard unitCard = ScriptableObject.CreateInstance<UnitCard>();
        unitCard.Initialize(name, description, TypeofCard.Unit, faction, rarity, initialPower, effect, typeofUnit);
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

    private BaitCard CreateBaitCard(string name, string description, Faction faction, float initialDmg, Effect effect, TypeofUnit typeofUnit, string imageName)
    {
        BaitCard baitCard = ScriptableObject.CreateInstance<BaitCard>();
        baitCard.Initialize(name, description, TypeofCard.Bait, faction, initialDmg, effect, typeofUnit);
        baitCard.CardImage = Resources.Load<Sprite>($"{imageName}");
        return baitCard;
    }

    private ClimateCard CreateClimateCard(string name, string description, Faction faction, Effect effect, RowType affectedRow, int climatePower, string imageName)
    {
        ClimateCard climateCard = ScriptableObject.CreateInstance<ClimateCard>();
        climateCard.Initialize(name, description, TypeofCard.Climate, faction, effect, affectedRow, climatePower);
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