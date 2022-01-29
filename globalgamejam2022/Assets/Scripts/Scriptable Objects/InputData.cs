using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputData", menuName="Data/InputData" )]
public class InputData : ScriptableObject
{
    public PlayerInput playerInput;
    public PlayerControls.PlayerActions playerControls;
    public Vector2 move;
    public Vector2 look;
    public bool usingGamepad;
}
