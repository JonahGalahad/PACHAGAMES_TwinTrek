using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryZoneScript : MonoBehaviour
{
    public float recoveryRate = 5.0f; // Velocidad de recuperaci�n de vida

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si el jugador est� en la Zona de Recuperaci�n
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
