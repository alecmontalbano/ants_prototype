using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Destroy(this.gameObject);
    }
}
