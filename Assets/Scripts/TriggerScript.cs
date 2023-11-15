using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    public bool isInteractable;
    public UnityEvent colliderEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isInteractable){
            GameManager.Instance.interactBox.SetActive(true);
            GameManager.Instance.interactBox.GetComponent<TMP_Text>().text="Press E to interact";
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //if the object can be interacted with
        if (isInteractable && other.gameObject.tag=="Player"){
            Debug.Log("Colliding with player");
            if (Input.GetKeyDown(KeyCode.E)){
                //do wahtever we set in the inspector
                GameManager.Instance.interactBox.SetActive(false);
                colliderEvent.Invoke();
            }
            
        }
          
    }
   
    void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.interactBox.SetActive(false);
    }


}
