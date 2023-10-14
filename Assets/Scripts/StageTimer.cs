using System;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
  TimeSpan s;
  TimeSpan u;
  float currentTime = 120;
  [SerializeField]
  string currentTimeString;

  void Start()
  {
    s = TimeSpan.Zero;
  }

  void Update()
  {
    u = s.Add(TimeSpan.FromSeconds(Time.deltaTime));
    currentTime += Time.deltaTime;
    currentTimeString = u.ToString(@"mm\:ss\:ff");
    s = u;
  }

  public string GetTime()
  {
    return currentTimeString;
  }
}
