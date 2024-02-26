using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    public bool CanFire { get; private set; }
    public bool IsProtected { get; private set; }
    public bool IsFragile { get; private set; }

    public void MovePart(int shamt)
    {
        transform.position = new Vector2(transform.position.x + shamt, transform.position.y);
    }
}
