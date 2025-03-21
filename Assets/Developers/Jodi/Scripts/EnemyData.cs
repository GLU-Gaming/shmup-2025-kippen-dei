using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Bestiary/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemyIcon;
    [TextArea] public string description;
    public int health;
    public int damage;
    public GameObject enemyModel; 
}