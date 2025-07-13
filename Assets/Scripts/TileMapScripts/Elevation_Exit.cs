using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    [Tooltip("Collider gunung yang akan diaktifkan kembali")]
    public Collider2D[] mountainColliders;

    [Tooltip("Collider pembatas yang akan dinonaktifkan")]
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aktifkan kembali collider gunung
            foreach (Collider2D mountain in mountainColliders)
            {
                if (mountain != null)
                    mountain.enabled = true;
            }

            // Nonaktifkan collider pembatas
            foreach (Collider2D boundary in boundaryColliders)
            {
                if (boundary != null)
                    boundary.enabled = false;
            }

            // Ubah sorting order player agar terlihat berada di bawah lapisan foreground
            SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 5;
            }
        }
    }
}