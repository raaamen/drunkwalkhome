using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Conversation")]
public class Conversation : ScriptableObject
{
    public string conversationName;
    public string participants;
    public string description;
    //the text file is where the raw dialogue goes in. interpreted in dialoguemanager
    public TextAsset textFile;

    public string eventTriggeredOnEnd;

}
