using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.UI;



public class BattleDialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;

    [SerializeField] TextMeshProUGUI ppTexts;
    [SerializeField] TextMeshProUGUI typeTexts;

    


    public void SetDialogue(string dialogue) {
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue) {
        dialogueText.text = "";

        foreach (var letter in dialogue.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }

    public void EnableDialogueText(bool enabled) {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled) {
        actionSelector.SetActive(enabled);
    }

    public void DisableActionSelector(bool disable) {
        actionSelector.SetActive(disable);
    }

    public void EnableMoveSelector(bool enabled) {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction) {
        for ( int i = 0; i<actionTexts.Count; ++i) {
            if (i == selectedAction) {
                actionTexts[i].color = highlightedColor;
            }
            else {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move) {
        for ( int i = 0; i<moveTexts.Count; ++i) {
            if (i == selectedMove) {
                moveTexts[i].color = highlightedColor;
            }
            else {
                moveTexts[i].color = Color.black;
            }
        }
        
        ppTexts.text = $"pp {move.PP}/{move.Base.PP}";
        typeTexts.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Move> moves) {
        for (int i = 0; i<moveTexts.Count; ++i) {
            if (i < moves.Count) {
                moveTexts[i].text = moves[i].Base.Name;
            }else {
                moveTexts[i].text = "-";
            }
            }
        }
    }



