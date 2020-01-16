using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buds_Manager : MonoBehaviour
{
    private float vAxis;
    private float hAxis;

    private GameObject go;

    private GameObject player;
    private Player_Controller playerController;

    public List<GameObject> buds;

    private AudioSource audioSource;
    [SerializeField] private AudioClip eggCollectClip;
    [SerializeField] private AudioClip budDeathClip;

    [SerializeField] private Score_Manager scoreManager;

    [SerializeField] private float budSpeed = 10.0f;
    [SerializeField] private float budRotationSmoothAmount = 10.0f;

    void Start()
    {
        go = this.gameObject;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<Player_Controller>();
        buds = new List<GameObject>();
        scoreManager.scoreValue = 0;
        PlayerPrefs.SetInt("Player Score", 0);
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
        //Debug.Log(buds.Count);

        if (buds.Count > 0)
        {
            foreach (GameObject bud in buds)
            {
                if (bud != null)
                {
                    if ( !playerController.grounded )
                    {
                        budSpeed = 20.0f;
                    }
                    else
                    {
                        budSpeed = 9.5f;
                    }

                    if (buds.IndexOf(bud) == 0)
                    {
                        Transform wayPointToFollowPlayer = GameObject.Find("Player/wayPoint").transform;

                        bud.transform.position = Vector3.MoveTowards(bud.transform.position, wayPointToFollowPlayer.position, budSpeed * Time.deltaTime);

                        if (bud.transform.rotation != player.transform.rotation)
                        {
                            StartCoroutine(RotateBud(bud, player.transform, 0.25f));
                        }
                    }
                    else
                    {
                        int parentIndex = buds.IndexOf(bud) - 1;
                        Transform parentBudTransform = buds[parentIndex].transform;
                        Transform wayPointToFollow = parentBudTransform.GetChild(1);

                        bud.transform.position = Vector3.MoveTowards(bud.transform.position, wayPointToFollow.position, budSpeed * Time.deltaTime);

                        if (bud.transform.rotation != parentBudTransform.rotation)
                        {
                            StartCoroutine(RotateBud(bud, parentBudTransform, 0.25f));
                        }
                    }
                }

            }
        }
    }

    void FixedUpdate()
    {
        foreach (GameObject bud in buds)
        {

            Animator anim = bud.GetComponentInChildren<Animator>();

            anim.SetBool("IsRunning", false);

            if (vAxis != 0)
            {
                anim.SetBool("IsRunning", true);
            }

        }
    }


    IEnumerator RotateBud( GameObject bud, Transform parentTransform, float delayTime )
    {
        yield return new WaitForSeconds(delayTime);
        if ( bud != null && parentTransform != null )
        {
            bud.transform.rotation = Quaternion.Lerp(bud.transform.rotation, parentTransform.rotation, Time.deltaTime * budRotationSmoothAmount);
        }
    }

    private IEnumerator SubractScore( int value, float delayTime )
    {
        yield return new WaitForSeconds(delayTime);
        scoreManager.scoreValue -= 1;
        PlayerPrefs.SetInt("Player Score", scoreManager.scoreValue);
    }

    public void spawnBud(float offset, GameObject parent, GameObject bud)
    {
        audioSource.PlayOneShot(eggCollectClip);
        buds.Add( Instantiate(bud, parent.transform.position - parent.transform.forward * (offset * buds.Count/2 ), Quaternion.identity) );
        scoreManager.scoreValue += 1;
        PlayerPrefs.SetInt("Player Score", scoreManager.scoreValue);
    }

    public void removeBud( GameObject bud )
    {
        audioSource.PlayOneShot( budDeathClip );
        buds.Remove( bud );
        Destroy( bud );
        StartCoroutine( SubractScore( 1, 1 ) );
    }

}
