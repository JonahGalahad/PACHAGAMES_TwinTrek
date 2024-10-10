using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy_mov enemy_mov;
    public Enemy_accAtrapar enemy_AccAtrapar;
    public bool EnemigoEstatico = false;

    public bool jugadorAtrapado = false;

    private void Start()
    {
        enemy_mov = GetComponent<Enemy_mov>();
        enemy_AccAtrapar = GetComponent<Enemy_accAtrapar>();
    }

    private void Update()
    {
        if(jugadorAtrapado || EnemigoEstatico)
        {
            return;
        }
        enemy_mov.Mover();
    }

    public void DejarMoverse()
    {
        enemy_mov.velocidadMovimiento = 0.0f;
    }

    public void PermitirMoverse()
    {
        enemy_mov.velocidadMovimiento = 2f;
    }
}
