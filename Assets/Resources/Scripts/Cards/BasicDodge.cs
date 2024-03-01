using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDodge : Card
{
    protected override void Action(Ship ship)
    {
        ship.moves++;
    }
}
