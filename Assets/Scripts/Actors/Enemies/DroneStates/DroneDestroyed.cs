using UnityEditorInternal;
using UnityEngine;
using Zero;

[CreateAssetMenu(menuName = "StateMachine/Drone/Destroyed")]
[System.Serializable]
public class DroneDestroyed : Zero.State<Drone.DroneStates>
{
  private Animator anim;

  public override void Init(StateContextBehaviour<Drone.DroneStates> context)
  {
    base.Init(context);
    anim = context.GetComponent<Animator>();
  }

  public override void Enter()
  {
    Debug.Log("Drone Destroyed State Entered");
    anim.enabled = true;
    // NOTE: 0.417 seconds is the length of the animation, need a better way to handle this.
    stateContext.TransitionInSeconds(0.417f, Drone.DroneStates.Enter);
  }

  public override void Exit()
  {
    Debug.Log("Drone Destroyed State Exited");
    anim.enabled = false;
  }

  public override void Update()
  {

  }
}
