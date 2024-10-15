using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin_Controller : MonoBehaviour
{
    //atributos de objetos
    [SerializeField]private GameObject rock;
    [SerializeField]private GameObject trampolin;
    private Vector2 initPosRock;
    private Vector2 initPosTramp;
    private bool isMoved = false;

    //atributos del temporizador
    private int currentTime=0;
    [SerializeField]private int limitTime=0;
    private bool isTime = false;

    //atrrbutos de reinicio
    private bool isMoveFinish = false;
    public bool IsMoveFinish { get { return isMoveFinish; } set { isMoveFinish = value; } }
    
    void Start()
    {
        initPosRock = rock.transform.position;
        initPosTramp = trampolin.transform.position;
    }

    void Update()
    {
        CountingTime();
        ResetMechanic();
    }
    private void ResetMechanic(){
        if(isMoved==true && isTime==true){
            rock.transform.position = initPosRock;
            trampolin.transform.position = initPosTramp;
            isMoved=false;
            isTime=false;
        }
    }
    private void CountingTime() {
        if (isMoveFinish == true){
            isTime=true;
            isMoveFinish = false;
        }
    }
}
