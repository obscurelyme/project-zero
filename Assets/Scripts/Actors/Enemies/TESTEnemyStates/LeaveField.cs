using UnityEngine;
using UnityEngine.Splines;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/TESTEnemy/LeaveField")]
public class LeaveField : State<TESTEnemy.TESTEnemyStates>
{
  enum Substate
  {
    GoToPlayer,
    Loopback
  }

  Transform transform;
  Transform playerTransform;
  SplineAnimate splineAnim;
  SplineContainer container;

  Vector3 snapshotPlayerPosition;
  Vector3 randoPosition;

  [SerializeField]
  float movementSpeed = 5f;
  Substate substate = Substate.GoToPlayer;

  public override void Init(StateContextBehaviour<TESTEnemy.TESTEnemyStates> context)
  {
    base.Init(context);
    transform = context.transform;
    // TODO: come up with a better solution to this
    container = GameObject.FindGameObjectWithTag("Exit Spline").GetComponent<SplineContainer>();
  }


  public override void Enter()
  {
    substate = Substate.GoToPlayer;
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  public override void Update()
  {
    if (substate == Substate.GoToPlayer)
    {
      GoToPlayerUpdate();
      return;
    }
    LoopDeLoop();
  }

  public override void Exit()
  {

  }

  void GoToPlayerUpdate()
  {
    LookAtPlayer();
    MoveToPlayer();
    if (transform.position.y > -1)
    {
      substate = Substate.Loopback;
    }
  }

  void LoopDeLoop()
  {

  }

  void LookAtPlayer()
  {
    Vector3 relative = transform.InverseTransformPoint(playerTransform.position);
    float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
    transform.Rotate(0, 0, -angle);
  }

  void MoveToPlayer()
  {
    Vector3 currentPosition = transform.position;
    Vector3 newPos = Vector3.MoveTowards(currentPosition, playerTransform.position, movementSpeed * Time.deltaTime);
    transform.position = newPos;
  }
}
