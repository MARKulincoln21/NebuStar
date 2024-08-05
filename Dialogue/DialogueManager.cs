using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;


    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;

    private DialogueVariables dialogueVariables;

    private void Awake()
    {
       if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    // Update is called once per frame
    public static DialogueManager GetInstance() {
        if (instance != null) {
        return instance;
        }
        else {
            Debug.LogError("instance is Null!");
            return null;
        }
    }

    private void start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // choicesText = new TextMeshProUGUI[choices.Length];

        // int index = 0;
        // foreach (GameObject choice in choices) {
        //    choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        //   index++;
        // }
    }

    private void Update() {

        if(!dialogueIsPlaying) {
            // dialoguePanel.SetActive(false);
            return;
        }

        if (InputManager.GetInstance().GetSubmitPressed()) {

        ContinueStory();
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        if (inkJSON != null) {
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);
            
            dialogueVariables.StartListening(currentStory);


            ContinueStory();
        }
        else {
            Debug.LogError("inkJSON is null!");
        }
    }

    private IEnumerator ExitDialogueMode() {

        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private void ContinueStory() {
        if (currentStory == null) {
            Debug.LogError("currentStory is null. Cannot continue the story.");
        return;
        }

        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            }
        
        else {
            StartCoroutine(ExitDialogueMode());
        }

    }

    private void DisplayChoices() {
        if (currentStory == null) {
        Debug.LogError("currentStory is null. Cannot display choices.");
        return;
        }

        List<Choice> currentChoices = currentStory.currentChoices;


        if (currentChoices.Count > choices.Length) {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;

        Debug.Log(index);

        foreach(Choice choice in currentChoices) {
            if (index >= choices.Length) break;

            Debug.Log(choice.text);

            if (choicesText[index].text == null) {
                Debug.LogError("Choice GameObject is null at index: " + index);
                continue;
            }

            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            }

        for (int i = index; i < choices.Length; i++) {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
    
}
