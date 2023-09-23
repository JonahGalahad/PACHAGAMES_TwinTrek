using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConstant : MonoBehaviour
{
    public float damageAmount = 10.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si el jugador est� en la Zona de Recuperaci�n
        if (other.CompareTag("Player"))
        {
            // No causar da�o si el jugador est� en la Zona de Recuperaci�n
            return;
        }

        // Aplicar da�o constante al jugador
        HealthScript playerHealth = other.GetComponent<HealthScript>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

}
