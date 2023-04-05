using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> obstacles;
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float spawnRate = 2; 
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void StartSpawn()
    {
        StartCoroutine(SpawnObstacles());
    }*/

     IEnumerator SpawnObstacles()
    {
        while (playerControllerScript.gameOver == false)
        {
            if(playerControllerScript.dashSpeed == true)
            {
                yield return new WaitForSeconds(Random.Range(spawnRate / 2, (spawnRate + 2) / 2));
                int index = Random.Range(0, obstacles.Count);
                Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(spawnRate, (spawnRate + 2)));
                int index = Random.Range(0, obstacles.Count);
                Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
            }
            
        }


    }

    
}
