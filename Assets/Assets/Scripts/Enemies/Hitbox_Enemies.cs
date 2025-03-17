using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Enemies : MonoBehaviour
{
    [SerializeField] public int _Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack player = collision.gameObject.GetComponent<PlayerAttack>();
            if (player != null)
            {
                player.TakeDamage(_Damage);
                Debug.Log("Player Health: " + player._currentHealth);
            }
        }
    }
}
