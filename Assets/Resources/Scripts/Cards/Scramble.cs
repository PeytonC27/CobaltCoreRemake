using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scramble : Card
{
    protected override void Action(Ship ship)
    {
        ship.moves += 2;
    }
}
