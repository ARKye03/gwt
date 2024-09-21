using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// The Board class represents the game board in the Gwent game. It manages the state of the///  g/// ame, incl/// uding player decks, 
/// card slots, and player hands. It also handles the /// initialization of the game, shuffling of///  decks/// , and updating the 
/// visibility of player hand panels.
/// </summary>
public class Board : MonoBehaviour
{
    /// <summary>
    /// The main camera used to display the game board.
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// The deck of the ally player.
    /// </summary>
    public Deck allyDeck;

    /// <summary>
    /// The card slot for the climate card.
    /// </summary>
    public CardSlot climateSlot;

    /// <summary>
    /// The graveyard stack for the ally player.
    /// </summary>
    public Stack<Card> allyGraveyard = new();

    /// <summary>
    /// The melee card slots for the ally player.
    /// </summary>
    public CardSlot[] allyMeleeSlots;

    /// <summary>
    /// The bonus melee card slot for the ally player.
    /// </summary>
    public CardSlot allyMeleeBonusSlot;

    /// <summary>
    /// The ranged card slots for the ally player.
    /// </summary>
    public CardSlot[] allyRangedSlots;

    /// <summary>
    /// The bonus ranged card slot for the ally player.
    /// </summary>
    public CardSlot allyRangedBonusSlot;

    /// <summary>
    /// The siege card slots for the ally player.
    /// </summary>
    public CardSlot[] allySiegeSlots;

    /// <summary>
    /// The bonus siege card slot for the ally player.
    /// </summary>
    public CardSlot allySiegeBonusSlot;
    /// 
    /// <summary>
    /// The leader card slot for the ally player.
    /// </summary>
    public CardSlot allyLeaderSlot;

    /// <summary>
    /// The melee card slots for the enemy player.
    /// </summary>
    public CardSlot[] enemyMeleeSlots;

    /// <summary>
    /// The bonus melee card slot for the enemy player.
    /// </summary>
    public CardSlot enemyMeleeBonusSlot;

    /// <summary>
    /// The ranged card slots for the enemy player.
    /// </summary>
    public CardSlot[] enemyRangedSlots;

    /// <summary>
    /// The bonus ranged card slot for the enemy player.
    /// </summary>
    public CardSlot enemyRangedBonusSlot;

    /// <summary>
    /// The siege card slots for the enemy player.
    /// </summary>
    public CardSlot[] enemySiegeSlots;

    /// <summary>
    /// The bonus siege card slot for the enemy player.
    /// </summary>
    public CardSlot enemySiegeBonusSlot;

    /// <summary>
    /// The leader card slot for the enemy player.
    /// </summary>
    public CardSlot enemyLeaderSlot;
    /// /// 
    /// <summary>
    /// The deck of the enemy player.
    /// </summary>
    public Deck enemyDeck;

    /// <summary>
    /// The graveyard stack for the enemy player.
    /// </summary>
    public Stack<Card> enemyGraveyard = new();

    /// <summary>
    /// The ally player.
    /// </summary>
    public Player allyPlayer;

    /// <summary>
    /// The enemy player.
    /// </summary>
    public Player enemyPlayer;
    /// 
    /// <summary>
    /// The hand panel for player 1.
    /// </summary>
    public GameObject player1HandPanel;

    /// <summary>
    /// The hand panel for player 2.
    /// </summary>
    public GameObject player2HandPanel;

    /// <summary>
    /// The prefab used to instantiate cards.
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// A boolean variable to manage turn logic, indicating if the ally player is currently playing.
    /// </summary>
    public bool allyPlayerIsPlaying = true;

    /// <summary>
    /// Initializes the board and assigns decks, hand panels, and card prefabs to players.
    /// </summary>
    void Awake()
    {
        if (allyDeck == null)
        {
            allyDeck = new GameObject("AllyDeck").AddComponent<Deck>();
        }

        if (enemyDeck == null)
        {
            enemyDeck = new GameObject("EnemyDeck").AddComponent<Deck>();
        }

        // Assign hand panels and card prefab to players
        allyPlayer.handPanel = player1HandPanel;
        allyPlayer.cardPrefab = cardPrefab;
        enemyPlayer.handPanel = player2HandPanel;
        enemyPlayer.cardPrefab = cardPrefab;

        // Assign board reference to players
        allyPlayer.board = this;
        enemyPlayer.board = this;
    }

    /// <summary>
    /// Starts the game by initializing decks, shuffling them, and drawing initial hands for players.
    /// </summary>
    void Start()
    {
        var cardsQuanto = CardsQuanto.Instance;

        if (cardsQuanto == null)
        {
            Debug.LogError("CardsQuanto instance is null");
            return;
        }

        if (cardsQuanto.CardsOfIdanai == null || cardsQuanto.CardsOfYudivain == null || cardsQuanto.CardsOfCelai == null)
        {
            Debug.LogError("CardsQuanto card lists are not initialized");
            return;
        }

        List<Stack<Card>> decks = new()
        {
            cardsQuanto.CardsOfIdanai,
            cardsQuanto.CardsOfCelai,
            cardsQuanto.CardsOfYudivain
        };
        System.Random random = new();

        // Assign random decks to players
        int deckIndex1 = random.Next(decks.Count);
        allyPlayer.Name = "Ally Player";
        allyDeck.cards = decks[deckIndex1];
        decks.RemoveAt(deckIndex1);

        int deckIndex2 = random.Next(decks.Count);
        enemyPlayer.Name = "Enemy Player";
        enemyDeck.cards = decks[deckIndex2];

        // Place leader cards in the respective slots
        PlaceLeaderCard(allyPlayer, allyDeck, allyLeaderSlot);
        PlaceLeaderCard(enemyPlayer, enemyDeck, enemyLeaderSlot);

        // Shuffle decks
        allyDeck.Shuffle();
        enemyDeck.Shuffle();

        // Initialize player hands
        allyPlayer.DrawCards(allyDeck, 10);
        enemyPlayer.DrawCards(enemyDeck, 10);

        // Update hand panel visibility at the start of the game
        UpdateHandPanelVisibility();
    }

    /// <summary>
    /// Places the leader card from the specified deck into the specified leader slot for the given player.
    /// </summary>
    /// <param name="player">The player whose leader card is being placed.</param>
    /// <param name="deck">The deck from which the leader card is drawn.</param>
    /// <param name="leaderSlot">The slot where the leader card will be placed.</param>
    private void PlaceLeaderCard(Player player, Deck deck, CardSlot leaderSlot)
    {
        if (deck.cards.Count > 0 && deck.cards.Peek() is LeaderCard leaderCard)
        {
            // Pop the leader card from the deck
            deck.cards.Pop();

            // Instantiate the card prefab and place it in the leader slot
            GameObject leaderCardObject = Instantiate(cardPrefab, leaderSlot.transform);
            CardManager cardManager = leaderCardObject.GetComponent<CardManager>();

            if (cardManager != null)
            {
                cardManager.CardData = leaderCard;
                leaderSlot.PlaceCard(leaderCard, leaderCardObject);
            }
            else
            {
                Debug.LogError("CardManager component not found on card prefab.");
            }
        }
        else
        {
            Debug.LogError($"{player.Name} does not have a leader card in the deck.");
        }
    }

    /// Rotates the main camera smoothly by 180 degrees over a specified duration with an ease-in-out effect.
    /// Additionally, the camera zooms in slightly during the first half of the rotation and zooms out during the second half.
    /// </summary>
    /// <param name="duration">The duration over which the camera rotation and zoom effect will occur.</param>
    /// <returns>An IEnumerator that can be used to control the coroutine.</returns>
    public IEnumerator RotateCameraSmoothly(float duration)
    {
        yield return new WaitForSeconds(0.2f);
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0;

        float startSize = mainCamera.orthographicSize;
        float zoomInSize = startSize - 0.1f; // Zoom in
        float zoomOutSize = startSize; // Zoom out

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // Ease-in-out

            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            if (t < 0.5f)
            {
                mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomInSize, t * 2);
            }
            else
            {
                mainCamera.orthographicSize = Mathf.Lerp(zoomInSize, zoomOutSize, (t - 0.5f) * 2);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.rotation = endRotation;
        mainCamera.orthographicSize = zoomOutSize;
    }

    /// Updates the visibility and interactivity of the hand panels for both ally and enemy players.
    /// </summary>
    /// <remarks>
    /// This method adjusts the alpha, interactable, and blocksRaycasts properties of the CanvasGroup components
    /// attached to the hand panels of the ally and enemy players based on which player is currently playing.
    /// </remarks>
    public void UpdateHandPanelVisibility()
    {
        CanvasGroup allyHandPanelCanvasGroup = allyPlayer.handPanel.GetComponent<CanvasGroup>();
        CanvasGroup enemyHandPanelCanvasGroup = enemyPlayer.handPanel.GetComponent<CanvasGroup>();

        if (allyPlayerIsPlaying)
        {
            allyHandPanelCanvasGroup.alpha = 1;
            allyHandPanelCanvasGroup.interactable = true;
            allyHandPanelCanvasGroup.blocksRaycasts = true;

            enemyHandPanelCanvasGroup.alpha = 0;
            enemyHandPanelCanvasGroup.interactable = false;
            enemyHandPanelCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            allyHandPanelCanvasGroup.alpha = 0;
            allyHandPanelCanvasGroup.interactable = false;
            allyHandPanelCanvasGroup.blocksRaycasts = false;

            enemyHandPanelCanvasGroup.alpha = 1;
            enemyHandPanelCanvasGroup.interactable = true;
            enemyHandPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}