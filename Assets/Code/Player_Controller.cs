using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player_Controller : MonoBehaviour
{

    private float vAxis;
    private float hAxis;

    private GameObject go;
    private Transform tr;
    private Rigidbody rb;
    private Animator anim;
    private float vertVelocity;

    public bool grounded;

    private Vector3 damageKnockback;

    private AudioSource audioSource;

    [SerializeField] AudioClip playerdeathClip; 

    // settings
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float rotateSpeed = 1.0f;
    [SerializeField] private float jumpPower = 6.0f;

    [SerializeField] private float animatorSpeed = 0.25f;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        tr = this.transform;
        go = this.gameObject;
        anim = this.GetComponentInChildren<Animator>();

        anim.speed = animatorSpeed;
    }

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if ( hAxis != 0 )
        {
            tr.Rotate(0, hAxis * rotateSpeed, 0, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            vertVelocity = jumpPower;
        }
        else
        {
            vertVelocity = 0.0f;
        }
          
    }

    void FixedUpdate()
    {
        anim.SetBool("IsRunning", false);

        if (vAxis != 0)
        {
            anim.SetBool("IsRunning", true);
        }
         
        rb.velocity = new Vector3(tr.forward.x * moveSpeed * vAxis, rb.velocity.y + vertVelocity, tr.forward.z * moveSpeed * vAxis);
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            grounded = true;
        }
        else if ( theCollision.gameObject.name == "Slug" )
        {
            //dealKnockback(theCollision);
            KillPlayer();
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if ( col.gameObject.name =="Kill Field" )
        {
            KillPlayer();
        }
        else if ( col.gameObject.name == "Goal Field" )
        {
            StartCoroutine(WinDelay(1));
        }
    }

    private void KillPlayer ()
    {
        audioSource.PlayOneShot(playerdeathClip);
        anim.SetBool("IsRunning", false);
        anim.SetBool("Dead", true);
        StartCoroutine( KillDelay(0.25f) );      
    }

    private void dealKnockback( Collision col )
    {
        damageKnockback = rb.transform.position - col.transform.position;
        rb.AddForce(damageKnockback.normalized * 2000f);
    }

    IEnumerator KillDelay( float delayTime )
    {
        yield return new WaitForSeconds(delayTime);

        rb.isKinematic = true;

        StartCoroutine( GameOverDelay( 1.5f ) );
    }

    IEnumerator GameOverDelay( float delayTime )
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2 );
    }

    IEnumerator WinDelay( float delayTime )
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
