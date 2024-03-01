using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCard : Card
{
    protected override void Action(Ship ship)
    {
        Debug.Log("Blank Card");
    }
}
