using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deirin.EB;
using UnityEngine.InputSystem;

public class PlayerEntity : BaseEntity {
    [Header("References")]
    public Team team;
    public Transform graphics;
    public PlayerInput playerInput;
}