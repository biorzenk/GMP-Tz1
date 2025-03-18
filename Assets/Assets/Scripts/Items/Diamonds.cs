using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack player = collision.GetComponent<PlayerAttack>();
            if (player != null)
            {
                player._Diamonds += 1;
            }
            Destroy(this.gameObject);
            Debug.Log("Player picked up a diamond.");
        }
    }
}
