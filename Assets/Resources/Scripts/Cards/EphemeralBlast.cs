using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralBlast : Card
{
    protected override void Action(Ship ship)
    {
        ship.Fire(7);
    }
}
