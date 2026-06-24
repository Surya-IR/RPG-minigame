using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
[Serializable]
public class Items : ScriptableObject
{
    public enum ItemType
    {
        heal,
        damage,
        debuff
    }

    public string itemID;
    public string itemName;
    public ItemType type;
    public float effectNumber;
    public float amount;
}
