using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug_Bud_Controller : MonoBehaviour
{

    private Buds_Manager budsManager;

    [SerializeField] private GameObject budPartParticles;

    private void Start()
    {
        budsManager = GameObject.Find("Player").GetComponentInChildren<Buds_Manager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Kill Field")
        {
            Instantiate( budPartParticles, transform.position, Quaternion.identity );
            budsManager.removeBud(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Slug")
        {
            Instantiate(budPartParticles, transform.position, Quaternion.identity);
            budsManager.removeBud(this.gameObject);
        }
    }

}
