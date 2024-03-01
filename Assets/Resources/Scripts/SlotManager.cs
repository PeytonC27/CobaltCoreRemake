using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] public int cardsToDraw;
    [SerializeField] float startX;
    [SerializeField] float endX;
    [SerializeField] float yPos;

    float distBetweenCards = 3;
    float length;

    private void Start()
    {
        length = Mathf.Abs(startX - endX);
        distBetweenCards = length / (cardsToDraw - 1);
        DrawCards();
    }

    void DrawCards()
    {
        GameObject temp;
        float currX = startX;
        for (int i = 1; i <= cardsToDraw; i++)
        {
            temp = Instantiate(slot, new Vector2(currX, yPos), Quaternion.identity, this.transform);
            temp.name = "Slot" + i;
            currX += distBetweenCards;
        }
    }
}
