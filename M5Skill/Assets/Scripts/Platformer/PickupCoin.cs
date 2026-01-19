using Unity.VisualScripting;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.coins += 1;
            Destroy(gameObject);
        }
    }
}
