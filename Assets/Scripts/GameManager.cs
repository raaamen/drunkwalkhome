using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent StartGame;

    public TMP_Text inventoryText;
    public int health;
    public bool home;
    public DisplayObject interactBox;
    public Conversation testconvo;

    public bool gameRunning;

    public List<Item> inventory;

    public bool hasBread;
    public Conversation pigeonHasBread;
    public Conversation pigeonNoHasBread;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        StartGame.Invoke();
    }
    
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


    public void GiveItem(Item item){
        inventory.Add(item);
    }

    public IEnumerator PigeonConversation(){
        if (hasBread){
            DialogueBoxManager.Instance.StartDialogue(pigeonHasBread);
            yield return new WaitUntil(()=> DialogueBoxManager.Instance.conversationOccuring == false);
            inventoryText.gameObject.SetActive(true);
            inventoryText.text = "You got a pigeon~!";
            //maybe play sound effect
            yield return new WaitForSeconds(2);
            inventoryText.gameObject.SetActive(false);
        }
        else {
            DialogueBoxManager.Instance.StartDialogue(pigeonNoHasBread);
        }
        yield return null;
    }

    public void RunCoroutine(string coroutine){
        StartCoroutine(coroutine);
    }

    
}
