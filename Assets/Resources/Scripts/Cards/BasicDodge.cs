using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDodge : Card
{
    public override void Action(Ship ship)
    {
        ship.moves+=5;
    }
}
