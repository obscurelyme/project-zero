using UnityEngine;
using Zero;

public class Player : MonoBehaviour
{
  [SerializeField]
  PlayerInfo playerInfo;

  void OnCollisionEnter2D(Collision2D collision2D)
  {
    Debug.Log(collision2D);
    playerInfo.SubtractLife();
  }

  void OnTriggerEnter2D(Collider2D collider2D)
  {
    // NOTE: trigger
    Debug.Log(collider2D);
    playerInfo.SubtractLife();
  }

  void OnEnable()
  {
    GameEvents.PauseGame += HandlePause;
  }

  void OnDisable()
  {
    GameEvents.PauseGame -= HandlePause;
  }

  void HandlePause(bool isPaused)
  {
    Debug.Log($"Game was {(isPaused ? "Paused" : "Resumed")}, Player");
  }
}
