using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
  [SerializeField]
  [Range(0, 99)]
  private int _lives = 3;

  public int Lives
  {
    get
    {
      return _lives;
    }
    set
    {
      int priorLives = _lives;
      _lives = value;
      GameEvents.PlayerLivesChanged?.Invoke(priorLives, _lives, priorLives > _lives);
    }
  }

  public void SubtractLife()
  {
    Lives = _lives - 1;
    GameEvents.PlayerLostLife?.Invoke();
  }
}
