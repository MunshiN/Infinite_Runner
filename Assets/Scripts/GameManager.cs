using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform startingPoint;
    public float lerpSpeed;
    private PlayerController playerControllerscript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerscript = GameObject.Find("Player").GetComponent<PlayerController>();    
        playerControllerscript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayIntro()
    {
        
            Vector3 startPos = playerControllerscript.transform.position;
            Vector3 endPos = startingPoint.position;
            float journeyLength = Vector3.Distance(startPos, endPos);
            float startTime = Time.time;
            float distanceCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            playerControllerscript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

            while (fractionOfJourney < 1)
            {
                distanceCovered = (Time.time - startTime) * lerpSpeed;
                fractionOfJourney = distanceCovered / journeyLength;
                playerControllerscript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                yield return null;
            }
        

        playerControllerscript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerscript.gameOver = false;
    }

}
