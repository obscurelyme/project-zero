using System.Collections;
using UnityEngine;

public class Drone : Zero.StateContextBehaviour<Drone.DroneStates>
{
  public enum DroneStates
  {
    Idle,
    Enter,
    Destroyed,
    Leave,
    Spawn
  }

  void Start()
  {
    InitializeState(initialState);
    currentState?.Enter();
  }

  void Update()
  {
    currentState?.Update();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    // currentState.OnTriggerEnter2D( collider);
  }
}
