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

    [Header("Training UI")]

    [SerializeField] private GameObject trainingCanvas;
    [SerializeField] private TextMeshProUGUI trainingText;

    [Header("TrainBattleDialogue UI")]

    [SerializeField] private GameObject trainBattlePanel;
    [SerializeField] private TextMeshProUGUI trainingBattleText;

    [Header("TrainBattleCamera UI")]
    [SerializeField] private GameObject trainBattleCamera;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    [Header("TrainCamera UI")]
    [SerializeField] private GameObject trainCamera;


    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;


    [Header("TrainingChoices UI")]

    [SerializeField] private GameObject[] trainchoices;

    private TextMeshProUGUI[] trainchoicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    public bool trainingIsPlaying { get; private set; }

    public bool trainActivation { get; private set; }

    public bool trainBattleActivation { get; set; }
    public bool trainBattleIsPlaying { get; private set; }
    private static DialogueManager instance;
    private DialogueVariables dialogueVariables;

    private const string TRAINING_TAG = "Trigger_training";
    private const string TRAINBATTLE_TAG = "Trigger_trainBattle";

    [SerializeField] private BattleSystem bSystem;
    [SerializeField] private BattleDialogueBox dBox;

    private void Awake()
    {
       if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        trainingIsPlaying = false;
        trainingCanvas.SetActive(false);
        trainCamera.SetActive(false);
        trainActivation = false;

        trainBattleIsPlaying = false;
        trainBattlePanel.SetActive(false);
        trainBattleCamera.SetActive(false);
        trainBattleActivation = false;

        // DialogueChoices

        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        // TrainChoices

        trainchoicesText = new TextMeshProUGUI[trainchoices.Length];

        int indexTrain = 0;
        foreach (GameObject choice in trainchoices) {
            trainchoicesText[indexTrain] = choice.GetComponentInChildren<TextMeshProUGUI>();
            indexTrain++;
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

    private void Start() {
        }
    

    public void Update() {
        if(dialogueIsPlaying && !trainingIsPlaying && !trainBattleIsPlaying) {
            Debug.Log("DialogueIsPlaying");
                if (InputManager.GetInstance().GetSubmitPressed()) {
                    ContinueStory();
                }
            }

        else if (!dialogueIsPlaying && trainingIsPlaying && !trainBattleIsPlaying) {
            Debug.Log("trainingIsPlaying");
             if (InputManager.GetInstance().GetSubmitPressed()) {
                    ContinueStory();
                }
        }

        else if (!dialogueIsPlaying && !trainingIsPlaying && trainBattleIsPlaying && !trainBattleActivation) {
            dialogueVariables.StartListening(currentStory);
            // Debug.Log("trainBattleIsPlaying");
             if (InputManager.GetInstance().GetSubmitPressed()) {
                    ContinueStory();
                }
        }

        else if(!dialogueIsPlaying && !trainingIsPlaying && !trainBattleIsPlaying) {
            return;
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

    public void EnterTrainMode(TextAsset inkJSON) {
         if (trainingText == null) {
            Debug.LogError("TrainingText is not assigned!");
        }
        if (inkJSON != null) {
            Debug.Log(inkJSON.text);
            trainingIsPlaying = true;
            trainingCanvas.SetActive(true);
            trainCamera.SetActive(true);
            trainingText.text = currentStory.Continue();
            dialogueVariables.StartListening(currentStory);
            ContinueStory();
        }
        else {
            Debug.LogError("inkJSON is null!");
        }
    }

     public void EnterTrainBattleMode(TextAsset inkJSON) {
         if (trainingBattleText == null) {
            Debug.LogError("TrainBattleText is not assigned!");
        }
        if (inkJSON != null) {
            Debug.Log(inkJSON.text);
            trainBattleIsPlaying = true;
            trainBattlePanel.SetActive(true);
            trainBattleCamera.SetActive(true);
            trainingBattleText.text = currentStory.Continue();
            dialogueVariables.StartListening(currentStory);
            ContinueStory();
        }
        else {
            Debug.LogError("inkJSON is null!");
        }
    }

    private IEnumerator ExitDialogueMode() {

       yield return new WaitForSeconds(0.21f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private IEnumerator ExitTrainingMode() {

        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        trainingIsPlaying = false;
        trainingCanvas.SetActive(false);
        trainCamera.SetActive(false);

        trainActivation = false;
        trainingText.text = "";
    }

    private IEnumerator ExitTrainBattleMode() {

        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        trainBattleIsPlaying = false;
        trainBattlePanel.SetActive(false);
        trainBattleCamera.SetActive(false);

        // trainActivation = false;
        trainingBattleText.text = "";
    }

    public void TriggerTraining() {
        DialogueTrigger.GetInstance().EnterTraining();
     }

    public void TriggerBattleTraining() {
        DialogueTrigger.GetInstance().EnterBattleTraining();
     }

    private void ContinueStory() {
        if (currentStory == null) {
            Debug.LogError("currentStory is null. Cannot continue the story.");
        return;
        }

        if (dialogueIsPlaying) {
            if (currentStory.canContinue) {
                dialogueText.text = currentStory.Continue();
                DisplayChoices();
                List<string> tags = currentStory.currentTags;
                foreach (string tag in tags) {
                if (tag == "Trigger_training") {
                    StartCoroutine(ExitDialogueMode());
                    TriggerTraining();
                }
            }
            }
            else {
            StartCoroutine(ExitDialogueMode());
            }
        }
        else if (trainingIsPlaying) {
            if (currentStory.canContinue) {
                trainingText.text = currentStory.Continue();
                DisplayTrainChoices();
                List<string> tags = currentStory.currentTags;
                foreach (string tag in tags) {
                if (tag == "Trigger_trainBattle") {
                    StartCoroutine(ExitTrainingMode());
                    TriggerBattleTraining();
                    }
                }
            }
             else {
            StartCoroutine(ExitTrainingMode());
            }   
        } else if (trainBattleIsPlaying) {
            if (currentStory.canContinue) {
                trainingBattleText.text = currentStory.Continue();
                List<string> tags = currentStory.currentTags;
                foreach (string tag in tags) {
                if (tag == "Trigger_pickGravity") {
                    trainBattleActivation = true;
                    dialogueVariables.StopListening(currentStory);
                    StartCoroutine(bSystem.SetupEnemyBattle());
                    bSystem.SetupPlayerBattle();
                    }
                else if (tag == "Trigger_pickMeteor") {
                    trainBattleActivation = true;
                    dialogueVariables.StopListening(currentStory);
                    StartCoroutine(bSystem.ContinueEnemyBattle());
                    bSystem.ContinuePlayerBattle();
                }

                else if (tag == "Trigger_pickMega") {
                    trainBattleActivation = true;
                    dialogueVariables.StopListening(currentStory);
                    StartCoroutine(bSystem.ContinueEnemyBattle());
                    bSystem.SetupPlayerBattle();
                }

                else if (tag == "Trigger_pickKinetic") {
                    trainBattleActivation = true;
                    dialogueVariables.StopListening(currentStory);
                    StartCoroutine(bSystem.ContinueEnemyBattle());
                    bSystem.SetupPlayerBattle();
                }
                }
            }
        else {
            // dialogueVariables.StopListening(currentStory);
            StartCoroutine(ExitTrainBattleMode());
            }
       } else {
        Debug.LogWarning("Neither dialogueIsPlaying nor trainingIsPlaying is true.");
        }
    }


    private void DisplayTrainChoices() {
        if (currentStory == null) {
        Debug.LogError("currentStory is null. Cannot display choices.");
        return;
        }

        List<Choice> currentChoices = currentStory.currentChoices;


        if (currentChoices.Count > trainchoices.Length) {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;

        Debug.Log(index);

        foreach(Choice choice in currentChoices) {
            if (index >= trainchoices.Length) break;

            Debug.Log(choice.text);

            if (trainchoicesText[index].text == null) {
                Debug.LogError("Choice GameObject is null at index: " + index);
                continue;
            }

            trainchoices[index].gameObject.SetActive(true);
            trainchoicesText[index].text = choice.text;
            index++;
            }

        for (int i = index; i < trainchoices.Length; i++) {
            trainchoices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectTrainFirstChoice());

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

    private IEnumerator SelectTrainFirstChoice() {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(trainchoices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
    

