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
    playerInputActions.General.Pause.Enable();
    playerInputActions.General.Pause.performed += ToggleGamePaused;
  }

  void OnDestroy()
  {
    playerInputActions.General.Pause.performed -= ToggleGamePaused;
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
