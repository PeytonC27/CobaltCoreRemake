using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : Card
{
    protected override void Action(Ship ship)
    {
        ship.Fire(1);
    }
}
