using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
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
  Vector3 containerOffset;

  Vector3 snapshotPlayerPosition;
  Vector3 randoPosition;

  [SerializeField]
  float movementSpeed = 5f;
  Substate substate = Substate.GoToPlayer;

  public override void Init(StateContextBehaviour<TESTEnemy.TESTEnemyStates> context)
  {
    base.Init(context);
    transform = context.transform;
    splineAnim = context.GetComponent<SplineAnimate>();
    // TODO: come up with a better solution to this
    GameObject exitSplineObj = GameObject.FindGameObjectWithTag("Exit Spline");
    container = exitSplineObj.GetComponent<SplineContainer>();
    containerOffset = exitSplineObj.transform.position;
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
    container.Spline.RemoveAt(0);
    container.Spline.RemoveAt(0);
    container.Spline.RemoveAt(0);
    splineAnim.Container = null;
  }

  void GoToPlayerUpdate()
  {
    LookAtPlayer();
    MoveToPlayer();
    if (transform.position.y < -1)
    {
      snapshotPlayerPosition = playerTransform.position;
      substate = Substate.Loopback;
      splineAnim.Container = container;

      BezierKnot randoKnot = new BezierKnot(new float3(transform.position.x + 2 - containerOffset.x, transform.position.y + 2 - containerOffset.y, 0));
      BezierKnot playerKnot = new BezierKnot(new float3(snapshotPlayerPosition.x - containerOffset.x, snapshotPlayerPosition.y - containerOffset.y, 0));
      BezierKnot currentKnot = new BezierKnot(new float3(transform.position.x - containerOffset.x, transform.position.y - containerOffset.y, 0));

      randoKnot.Rotation = Quaternion.Euler(0, 90, 270);
      playerKnot.Rotation = Quaternion.Euler(0, 90, 270);
      currentKnot.Rotation = Quaternion.Euler(0, 90, 270);

      randoKnot.TangentIn = new float3(0, 0, -1.5f);
      playerKnot.TangentIn = new float3(0, 0, -1.5f);
      currentKnot.TangentIn = new float3(0, 0, 0);

      randoKnot.TangentOut = new float3(0, 0, 1.5f);
      playerKnot.TangentOut = new float3(0, 0, 1.5f);
      currentKnot.TangentOut = new float3(0, 0, 0);

      // playerKnot.TangentIn

      container.Spline.Insert(0, randoKnot);
      container.Spline.Insert(0, playerKnot);
      container.Spline.Insert(0, currentKnot);

      // container.Spline.SetTangentMode(TangentMode.Mirrored);
      // container.Spline.

      splineAnim.Restart(false);
      splineAnim.Play();
    }
  }

  void LoopDeLoop()
  {
    if (splineAnim.NormalizedTime >= 1f)
    {
      stateContext.ChangeState(TESTEnemy.TESTEnemyStates.EnterField);
    }
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
