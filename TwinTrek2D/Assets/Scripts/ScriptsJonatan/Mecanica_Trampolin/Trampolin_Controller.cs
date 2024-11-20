using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin_Controller : MonoBehaviour
{
    //atributos de objetos
    [SerializeField]private GameObject rock;
    [SerializeField]private GameObject trampolin;
    [SerializeField]private Vector3 initPosRock;
    [SerializeField] private Quaternion initRotRock;
    [SerializeField]private float initPosTramp;
    [SerializeField] private Rigidbody2D rocaRigid;
    private bool isMoved = false;
    public bool IsMoved { get { return isMoved; } set { isMoved = value; } }

    //atributos del temporizador
    [SerializeField]private float currentTime=0;
    [SerializeField]private float limitTime;
    private bool isTime = false;

    //atrbutos de reinicio
    private bool isMoveFinish = false;
    public bool IsMoveFinish { get { return isMoveFinish; } set { isMoveFinish = value; } }
    
    void Start()
    {
        initPosRock = new Vector3(rock.transform.position.x,rock.transform.position.y,0); //posicion inicial de la roca
        initRotRock = new Quaternion(rock.transform.rotation.x,rock.transform.rotation.y,0,0);

        initPosTramp = trampolin.transform.rotation.z; //posicion de la rotacion inicial del trampolin
        currentTime = limitTime; //tiempo de la cuenta regresiva inicial para que se reinicie la mecanica
        rocaRigid = rock.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        StartCounting();
        ResetMechanic();
    }

    // resetea todos los objetos de la mecanica a su posicion inicial y reinicia el tiempo de la cuenta regresiva
    private void ResetMechanic(){
        if(isMoved==true && isTime==true){
            rock.transform.position = initPosRock;

            rocaRigid.velocity = Vector2.zero;
            rock.transform.rotation = initRotRock;
            //trampolin.transform.rotation = new Quaternion(0,0,initPosTramp,0);
            isMoved=false;
            isTime=false;
            currentTime=limitTime;
        }
    }

    //cuenta regresiva desde que reiniciar mecanicas
    private void CountingTime() {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            isTime=true;
            isMoveFinish=false;
        }
    }

    //disparador de la cuenta regresiva 
    private void StartCounting() {
        if (isMoveFinish == true) {
            CountingTime();
        }
    }
}
