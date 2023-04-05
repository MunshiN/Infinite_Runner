using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    //private float acceleration = .002f;
    private PlayerController playerControllerScript;
    private float leftBound = -10;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (playerControllerScript.gameOver == false)
        {
            if(playerControllerScript.dashSpeed == false)
            {
                //speed += Time.deltaTime + acceleration * Time.deltaTime;
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
           
            else if(playerControllerScript.dashSpeed == true)
            {
                if(!playerControllerScript.isOnGround)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                else
                {
                    //speed += (Time.deltaTime + acceleration * Time.deltaTime) * 2;
                    transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
                }
                

            }
            
        }
            

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
