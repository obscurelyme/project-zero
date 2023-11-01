using UnityEngine;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/Drone/Leave")]
[System.Serializable]
public class DroneLeave : Zero.State<Drone.DroneStates>
{
  [SerializeField]
  float speed = 6f;

  [SerializeField]
  Transform goalPosition;

  Transform transform;
  Vector3 currentPosition;
  Vector3 movement;

  public override void Init(StateContextBehaviour<Drone.DroneStates> context)
  {
    base.Init(context);
    transform = context.transform;
  }

  public override void Enter()
  {
    goalPosition = GameObject.FindGameObjectsWithTag("Spawn_Loc")[0].transform;
    Debug.Log("Drone Leave State Entered");
  }

  public override void Exit()
  {
    Debug.Log("Drone Leave State Exited");
  }

  public override void Update()
  {
    LookAtGoal();
    MoveToGoal();
    CheckArrivedAtGoal();
  }

  void LookAtGoal()
  {
    Vector3 relative = transform.InverseTransformPoint(goalPosition.position);
    float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
    transform.Rotate(0, 0, -angle);
  }

  void MoveToGoal()
  {
    currentPosition = transform.position;
    Vector3 newPos = Vector3.MoveTowards(currentPosition, goalPosition.position, speed * Time.deltaTime);
    transform.position = newPos;
  }

  void CheckArrivedAtGoal()
  {
    if (transform.position == goalPosition.position)
    {
      stateContext.ChangeState(Drone.DroneStates.Enter);
    }
  }
}
