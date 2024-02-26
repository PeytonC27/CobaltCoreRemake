using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool PressingLeft { get; private set; } = false;
    public bool PressingRight { get; private set; } = false;
    public bool LeftClick { get; private set; } = false;

    void Update()
    {
        PressingLeft = Input.GetKeyDown(KeyCode.A);
        PressingRight = Input.GetKeyDown(KeyCode.D);
        LeftClick = Input.GetKeyDown(KeyCode.Mouse0);
    }
}
