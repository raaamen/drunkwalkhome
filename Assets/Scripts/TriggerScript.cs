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
    public bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract){
                //do wahtever we set in the inspector
                Debug.Log("Interact");
                GameManager.Instance.interactBox.SetActive(false);
                colliderEvent.Invoke();
            }
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
            canInteract=true;
            
        }
          
    }
   
    void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.interactBox.SetActive(false);
        canInteract=false;
    }


}
