using UnityEngine;

public delegate bool Effect(Player player);
interface IEffectRow
{
    bool ApplyEffect(RowType rowType);
    void ApplyEffectToRow(CardSlot[] slots);

}

/// <summary>
/// Represents a card in the game with various attributes such as name, description, type, faction, effect, and image.
/// </summary>
[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    /// <summary>
    /// Gets or sets the name of the card.
    /// </summary>
    public string Name;

    /// <summary>
    /// Gets or sets the description of the card.
    /// </summary>
    public string Description;

    /// <summary>
    /// Gets or sets a value indicating whether the card is a void card.
    /// </summary>
    public bool IsVoidCard;

    /// <summary>
    /// Gets or sets the type of the card.
    /// </summary>
    public TypeofCard TypeofCard;

    /// <summary>
    /// Gets or sets the faction of the card.
    /// </summary>
    public Faction Faction;

    /// <summary>
    /// Gets or sets the effect of the card.
    /// </summary>
    public Effect Effect;

    /// <summary>
    /// Gets or sets the image of the card.
    /// </summary>
    public Sprite CardImage;

    /// <summary>
    /// Gets or sets a value indicating whether the card can be played.
    /// </summary>
    public bool CanBePlayed { get; set; } = true;
}