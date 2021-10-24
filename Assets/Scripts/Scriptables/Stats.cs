using UnityEngine;

[CreateAssetMenu(fileName = "New stat", menuName = "Scriptables/Stats/Enemy stats")]
public class Stats : ScriptableObject
{
    [Header("X* multiplier by value")]
    public int enemyHealthPoints;
    public int enemyDamagePoints;

}
