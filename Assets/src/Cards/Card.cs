using UnityEngine;

public delegate bool Effect(Scope scope);

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;
    public bool IsVoidCard;
    public TypeofCard TypeofCard;
    public Faction Faction;
    public Effect Effect;
    public Sprite CardImage;
}