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
    

    public DisplayObject dialogueBox;


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
        else if (currentSentence[0] == '$')
        {
            //set audio clip
            nameBox.text = currentSentence.Substring(1);
            //not functioning PrintDictionary(charVoices);
            //Debug.Log(charVoices.ContainsKey(nameBox.text));
            //audioSource.clip = charVoices[nameBox.text];
            sentences.Dequeue();
        }
        StartCoroutine(PrintOneByOne(sentences.Peek()));
    }
    public void EndDialogue(){
        //clear queue
        sentences.Clear();
        dialogueOccuring=false;
        Debug.Log("dialogue ended");
        textUIElements.SetActive(false);
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
        yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
        AdvanceDialogue();
        yield return null;
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
