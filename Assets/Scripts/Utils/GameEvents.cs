using System;

public static class GameEvents
{
  public static Action<bool> PauseGame;
  /// <summary>
  /// Prior Lives,
  /// Current Lives,
  /// Lost life
  /// </summary>
  public static Action<int, int, bool> PlayerLivesChanged;
  public static Action PlayerLostLife;
  public static Action PlayerGainedLife;
}
