#nullable enable

using System.Collections.Generic;
using UnityEngine;
using Zero;

public class PlayerFire : MonoBehaviour
{
  PlayerInputActions.PlayerActions playerActions;
  [SerializeField]
  GameObject missilePrefab;
  [SerializeField]
  Transform missileSpawnLoc;
  List<Missile> missiles = new List<Missile>();
  [SerializeField]
  int missileCount = 10;
  [SerializeField]
  int currentMissileIndex = 0;

  void Start()
  {
    for (int i = 0; i < missileCount; i++)
    {
      Quaternion rot = Quaternion.identity;
      rot.eulerAngles = new Vector3(0, 0, 90);
      GameObject obj = Instantiate(missilePrefab, new Vector3(0, -8, 0), rot);
      missiles.Add(obj.GetComponent<Missile>());
    }

    playerActions = GetComponent<PlayerInput>().PlayerActions;
    playerActions.Fire.performed += Fire;
  }

  void OnDestroy()
  {
    playerActions.Fire.performed -= Fire;
  }

  void Fire(UnityEngine.InputSystem.InputAction.CallbackContext _)
  {
    currentMissileIndex = currentMissileIndex + 1;
    if (currentMissileIndex == missiles.Count)
    {
      currentMissileIndex = 0;
    }
    missiles[currentMissileIndex].Fire(missileSpawnLoc.position);
  }
}
