using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem")]
public class ItemSO: ScriptableObject
{
    public string itemName;
    [TextArea]public string itemDescription;
    public Sprite icon;

    public bool isGold; // Indicates if the item is gold or not

    [Header("Stats")]
    public int health;
    public int maxHealth;
    public int damage;
    public int speed;

    [Header("For Temporary items")]
    public float duration; // Duration for temporary items
}
