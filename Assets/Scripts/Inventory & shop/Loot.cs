using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
   public ItemSO itemSO;// Reference to the ItemSo scriptable object
    public SpriteRenderer sr;
    public Animator anim;

    public int quantity;
    public static event Action<ItemSO, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSO == null)
            return;

            sr.sprite = itemSO.icon;// Set the sprite of the SpriteRenderer to the icon of the ItemSo
            this.name = itemSO.itemName; // Set the name of the GameObject to the item name


    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity); // Invoke the event with the item and quantity
            Destroy(gameObject, 0.5f); // Destroy the loot after a short delay to allow the animation to play
        }
    }
}
