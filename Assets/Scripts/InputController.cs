using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    protected bool _upIsHeld;
    protected bool _downIsHeld;

    public virtual bool UpIsHeld()
    {
        return _upIsHeld;
    }
    
    public virtual  bool DownIsHeld()
    {
        return _downIsHeld;
    }
}
