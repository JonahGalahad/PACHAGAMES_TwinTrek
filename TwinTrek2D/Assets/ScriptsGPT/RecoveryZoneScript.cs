using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryZoneScript : MonoBehaviour
{
    public float recoveryRate = 5.0f; // Velocidad de recuperación de vida

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si el jugador está en la Zona de Recuperación
        if (other.CompareTag("Player"))
        {
            // Recuperar vida del jugador
            HealthScript playerHealth = other.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.Heal(recoveryRate * Time.deltaTime);
            }
        }
    }

    
}
