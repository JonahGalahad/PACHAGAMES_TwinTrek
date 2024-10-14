using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de agregar esta línea para usar TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 300f; // 5 minutos en segundos
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Ajusta el tiempo inicial a 300 segundos (5 minutos)
        totalTime = 300f;
        UpdateTimer();
    }

    private void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            // Aquí puedes definir la acción al llegar a 00:00
            // Por ejemplo, cambiar de escena o mostrar un mensaje.
        }
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        textMeshPro.text = $"{minutes:D2}:{seconds:D2}";
    }
}
