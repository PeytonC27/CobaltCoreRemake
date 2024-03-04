using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralBlast : Card
{
    public override void Action(Ship ship)
    {
        ship.Fire(7);
    }
}
