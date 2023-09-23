using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConstant : MonoBehaviour
{
    public float damageAmount = 10.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si el jugador está en la Zona de Recuperación
        if (other.CompareTag("Player"))
        {
            // No causar daño si el jugador está en la Zona de Recuperación
            return;
        }

        // Aplicar daño constante al jugador
        HealthScript playerHealth = other.GetComponent<HealthScript>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

}
