using UnityEngine;

public interface IBossState
{
    void Enter(BossStateMachine boss);  // Wordt aangeroepen als de speler in deze state komt
    void Update(BossStateMachine boss); // Wordt aangeroepen in Update()
    void Exit(BossStateMachine boss);   // Wordt aangeroepen als de speler deze state verlaat
}
