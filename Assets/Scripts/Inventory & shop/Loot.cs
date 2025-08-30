using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header("Loot Settings")]
    public ItemSO itemSO;          // ScriptableObject data item
    public int quantity = 1;

    [Header("Components")]
    public SpriteRenderer sr;
    public Animator anim;

    public bool canBePickedUp = true;

    public static event Action<ItemSO, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSO == null) return;
        UpdateAppearance();
    }

    public void Initialize(ItemSO itemSO, int quantity)
    {
        
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedUp = false;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        if (sr != null && itemSO != null)
        {
            sr.sprite = itemSO.icon;
            gameObject.name = itemSO.itemName;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp)
        {
            if (anim != null) anim.Play("LootPickup");

            OnItemLooted?.Invoke(itemSO, quantity);

            Destroy(gameObject, 0.5f); // tunggu animasi pickup dulu
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }
}
