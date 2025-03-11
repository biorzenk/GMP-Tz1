using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAttack player = other.GetComponent<PlayerAttack>();
            if (player != null)
            {
                if (player._currentHealth < player._MaxHealth)
                {
                    player._currentHealth += 1;
                }
                Destroy(this.gameObject);
                Debug.Log("Player picked up a heart.");
            }
        }
    }
}
