using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCard : Card
{
    public override void Action(Ship ship)
    {
        Debug.Log("Blank Card");
    }
}
