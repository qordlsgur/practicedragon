using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class FSM<T>
{
    private State<T> currentState;
    private State<T> saveState;
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

    public void SaveStaet()
    {
        if (currentState == null)
            return;

        if (currentState == saveState)
            return;

        saveState = currentState;
    }

    public void SaveReturn()
    {
        if (saveState != null)
            saveState = null;
    }

    public void Init(State<T> startState)
    {
        if (startState == null)
            return;

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

    public void ReturnState()
    {
        if (saveState == null)
            return;

        if (currentState == null)
            return;

        if (currentState == saveState)
            return;

        currentState = saveState;

        saveState?.Exit();

        currentState.Enter();
    }
}