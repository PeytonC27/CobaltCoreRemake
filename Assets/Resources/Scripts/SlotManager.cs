using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] public int cardsToDraw;
    [SerializeField] float startX;
    [SerializeField] float endX;
    [SerializeField] float yPos;

    public List<GameObject> slots;

    private void Start()
    {
        slots = new();
        SetupSlots();
    }

    void SetupSlots()
    {
        // destroy current slots
        for (int i = 0; i < slots.Count;)
        {
            Destroy(slots[i]);
            slots.RemoveAt(i);
        }

        GameObject temp;
        for (int i = 1; i <= cardsToDraw; i++)
        {
            temp = Instantiate(slot, new Vector2(0, yPos), Quaternion.identity, this.transform);
            temp.name = "Slot" + i;
            slots.Add(temp);
        }

        RelocateSlots();
    }

    public void SetupAdditionalSlots(int amount)
    {
        GameObject temp;
        int start = slots.Count + 1;
        int end = slots.Count + amount;
        for (int i = start; i <= end; i++)
        {
            temp = Instantiate(slot, new Vector2(0, yPos), Quaternion.identity, this.transform);
            temp.name = "Slot" + i;
            slots.Add(temp);
        }

        RelocateSlots();
    }

    public void RemoveSlots(int toRemove)
    {
        int count = slots.Count;
        for (int i = count - 1; i >= count - toRemove; i--)
        {
            Destroy(slots[i]);
            slots.RemoveAt(i);
        }

        RelocateSlots();
    }

    void RelocateSlots()
    {
        startX = -(slots.Count - 1);
        endX = slots.Count - 1;

        float currX = startX;
        float distBetweenCards = Mathf.Abs(startX - endX) / (slots.Count - 1);
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.position = new Vector2(currX, yPos);
            currX += distBetweenCards;
        }
    }
}
