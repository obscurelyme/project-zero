using UnityEngine;
using UnityEngine.Splines;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/TESTEnemy/EnterField")]
public class EnterField : State<TESTEnemy.TESTEnemyStates>
{
  SplineAnimate splineAnimate;
  SplineContainer splineContainer;

  public override void Init(StateContextBehaviour<TESTEnemy.TESTEnemyStates> context)
  {
    base.Init(context);
    splineAnimate = context.GetComponent<SplineAnimate>();
    splineContainer = splineAnimate.Container;
  }

  public override void Enter()
  {
    if (splineAnimate.Container == null)
    {
      splineAnimate.Container = splineContainer;
    }
    splineAnimate.Restart(false);
    splineAnimate.Play();
  }

  public override void Update()
  {
    if (splineAnimate.NormalizedTime >= 1f)
    {
      stateContext.ChangeState(TESTEnemy.TESTEnemyStates.Idle);
    }
  }

  public override void Exit()
  {
  }
}
