using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int health;

    public Conversation testconvo;

    // Start is called before the first frame update
    void Start()
    {
        
        //DialogueBoxManager.Instance.StartDialogue(testconvo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            DialogueBoxManager.Instance.StartDialogue(testconvo);
        }
    }
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
