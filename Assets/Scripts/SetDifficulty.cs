using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    private PlayerController playerController;
    public int select;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        button = GetComponent<Button>();
        //button.onClick.AddListener(SetDifficult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SetDifficult()
    {
        playerController.GameStart(select);
    }*/
}
