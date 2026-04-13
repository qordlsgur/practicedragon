using UnityEngine;

public class FSM<T>
{
    private State<T> currentState;

    public State<T> CurrentState => currentState;

    public void ChangeState(State<T> newState)
    {
        if (newState == null)
            return;

        if (currentState == newState)
            return;

        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void Init(State<T> startState)
    {
        currentState = startState;

        currentState.Enter();
    }

    public void Update()
    {
        currentState?.FSMUpdate();
    }

    public void fixedUpdate()
    {
        currentState?.FSMFixedUpdate();
    }
}