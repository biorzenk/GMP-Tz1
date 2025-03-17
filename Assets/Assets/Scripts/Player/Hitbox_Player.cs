using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Player : MonoBehaviour
{

    [SerializeField] public int _Damage;


    private void Awake()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemies_Move enemy = collision.gameObject.GetComponent<Enemies_Move>();
            if (enemy != null)
            {
                enemy.TakeDamage(_Damage);
            }

            Bat_Enemies bat_Enemies = collision.gameObject.GetComponent<Bat_Enemies>();
            if (bat_Enemies != null)
            {
                bat_Enemies.TakeDamage(_Damage);
            }

            AI_666 aI_666 = collision.gameObject.GetComponent<AI_666>();
            if (aI_666 != null)     
            {
                aI_666.TakeDamage(_Damage);
            }
        }
    }
}
