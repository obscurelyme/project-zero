using UnityEngine;
using Zero;

public class PlayerInput : MonoBehaviour
{
  PlayerInputActions inputActions;
  bool isPaused = false;


  void Awake()
  {
    inputActions = new PlayerInputActions();
  }

  void Start()
  {
    inputActions.General.Pause.performed += TogglePause;
  }

  void OnEnable()
  {
    inputActions.General.Enable();
    inputActions.Player.Enable();
  }

  void OnDisable()
  {
    inputActions.General.Disable();
    inputActions.Player.Disable();
  }

  public PlayerInputActions.PlayerActions PlayerActions
  {
    get
    {
      return inputActions.Player;
    }
  }

  public PlayerInputActions.GeneralActions GeneralActions
  {
    get
    {
      return inputActions.General;
    }
  }

  public PlayerInputActions.UIActions UserInterfaceActions
  {
    get
    {
      return inputActions.UI;
    }
  }

  void TogglePause(UnityEngine.InputSystem.InputAction.CallbackContext _)
  {
    isPaused = !isPaused;
    if (isPaused)
    {
      inputActions.Player.Disable();
      inputActions.UI.Enable();
    }
    else
    {
      inputActions.UI.Disable();
      inputActions.Player.Enable();
    }
    GameEvents.PauseGame.Invoke(isPaused);
  }
}
