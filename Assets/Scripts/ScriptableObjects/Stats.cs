using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class Stats : ScriptableObject
{
    public enum CharacterRole
    {
        Party,
        Enemy,
        Neutral
    }
    public string characterName;
    public CharacterRole role;
    public float health;
    public float mana;
    public float speed;
    public float damage;
    public float block; 
}
