using System.Collections;
using UnityEngine;
using Zero;

public class TESTEnemy : StateContextBehaviour<TESTEnemy.TESTEnemyStates>
{
  public enum TESTEnemyStates
  {
    EnterField,
    Idle,
    LeaveField
  }

  PlayerInputActions inputActions;

  void Awake()
  {
    inputActions = new PlayerInputActions();
    inputActions.StateDriver.Enable();
  }

  void Start()
  {
    InitializeState(initialState);

    inputActions.StateDriver.State1.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      ChangeState(TESTEnemyStates.EnterField);
    };
    inputActions.StateDriver.State2.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      ChangeState(TESTEnemyStates.Idle);
    };
    inputActions.StateDriver.State3.performed += (UnityEngine.InputSystem.InputAction.CallbackContext _) =>
    {
      ChangeState(TESTEnemyStates.LeaveField);
    };

    StartCoroutine(DelayStartEnter());
  }

  void Update()
  {
    currentState?.Update();
  }

  IEnumerator DelayStartEnter()
  {
    yield return new WaitForEndOfFrame();
    currentState?.Enter();
  }
}
