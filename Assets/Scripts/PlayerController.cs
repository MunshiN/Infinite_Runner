using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody rigidBody;
    private float jumpForce = 450;
    private float gravityModifier = 1.1f;
    public bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionSmoke;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    private int score;
    private bool secJump;
    public bool dashSpeed;
    private float ScreenWidth;
    //public GameObject gO;
    
    /*public GameObject titleScreen;
    private SpawnManager spawnManager;*/
    


    // Start is called before the first frame update
    void Start()
    {
        //spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        rigidBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.8f, 0) * gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        score = 0;
        StartCoroutine(UpdateScore());
        ScreenWidth = Screen.width;
        

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            secJump = true;
        }

        else
        {
            if(secJump == true && Input.GetKeyDown(KeyCode.Space)) 
            {
                rigidBody.AddRelativeForce(Vector3.up * (3*jumpForce/4), ForceMode.Impulse);
                secJump=false;
            }

            
        }

        if (Input.GetKey(KeyCode.Z))
        {
            dashSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (dashSpeed)
        {
            dashSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
#endif
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
               


                    if (Input.GetTouch(i).phase == TouchPhase.Began && isOnGround && !gameOver && Input.GetTouch(i).position.x < ScreenWidth/4)
                    {
                        
                        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isOnGround = false;
                        playerAnim.SetTrigger("Jump_trig");
                        dirtParticle.Stop();
                        playerAudio.PlayOneShot(jumpSound, 1.0f);
                        secJump = true;
                    }

                    else
                    {
                        if (secJump == true && Input.GetTouch(i).phase == TouchPhase.Began && !gameOver && Input.GetTouch(i).position.x < ScreenWidth / 4)
                        {
                            
                            isOnGround = false;
                            playerAnim.SetTrigger("Jump_trig");
                            dirtParticle.Stop();
                            playerAudio.PlayOneShot(jumpSound, 1.0f);
                            rigidBody.AddRelativeForce(Vector3.up * (3 * jumpForce / 4), ForceMode.Impulse);
                            secJump = false;
                        }


                    }

                if (Input.GetTouch(i).phase == TouchPhase.Stationary && isOnGround && !gameOver && Input.GetTouch(i).position.x > (ScreenWidth*3) / 4 && Input.GetTouch(i).position.x <(ScreenWidth*4)/5 && Input.GetTouch(i).position.y < ScreenWidth / 8 )
                {
                    Dash();

                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended && isOnGround && !gameOver && Input.GetTouch(i).position.x > (ScreenWidth * 3) / 4 && Input.GetTouch(i).position.x < (ScreenWidth * 4)/5 && Input.GetTouch(i).position.y < ScreenWidth / 8 )
                {
                    dashSpeed = false;
                    playerAnim.SetFloat("Speed_Multiplier", 1.0f);
                    UpdateScore();
                }


            }
        }

        



    }
    
    public void Dash()
    {
       dashSpeed = true;
        playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        UpdateScore();
    }

    

    IEnumerator UpdateScore()
    {
        while(gameOver == false)
        {
            if (dashSpeed == false)
            {
                score += 1;
                yield return new WaitForSeconds(.1f);
                scoreText.text = "Score: " + score;
            }
            else
            {
                score += 2;
                yield return new WaitForSeconds(.1f);
                scoreText.text = "Score: " + score;
            }
            
        }
    }

   /* public void GameStart(int select)
    {
        gameOver = false;
        rigidBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.8f, 0) * gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        score = 0;
        
        StartCoroutine(UpdateScore());
        ScreenWidth = Screen.width;
        titleScreen.gameObject.SetActive(false);
       StartCoroutine(spawnManager.SpawnObstacles());
    }*/

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        gameOver = true;
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionSmoke.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            GameOver();
        }
    }

    public void RestartGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
