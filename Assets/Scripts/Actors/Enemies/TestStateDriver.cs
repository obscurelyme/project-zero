using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zero;

public class TestStateDriver : MonoBehaviour
{
  PlayerInputActions inputActions;

  [SerializeField]
  Drone drone;

  void Awake()
  {
    inputActions = new PlayerInputActions();
    inputActions.StateDriver.Enable();
  }

  void Start()
  {
    inputActions.StateDriver.State1.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      drone.ChangeState(Drone.DroneStates.Enter);
    };
    inputActions.StateDriver.State2.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      drone.ChangeState(Drone.DroneStates.Leave);
    };
    inputActions.StateDriver.State3.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      drone.ChangeState(Drone.DroneStates.Destroyed);
    };
    inputActions.StateDriver.State4.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      drone.ChangeState(Drone.DroneStates.Idle);
    };
    inputActions.StateDriver.State5.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      drone.CancelTransition();
    };
  }
}
