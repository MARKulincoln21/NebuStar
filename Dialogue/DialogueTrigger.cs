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

    private static DialogueTrigger instance;

    private bool playerInRange;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        playerInRange = false;
        visualCue.SetActive(false);
    }

    public static DialogueTrigger GetInstance() {
        if (instance != null) {
        return instance;
        }
        else {
            Debug.LogError("instance is Null!");
            return null;
        }
    }

    
    private void Update() {
    if(playerInRange && DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().dialogueIsPlaying && DialogueManager.GetInstance().stageOneDialogueFinished == false || playerInRange && DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().trainingIsPlaying && DialogueManager.GetInstance().stageOneDialogueFinished == false) {
        visualCue.SetActive(true);
        if (InputManager.GetInstance() != null && InputManager.GetInstance().GetInteractPressed() && DialogueManager.GetInstance().stageOneDialogueFinished == false) {
            if (inkJSON != null) {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            } else {
                Debug.LogError("inkJSON is not assigned!");
            }
        }
    }

    else if (playerInRange && DialogueManager.GetInstance().dialogueIsPlaying || playerInRange && DialogueManager.GetInstance().trainingIsPlaying) {
        visualCue.SetActive(false);
    }
    else {
        visualCue.SetActive(false);
    }

}
    



    public void EnterTraining() {
        DialogueManager.GetInstance().EnterTrainMode(inkJSON);
    }

    public void EnterBattleTraining() {
        DialogueManager.GetInstance().EnterTrainBattleMode(inkJSON);
    }

    public void ContinueDialogue() {
        DialogueManager.GetInstance().ContinueDialogueMode(inkJSON);
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
