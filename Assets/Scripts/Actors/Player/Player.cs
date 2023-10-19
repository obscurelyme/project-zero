using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;
using Zero;

public class Player : MonoBehaviour
{
  PlayerInputActions inputActions;
  // Transform playerTransform;
  UnityEngine.Vector2 movement;
  UnityEngine.Vector3 currentPos;
  UnityEngine.Vector3 newPos;

  [SerializeField]
  int movementSpeed = 10;

  void Awake()
  {
    inputActions = new PlayerInputActions();
    inputActions.Player.Enable();
  }

  void Start()
  {
    // playerTransform = GetComponent<Transform>();
  }

  void Update()
  {
    currentPos = transform.position;
    movement = inputActions.Player.Move.ReadValue<UnityEngine.Vector2>();
    if (movement.x == -1)
    {
      // NOTE: go left
      currentPos.x -= movementSpeed * Time.deltaTime;
    }
    if (movement.x == 1)
    {
      // NOTE: go right
      currentPos.x += movementSpeed * Time.deltaTime;
    }

    transform.position = currentPos;
  }
}
