using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase tiene un Iput Axe en unity, (agrega otra tecla mas a la lista de los 30, la ultima agregada se le pone el nombre "Agacharse" y en Positive Button se le coloca "c" y listo
public class Jugador_accAgacharse : MonoBehaviour
{
    private Animator agacharse;
    // Start is called before the first frame update
    private void Start()
    {
        agacharse = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Agacharse"))
        {

            agacharse.GetComponent<Animator>().SetBool("Agacharse", true);

        }
        else
        {
            agacharse.GetComponent<Animator>().SetBool("Agacharse", false);


        }
    }
}
