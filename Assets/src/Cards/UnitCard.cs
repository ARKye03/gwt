using UnityEngine;
using static Lib;

public class UnitCard : Card
{
    public float initialDmg = 0;
    public float dmg = 0;

    public float GetCurrentDMG => dmg;
    public float ModDmg { get => dmg; set => dmg = value; }

    public Rarity Rarity { get; }

}
