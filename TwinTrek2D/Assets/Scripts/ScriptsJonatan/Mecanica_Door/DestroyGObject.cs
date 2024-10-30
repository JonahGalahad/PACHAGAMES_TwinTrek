using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGObject : MonoBehaviour
{
    private void Destroy() {
        Destroy(this.gameObject);
    }
}
