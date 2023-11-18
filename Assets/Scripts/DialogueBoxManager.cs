using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;
//using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class DialogueBoxManager : Singleton<DialogueBoxManager>
{
    //unity action
    public bool conversationOccuring;
    
    public List<Character> characters;

    public Queue<string> sentences;
    public GameObject textUIElements;
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI onScreenTextBox;
    public Conversation conversationObj;
    public bool dialogueOccuring;
    public float textOffset;
    public AudioClip textSFX;
    public AudioSource audioSource;
    public string currentDialogueName;

    public Conversation currentConvo;
    
    public DisplayObject dialogueChoice1;
    public DisplayObject dialogueChoice2;

    public Image characterPortrait;

    public bool dialogueChoice;
    
    public Character currentCharacterTalking;


    public void SetDialogueChoice(bool val){
        dialogueChoice=val;
    }
    
    void Awake() {
        sentences = new Queue<string>();
    //this is not functioning at the moment
    //charVoices = new Dictionary<string, AudioClip>();
     // foreach (KeyValuePair kvp in charVoicesList) {
      //  charVoices.Add(kvp.key, kvp.val);
     // }
      //PrintDictionary(charVoices);

      
    }
    //take in textasset from scriptableobject and turn it into queue
    public void StartDialogue(Conversation convoObj){
        conversationOccuring = true;
        Debug.Log("starting dialogue: "+convoObj.conversationName);
        currentDialogueName = convoObj.conversationName;
        ReadTextFile(convoObj.textFile);
        currentConvo = convoObj;
        textUIElements.SetActive(true);
        WriteDialogue();
    }

    public void AdvanceDialogue(){
        sentences.Dequeue();
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        else WriteDialogue();
    }

    public void WriteDialogue(){
        
        string currentSentence = sentences.Peek();
        
        if (string.IsNullOrEmpty(currentSentence) || string.IsNullOrWhiteSpace(currentSentence))
        {
            EndDialogue();
            return;
        }
        
        if (currentSentence[0] == '$')
        {
            Debug.Log("Name encountered");
            //set audio clip
            
            //character portrait work here
        
            foreach (var item in characters)
            {
                if (item.name == currentSentence.Substring(1)){
                    textBox.text = item.characterName;
                    characterPortrait.sprite = item.neutral;
                }
            }
            
            Debug.Log(currentSentence.Substring(1, 6));
            if (currentSentence.Substring(1, 6)=="Player"){
                
                currentCharacterTalking = characters[0];
                nameBox.text = currentCharacterTalking.name;
                switch (currentSentence.Substring(1))
                {
                    case "Player_Happy":
                        characterPortrait.sprite = currentCharacterTalking.happy;
                        break;
                    case "Player_Pigeon":
                        characterPortrait.sprite = currentCharacterTalking.sparkle;
                        break;
                    case "Player_Neutral":
                        characterPortrait.sprite = currentCharacterTalking.neutral;
                        break;
                    case "Player_Confused":
                        characterPortrait.sprite = currentCharacterTalking.confused;
                        break;
                }
            }
            

            //not functioning PrintDictionary(charVoices);
            //Debug.Log(charVoices.ContainsKey(nameBox.text));
            //audioSource.clip = charVoices[nameBox.text];
            
            sentences.Dequeue();
            currentSentence=sentences.Peek();
        }

        if (currentSentence.Substring(0,2) == "#1"){
            //display choice 
            
            Debug.Log("Choice 1 encountered");
            dialogueChoice=true;
            dialogueChoice1.gameObject.GetComponentInChildren<TMP_Text>().text = currentSentence.Substring(2);
            //WriteDialogue();
            sentences.Dequeue();
            currentSentence=sentences.Peek();
            Debug.Log(currentSentence + " is the next sentence");
            //sentences.Dequeue();
            //currentSentence=sentences.Peek();
            
        }
        if (currentSentence.Substring(0,2)=="#2"){
            Debug.Log("Choice 2 encountered");
            dialogueChoice2.gameObject.GetComponentInChildren<TMP_Text>().text = currentSentence.Substring(2);
            sentences.Dequeue();
            //currentSentence=sentences.Peek();
        }
        StartCoroutine(PrintOneByOne(currentSentence));
        /*
        if (currentSentence.Substring(0,2) == "#2"){
            //display choice
            dialogueChoice=true;
            dialogueChoice2.gameObject.GetComponentInChildren<TMP_Text>().text = currentSentence.Substring(2);
            sentences.Dequeue();
        }
        */
        
    }
    public void EndDialogue(){
        //clear queue
        sentences.Clear();
        dialogueOccuring=false;
        Debug.Log("dialogue ended");
        textUIElements.SetActive(false);
        conversationOccuring = false;
        //trigger event if there is one to be triggered
        /*
        
        if (string.IsNullOrWhiteSpace(currentConvo.eventTriggeredOnEnd))
        {
            return;
        }
        */
        //Debug.Log("invoking event: "+currentConvo.eventTriggeredOnEnd);
        //EventManager.TriggerEvent(currentConvo.eventTriggeredOnEnd);
    }
    public void ReadTextFile(TextAsset asset){
        //read in from conversation
        sentences.Clear();
        string content = asset.ToString();
        List<string> lines = content.Split('\n').ToList<string>();
        foreach (var item in lines)
        {
            sentences.Enqueue(item);
        }
    }

    public IEnumerator PrintOneByOne(string currentSentence){
        dialogueOccuring=true;
        Debug.Log("current sentence: "+currentSentence);
        string tempsentence = "";
        for (int i = 0; i < currentSentence.Length; i++)
        {
            tempsentence+=currentSentence[i].ToString();
            //Debug.Log(tempsentence);
            textBox.text = tempsentence;
            //audioSource.Play();
            yield return new WaitForSeconds(textOffset);
        }
        currentSentence="";
        tempsentence = "";
        dialogueOccuring=false;
        if (dialogueChoice){
            Debug.Log("Dialogue choice visible");
            dialogueChoice1.SetActive(true);
            dialogueChoice2.SetActive(true);
            yield return new WaitUntil(()=> dialogueChoice=false);
            
        }
        else {

        yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
        AdvanceDialogue();
        }
        yield return null;
    }

    public void Choice1Clicked(){
        WriteDialogue();
        sentences.Dequeue();
    }
    public void Choice2Clicked(){
        //dequeues choice 1
        //sentences.Dequeue();
        //AdvanceDialogue();
        sentences.Dequeue();
        WriteDialogue();
    }

    private void FixedUpdate() {
        if (Input.GetMouseButtonDown(0) && dialogueOccuring)
        {
            dialogueOccuring=false;
            StopAllCoroutines();
            textBox.text = sentences.Peek();
            AdvanceDialogue();
            
        }
        else return;
    }

    public string ConvoNameOccuring(){
        if (dialogueOccuring == false)
        {
            return null;
        }
        //todo
        return null;
    }

    public bool DialogueOccuring(){
        return dialogueOccuring;
    }
}
