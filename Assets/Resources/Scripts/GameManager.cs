using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cardSlotHolder;
    [SerializeField] Ship playerShip;
    [SerializeField] GameObject cardHolder;

    private List<Card> deck = new List<Card>();
    private Transform[] cardSlots;
    public bool[] availableCardSlots;

    private void Start()
    {
        cardSlots = new Transform[cardSlotHolder.transform.childCount];
        availableCardSlots = new bool[cardSlots.Length];

        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i] = cardSlotHolder.transform.GetChild(i);
            availableCardSlots[i] = true;
        }

        for (int i = 0; i < cardHolder.transform.childCount; i++)
            deck.Add(cardHolder.transform.GetChild(i).GetComponent<Card>());
    }

    public void DrawCard()
    {
        if (deck.Count > 0) 
        {
            Card randCard = deck[UnityEngine.Random.Range(0,deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i])
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0;i < 5; i++)
                DrawCard();
        }

        // update slots

    }

    public void PerformShipAction(Action<Ship> cardAction)
    {
        cardAction.Invoke(playerShip);
    }
}
