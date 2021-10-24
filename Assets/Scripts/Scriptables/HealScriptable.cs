using UnityEngine;

[CreateAssetMenu(fileName = "New heal powerup", menuName = "Scriptables/Powerups/Heal player")]
public class HealScriptable : ScriptableObject
{
    [Header("Set healing point")]
    public int healPoints;
}
