using System;
using UnityEngine;

[Flags]
public enum PlayerInputFlags
{
    None = 0,
    Move = 1 << 0, 
    Sprint = 1 << 1, 
    Jump = 1 << 2,   
    Interact = 1 << 3 
}

public class PlayerInputBuffer:MonoBehaviour
{
    private PlayerInputFlags _currentFlags = PlayerInputFlags.None;
    
    public void SetFlag(PlayerInputFlags flag, bool value)
    {
        if (value)
            _currentFlags |= flag; 
        else
            _currentFlags &= ~flag;
    }
    
    public bool HasFlag(PlayerInputFlags flag)
    {
        return (_currentFlags & flag) != 0;
    }
    
    public PlayerInputFlags CurrentFlags => _currentFlags;
}