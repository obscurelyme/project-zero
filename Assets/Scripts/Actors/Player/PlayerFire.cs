using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zero;

public class PlayerFire : MonoBehaviour
{
  PlayerInputActions.PlayerActions playerActions;

  void Start()
  {
    playerActions = GetComponent<PlayerInput>().PlayerActions;
  }

  void OnEnable()
  {
    playerActions.Fire.performed += Fire;
  }

  void OnDisable()
  {
    playerActions.Fire.performed -= Fire;
  }

  void Fire(UnityEngine.InputSystem.InputAction.CallbackContext _)
  {
    Debug.Log("Fire laser");
  }
}
