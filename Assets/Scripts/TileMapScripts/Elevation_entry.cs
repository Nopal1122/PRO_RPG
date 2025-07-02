using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_entry : MonoBehaviour
{
    [Tooltip("Collider gunung yang akan dinonaktifkan saat player masuk")]
    public Collider2D[] mountainColliders;

    [Tooltip("Collider pembatas yang akan diaktifkan saat player masuk")]
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Nonaktifkan collider gunung
            foreach (Collider2D mountain in mountainColliders)
            {
                if (mountain != null)
                    mountain.enabled = false;
            }

            // Aktifkan collider pembatas
            foreach (Collider2D boundary in boundaryColliders)
            {
                if (boundary != null)
                    boundary.enabled = true;
            }

            // Naikkan sorting order player agar terlihat di depan layer
            SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 15;
            }
        }
    }
}
