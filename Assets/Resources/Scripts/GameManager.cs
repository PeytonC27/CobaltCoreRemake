using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SlotManager slotManager;
    [SerializeField] Ship playerShip;
    [SerializeField] EnemyShip enemyShip;
    [SerializeField] GameObject cardHolder;

    private List<Card> deck = new List<Card>();
    private List<Card> hand = new List<Card>();
    private List<Transform> cardSlots;
    public List<bool> availableCardSlots;

    private bool playerTurn = true;
    private int energy;

    private void Start()
    {
        cardSlots = new();
        availableCardSlots = new();

        for (int i = 0; i < slotManager.slots.Count; i++)
        {
            cardSlots.Add(slotManager.slots[i].transform);
            availableCardSlots.Add(true);
        }

        for (int i = 0; i < cardHolder.transform.childCount; i++)
            deck.Add(cardHolder.transform.GetChild(i).GetComponent<Card>());
    }

    public void DrawCard()
    {
        if (deck.Count > 0) 
        {
            Card randCard = deck[UnityEngine.Random.Range(0,deck.Count)];

            for (int i = 0; i < availableCardSlots.Count; i++)
            {
                if (availableCardSlots[i])
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    hand.Add(randCard);
                    return;
                }
            }
        }
    }

    public void DrawAdditionalCards(int amount)
    {
        // setup slots
        slotManager.SetupAdditionalSlots(amount);

        ResetAndTransformCards();

        // draw the additional cards
        for (int i = 0; i < amount; i++)
        {
            availableCardSlots.Add(true);
            DrawCard();
        }

        Debug.Log("Added new cards");
    }

    public bool OnCardPlayed(Card card)
    {
        if (energy <= 0)
            return false;

        availableCardSlots.RemoveAt(card.handIndex);
        hand.RemoveAt(card.handIndex);
        slotManager.RemoveSlots(1);

        // reset the transforms
        ResetAndTransformCards();

        PerformShipAction(card.Action);

        energy--;

        return true;
    }

    private void Update()
    {
        if (playerTurn)
        {
            playerTurn = false;
            for (int i = 0; i < 5; i++)
                DrawCard();
            energy = 3;
        }

        if (!playerTurn && Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(runEnemyTurn());
        }

        // update slots

    }

    IEnumerator runEnemyTurn()
    {
        // shoot their gun
        StartCoroutine(enemyShip.FireCannons(1));

        yield return new WaitForSeconds(0.5f);

        // enemy will align to player's ship randomly
        int playerShipSize = playerShip.Parts;
        enemyShip.Move(
            (short)-(enemyShip.transform.position.x - playerShip.transform.position.x +
            UnityEngine.Random.Range(-playerShipSize / 2, playerShipSize / 2 + 1))
        );

        playerTurn = true;
    }

    public void PerformShipAction(Action<Ship> cardAction)
    {
        cardAction.Invoke(playerShip);
    }

    void ResetAndTransformCards()
    {
        // reset the transforms
        cardSlots.Clear();
        for (int i = 0; i < slotManager.slots.Count; i++)
            cardSlots.Add(slotManager.slots[i].transform);

        // adjust positions of cards in hand
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position = cardSlots[i].position;
            hand[i].handIndex = i;
        }
    }
}
