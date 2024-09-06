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

    public List<UnitCard> CardsOfIdanai;
    public List<UnitCard> CardsOfCelai;
    public List<UnitCard> CardsOfYudivain;

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
        CardsOfIdanai = new List<UnitCard>
        {
            CreateUnitCard("Prasalaas", "How ARKye was supposed to be", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 10, null),
            CreateUnitCard("Prasanthrangan", "HyDe creator, where dots unleash the pain", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 8, null),
            CreateUnitCard("Aylur", "TSX is not the limit", TypeofCard.Unit, Faction.Idanai, Rarity.Gold, 8, null),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),
            CreateUnitCard("Kuruthi", "The one who can't be stopped", TypeofCard.Unit, Faction.Idanai, Rarity.Silver, 8, null),
        };

        CardsOfCelai = new List<UnitCard>
        {
            CreateUnitCard("Vinceliuice", "Designer Linuxer 来自中国, 喜欢用linux的设计师!", TypeofCard.Unit, Faction.Celai, Rarity.Gold, 10, null),
            CreateUnitCard("Pheralb", "The one who can't be stopped", TypeofCard.Unit, Faction.Celai, Rarity.Silver, 8, null),
            CreateUnitCard("Farther", "The one who can't be stopped", TypeofCard.Unit, Faction.Celai, Rarity.Silver, 8, null),
        };

        CardsOfYudivain = new List<UnitCard>
        {
            CreateUnitCard("Yudivain", "Yudivain", TypeofCard.Leader, Faction.Yudivain, Rarity.Gold, 0, null),
        };
    }

    private UnitCard CreateUnitCard(string name, string description, TypeofCard typeOfCard, Faction faction, Rarity rarity, float initialDmg, Effect effect)
    {
        UnitCard unitCard = ScriptableObject.CreateInstance<UnitCard>();
        unitCard.Initialize(name, description, typeOfCard, faction, rarity, initialDmg, effect);
        return unitCard;
    }
}