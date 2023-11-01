using UnityEngine;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/TESTEnemy/Idle")]
public class Idle : State<TESTEnemy.TESTEnemyStates>
{
  Transform transform;
  [SerializeField]
  float rotateSpeed = 50f;
  [SerializeField]
  float movementSpeed = 4.5f;

  Vector3 newPosition;
  bool moveLeft;

  public override void Init(StateContextBehaviour<TESTEnemy.TESTEnemyStates> context)
  {
    base.Init(context);
    transform = context.GetComponent<Transform>();
  }


  public override void Enter()
  {
    Debug.Log("Before rotation reset " + transform.rotation.eulerAngles.ToString());
    // NOTE: set the rotation to something that won't brick later.
    transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    Debug.Log("After rotation reset " + transform.rotation.eulerAngles.ToString());
    moveLeft = false;
  }

  public override void Update()
  {

    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -180), rotateSpeed * Time.deltaTime);
    Move();
  }

  public override void Exit()
  {

  }

  void Move()
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
