using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
    public override bool UpIsHeld()
    {
        return Input.GetAxisRaw("Vertical") > 0;
    }
    
    public override bool DownIsHeld()
    {
        return Input.GetAxisRaw("Vertical") < 0;
    }
}
