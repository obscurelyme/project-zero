#nullable enable

using UnityEngine;
using Zero;

public class PlayerFire : MonoBehaviour
{
  PlayerInputActions.PlayerActions playerActions;
  [SerializeField]
  Transform missileSpawnLoc;
  [SerializeField]
  Missile missile;

  void Start()
  {
    playerActions = GetComponent<PlayerInput>().PlayerActions;
    playerActions.Fire.performed += Fire;
  }

  void OnDestroy()
  {
    playerActions.Fire.performed -= Fire;
  }

  void Fire(UnityEngine.InputSystem.InputAction.CallbackContext _)
  {
    Debug.Log("Fire laser");
    missile.Fire(missileSpawnLoc.position);
  }
}
