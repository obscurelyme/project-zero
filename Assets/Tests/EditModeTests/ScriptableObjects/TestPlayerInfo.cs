using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerInfo
{
  [Test]
  public void TestPlayerInfo_Lives()
  {
    const int LIVES = 3;
    PlayerInfo info = ScriptableObject.CreateInstance<PlayerInfo>();

    Assert.AreEqual(LIVES, info.Lives);
  }

  [Test]
  public void TestPlayerInfo_SubtractLives()
  {
    const int EXPECTED_LIVES = 2;
    PlayerInfo info = ScriptableObject.CreateInstance<PlayerInfo>();

    info.SubtractLife();

    Assert.AreEqual(EXPECTED_LIVES, info.Lives);
  }

  [Test]
  public void TestPlayerInfoSetLivesInvokesGameEvent_PlayerLivesChanged()
  {
    int prior = 0;
    int current = 0;
    bool lost = false;
    GameEvents.PlayerLivesChanged += (_prior, _current, _lost) =>
    {
      prior = _prior;
      current = _current;
      lost = _lost;
    };
    PlayerInfo info = ScriptableObject.CreateInstance<PlayerInfo>();

    info.Lives = 4;

    Assert.AreEqual(3, prior);
    Assert.AreEqual(4, current);
    Assert.AreEqual(false, lost);
  }
}
