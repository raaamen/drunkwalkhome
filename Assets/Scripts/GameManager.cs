using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public int health;
    public bool home;
    public DisplayObject interactBox;
    public Conversation testconvo;

    public bool gameRunning;

    

    // Start is called before the first frame update
    void Start()
    {
        gameRunning=true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.C)){
            DialogueBoxManager.Instance.StartDialogue(testconvo);
        }
        */
    }
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }


    public void EndGame(){
        if (health == 0){
            //die
        }
        else if (home){
            //home
        }
        else return;
    }

    
}
