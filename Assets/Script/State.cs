using UnityEngine;

public abstract class State<T>
{
    protected T owner;
    protected FSM<T> fsm;

    public State(T owner, FSM<T> fsm)
    {
        this.owner = owner;
        this.fsm = fsm;
    }

    public virtual void Enter()  { }
    public virtual void FSMUpdate() { }
    public virtual void FSMFixedUpdate() { }
    public virtual void Exit() { }
    public virtual void OnAttackInput() { }
    public virtual void OnJumpInput() { }
}

