using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float distanceBetween;


    private float distance;
    [SerializeField] private int dirigirseA = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dirigirseA == 1)
        {
            IrPuntoA();
        }
        else if(dirigirseA == 2)
        {
            IrPuntoB();
        }
    }

    public void IrPuntoA()
    {
        distance = Vector2.Distance(transform.position, pointA.transform.position);
        Vector2 direction = pointA.transform.position - transform.position;
        direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, pointA.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    public void IrPuntoB()
    {
        distance = Vector2.Distance(transform.position, pointB.transform.position);
        Vector2 direction = pointB.transform.position - transform.position;
        direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, pointB.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

}
