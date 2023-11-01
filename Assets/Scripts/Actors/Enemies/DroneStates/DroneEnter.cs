using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Drone/Enter")]
[Serializable]
public class DroneEnter : Zero.State<Drone.DroneStates>
{
  private Animator anim;
  [SerializeField]
  private Sprite sprite;
  private SpriteRenderer spriteRenderer;
  private Transform transform;

  public override void Init(Zero.StateContextBehaviour<Drone.DroneStates> context)
  {
    base.Init(context);
    anim = context.GetComponent<Animator>();
    spriteRenderer = context.GetComponent<SpriteRenderer>();
    transform = context.transform;
  }

  public override void Enter()
  {
    Debug.Log("Enter State");
    anim.enabled = false;
    spriteRenderer.sprite = sprite;
    // NOTE: set back to default position and rotation
    transform.position = new Vector3(0f, 3.41f, 0f);
    transform.rotation = Quaternion.Euler(0f, 0f, -180f);
    stateContext.TransitionInSeconds(2f, Drone.DroneStates.Idle);
  }

  public override void Exit()
  {
    Debug.Log("Exit enter State");
  }

  public override void Update() { }
}
