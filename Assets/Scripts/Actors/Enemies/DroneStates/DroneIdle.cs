using System;
using UnityEngine;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/Drone/Idle")]
[Serializable]
public class DroneIdle : Zero.State<Drone.DroneStates>
{
  [SerializeField]
  float movementSpeed = 4.5f;
  Vector3 newPosition = new Vector3();
  bool moveLeft = false;
  Transform transform;

  public override void Init(StateContextBehaviour<Drone.DroneStates> context)
  {
    base.Init(context);
  }

  public override void Enter()
  {
    Debug.Log("Enter Idle State");
    transform = stateContext.GetComponent<Transform>();
    transform.rotation = Quaternion.Euler(0, 0, -180);
  }

  public override void Exit()
  {
    Debug.Log("Exit Idle State");
  }

  public override void Update()
  {
    newPosition = transform.position;

    if (moveLeft)
    {
      newPosition.x = Mathf.Clamp(newPosition.x - movementSpeed * Time.deltaTime, -8, 8);
      if (newPosition.x == -8)
      {
        moveLeft = false;
      }
    }
    else
    {
      newPosition.x = Mathf.Clamp(newPosition.x + movementSpeed * Time.deltaTime, -8, 8);
      if (newPosition.x == 8)
      {
        moveLeft = true;
      }
    }

    transform.position = newPosition;
  }
}
