using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Zero
{
  [Serializable]
  public class StateContextBehaviour<EState> : MonoBehaviour where EState : Enum
  {
    [SerializeField]
    protected EState initialState;

    [SerializeField]
    protected EState currentStateKey;
    [SerializeField]
    protected State<EState> currentState;
    [SerializeField]
    protected Dictionary<EState, State<EState>> states = new Dictionary<EState, State<EState>>();
    [SerializeField]
    protected List<State<EState>> stateList = new();

    /// <summary>
    /// This is the first function that should be invoked for each StateContextBehaviour in either Awake or Start
    /// </summary>
    protected void InitializeState(EState initialState)
    {
      foreach (var s in stateList)
      {
        s.Init(this);
      }
      currentStateKey = initialState;
      currentState = states[initialState];
    }

    public void ChangeState(State<EState> newState)
    {
      currentState.Exit();
      currentState = newState;
      currentStateKey = newState.StateKey;
      currentState.Enter();
    }

    public void ChangeState(EState newState)
    {
      currentState.Exit();
      currentState = states[newState];
      currentStateKey = newState;
      currentState.Enter();
    }

    public void AddState(EState key, State<EState> state)
    {
      states.Add(key, state);
    }

    public void CancelTransition()
    {
      StopAllCoroutines();
    }

    public void TransitionInSeconds(float t, EState key)
    {
      StartCoroutine(WaitForSecondsTransition(t, key));
    }

    private IEnumerator WaitForSecondsTransition(float t, EState key)
    {
      yield return new WaitForSeconds(t);
      ChangeState(key);
    }

    private IEnumerator WaitUntilPredicateTransition(Func<bool> predicate, EState key)
    {
      yield return new WaitUntil(predicate);
      ChangeState(key);
    }
  }

  public abstract class State<EState> : ScriptableObject where EState : Enum
  {
    [SerializeField]
    private EState key;

    protected StateContextBehaviour<EState> stateContext;

    public virtual void Init(StateContextBehaviour<EState> context)
    {
      stateContext = context;
      StateKey = key;
      stateContext.AddState(StateKey, this);
    }

    public EState StateKey { get; private set; }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    // public abstract void OnTriggerEnter2D(Collider2D collider);
    // public abstract void OnTriggerExit2D(Collider2D collider);
    // public abstract void OnCollisionEnter2D(Collision2D collision);
    // public abstract void OnCollisionExit2D(Collision2D collision);
  }
}

