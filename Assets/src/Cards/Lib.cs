using UnityEngine;

public class Lib : MonoBehaviour
{

    public enum TypeofUnit
    {
        Melee,
        Ranged,
        Siege
    }
    public enum Rarity // This ain't Fortinaiti mate, grow up
    {
        Gold,
        Silver
    }

    public enum TypeofCard
    {
        Unit,
        Bonus,
        Leader,
        Weather,
        Clear,
        Baity
    }
    public enum Faction
    {
        Idanai,
        Celai,
        Yudivain
    }
}
