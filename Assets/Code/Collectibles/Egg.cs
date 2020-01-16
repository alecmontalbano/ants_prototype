using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Collectible
{
    [SerializeField] private GameObject reward;

    [SerializeField] private float spawnOffset = 1.0f;

    [SerializeField] private Buds_Manager budsManager;
    [SerializeField] private float spinSpeed = 100.0f;
    [SerializeField] private float bobSpeed = 20.0f;
    [SerializeField] private float bobHeight = 0.5f;

    [SerializeField] private GameObject shellParticles;

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * spinSpeed, 0);                                

        Vector3 pos = transform.position;

        float newY = Mathf.Sin((Time.time * bobSpeed)) + 1.5f;
        transform.position = new Vector3(pos.x, newY * bobHeight, pos.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if ( col.gameObject.name == "Player" )
        {
            Instantiate( shellParticles, transform.position, Quaternion.identity);
            budsManager.spawnBud( spawnOffset, col.gameObject, reward );
            Destroy(this.gameObject);
        }
    }

}
