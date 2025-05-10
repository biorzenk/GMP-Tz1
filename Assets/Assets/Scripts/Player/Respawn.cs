using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;

    private PlayerAttack playerAttack;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // Automatically find the player GameObject by tag
            if (player == null)
            {
                Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
                return;
            }
        }

        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not assigned in the Inspector.");
            return;
        }

        playerAttack = player.GetComponent<PlayerAttack>();
        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack component not found on the Player GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerAttack != null && playerAttack.isDead)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            Debug.Log("Player respawned at the respawn point.");
        }
        else
        {
            Debug.LogError("Player or Respawn Point is not assigned.");
        }
    }
}