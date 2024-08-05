using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake() {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    
    private void Update() {
    if(playerInRange && DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().dialogueIsPlaying) {
        visualCue.SetActive(true);
        if (InputManager.GetInstance() != null && InputManager.GetInstance().GetInteractPressed()) {
            if (inkJSON != null) {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            } else {
                Debug.LogError("inkJSON is not assigned!");
            }
        }
    }
    else {
        visualCue.SetActive(false);
    }
}

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            playerInRange = true;
            Debug.Log("I have entered into his field");
        }    
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }

}
