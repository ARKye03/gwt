/// <summary>
/// Contains the data that is passed between the main menu and the game scene.
/// </summary>
public static class GameData
{
    /// <summary>
    /// The index of the ally player's deck.
    /// </summary>
    public static int AllyPlayerDeckIndex { get; set; }
    /// <summary>
    /// The index of the enemy player's deck.
    /// </summary>
    public static int EnemyPlayerDeckIndex { get; set; }
}