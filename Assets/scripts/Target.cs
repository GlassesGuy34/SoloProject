using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public ParticleSystem hitEffect;


    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        GameObject playerControllerObject = GameObject.FindWithTag("playerController");

        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
            print("found the player script");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        rigidbody2D.simulated = false;
        //print("Hit the target!");
        Destroy(gameObject);
        Instantiate(hitEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        if (playerController !=null)
        {
            playerController.ChangeScore(1);
        }
    }

}
