using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    //  ESTE SCRIPT SOLO ES PARA PROBAR LA MECANICA, EL OTRO SCRIPT ME SALTA ERRORES POR LOS LAYERS XP
    [SerializeField] private float movementSpeed = 5f;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) this.transform.Translate(Vector3.left * (movementSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.D)) this.transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));
    }


}