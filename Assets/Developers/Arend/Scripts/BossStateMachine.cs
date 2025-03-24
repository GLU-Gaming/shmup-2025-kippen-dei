
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public IBossState currentState;
    
    private Rigidbody rb;

    public Vector3 startPosition;

    [Header("DashAttack Settings")]
    public float dashSpeed;
    public Vector3 dashPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // currentState = new IdleState();\
       // currentState.enter(this)
    }

    void Update()
    {
        currentState.Update(this);
    }
    public void ChangeState(IBossState newState)
    {
        currentState.Exit(this); // Exit de oude state
        currentState = newState;
        currentState.Enter(this); // Enter de nieuwe state
    }

    void ResetToIdle()
    {
       // ChangeState(new IdleState());
    }
}
