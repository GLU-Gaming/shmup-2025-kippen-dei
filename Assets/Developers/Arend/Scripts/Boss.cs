using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class DashAttackState : IBossState
{
    public void Enter(BossStateMachine boss)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(BossStateMachine boss)
    {
        throw new System.NotImplementedException();
    }

    public void Update(BossStateMachine boss)
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.dashPoint, boss.dashSpeed * Time.deltaTime);
        if (Vector3.Distance(boss.transform.position, boss.dashPoint) < 0.1f)
        {
            boss.transform.position = boss.startPosition;
        }
    } 
}
