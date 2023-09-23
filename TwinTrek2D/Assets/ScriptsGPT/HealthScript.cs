using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float maxHealth = 100f; // La salud m�xima del jugador
   [SerializeField] private float currentHealth;   // La salud actual del jugador

    private void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud actual a la m�xima cuando comienza el juego
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(); // Si la salud llega a cero o menos, el jugador muere (puedes implementar tu l�gica de muerte aqu�)
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Limitar la salud m�xima para que no exceda el valor m�ximo
        }
    }

    private void Die()
    {
        // Aqu� puedes implementar la l�gica de lo que ocurre cuando el jugador muere
        // Por ejemplo, mostrar un mensaje de game over, reiniciar el nivel, etc.
    }
}
