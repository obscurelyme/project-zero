using UnityEngine;
using Zero;

public class PauseGame : MonoBehaviour
{
  bool isPaused = false;
  PlayerInputActions playerInputActions;

  void Awake()
  {
#if UNITY_EDITOR
#else
    Cursor.visible = false;
#endif
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Pause.Enable();
    playerInputActions.Player.Pause.performed += ToggleGamePaused;
  }

  void OnDestroy()
  {
    playerInputActions.Player.Pause.performed -= ToggleGamePaused;
    Time.timeScale = 1;
  }

  void ToggleGamePaused(UnityEngine.InputSystem.InputAction.CallbackContext _)
  {
    isPaused = !isPaused;
#if UNITY_EDITOR
#else
    Cursor.visible = isPaused;
#endif
    Time.timeScale = isPaused ? 0 : 1;
  }
}
