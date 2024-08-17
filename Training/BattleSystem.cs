using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogueBox dialogueBox;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    BattleState state;
    int currentAction;
    int currentMove;
    int stateOfTraining;

    private DialogueVariables dialogueVariables;

    private void Start() {
        // StartCoroutine(SetupEnemyBattle());
        // SetupPlayerBattle();
    }

    public IEnumerator SetupEnemyBattle() {
        enemyUnit.SetupEnem();
        enemyHud.SetEnemyData(enemyUnit.Enemy);

        dialogueBox.SetDialogue($"Welcome");
        Debug.Log("welcome");
        yield return dialogueBox.TypeDialogue($"Choose your moves.");
        Debug.Log("welcome");
        yield return new WaitForSeconds(1);

        PlayerAction();
    }

    public IEnumerator ContinueEnemyBattle() {

        dialogueBox.SetDialogue($"Welcome");
        Debug.Log("welcome");
        yield return dialogueBox.TypeDialogue($"Choose your moves.");
        Debug.Log("welcome");
        yield return new WaitForSeconds(1);

        PlayerAction();
    }

    public void SetupPlayerBattle() {
        playerUnit.SetupPlayer();
        playerHud.SetPlayerData(playerUnit.Player);
        dialogueBox.SetMoveNames(playerUnit.Player.Moves);
    }

    public void ContinuePlayerBattle() {
        dialogueBox.SetMoveNames(playerUnit.Player.Moves);
    }

    void PlayerAction() {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelector(true);
    }

    void PlayerMove() {
        state = BattleState.PlayerMove;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove() {
        state = BattleState.Busy;
        var move = playerUnit.Player.Moves[currentMove];
        yield return dialogueBox.TypeDialogue($" Hero used {move.Base.Name}");
        yield return new WaitForSeconds(1f);
        if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 0) {
            // playerUnit.Player.ActGravityField();
            // Debug.Log(enemyUnit.Enemy._base.Speed);
             bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
             enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
            // StartCoroutine(EnemyMove());
        }

        else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 1) {
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
            // StartCoroutine(EnemyMove());
        }

        else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 2) {
            // playerUnit.Player.ActGravityField();
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
            // StartCoroutine(EnemyMove());
        }

         else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 3) {
            // playerUnit.Player.ActGravityField();
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
            // StartCoroutine(EnemyMove());
        }

       

        else {
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
            StartCoroutine(EnemyMove());
        }
        }



    

    IEnumerator EnemyMove() {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Enemy.GetRandomMove();
        // var move = playerUnit.Player.Moves[currentMove];
        yield return dialogueBox.TypeDialogue($" Mysterious Man used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

        playerHud.UpdatePlayerHP();

        if (isFainted) {
            yield return dialogueBox.TypeDialogue($"Hero was defeated.");
        }

        else {
            PlayerAction();
        }
    }

    private void Update() {
        if (state == BattleState.PlayerAction) {
            HandleActionSelection();
        }

        else if (state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentAction < 1) {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentAction > 0) {
                --currentAction;
            }
        }

        dialogueBox.UpdateActionSelection(currentAction);

        if (InputManager.GetInstance().GetSubmitPressed()) {
            if (currentAction == 0) {
                PlayerMove();               
            }

            else if (currentAction == 1) {
                PlayerMove();
            }
        }
    }

    void HandleMoveSelection() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (currentMove < playerUnit.Player.Moves.Count - 1) {
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (currentMove > 0) {
                --currentMove;
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentMove < playerUnit.Player.Moves.Count - 2) {
                currentMove += 2;
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentMove > 1) {
                currentMove -= 2;
            }
        }

        dialogueBox.UpdateMoveSelection(currentMove, playerUnit.Player.Moves[currentMove]);

        if (InputManager.GetInstance().GetSubmitPressed()) {
            if (currentMove == 0 && stateOfTraining == 0) {
                dialogueBox.EnableMoveSelector(false);
                dialogueBox.EnableDialogueText(true);
                StartCoroutine(PerformPlayerMove());
            }

            else if (currentMove == 1 && stateOfTraining == 1) {
                dialogueBox.EnableMoveSelector(false);
                dialogueBox.EnableDialogueText(true);
                StartCoroutine(PerformPlayerMove());
            }

            else if (currentMove == 2 && stateOfTraining == 2) {
                dialogueBox.EnableMoveSelector(false);
                dialogueBox.EnableDialogueText(true);
                StartCoroutine(PerformPlayerMove());
            }

            else if (currentMove == 3 && stateOfTraining == 3) {
                dialogueBox.EnableMoveSelector(false);
                dialogueBox.EnableDialogueText(true);
                StartCoroutine(PerformPlayerMove());
            }
        }
        
    }

    public enum EnemyType {
        None,
        IceBody,
        GasGiant,
        HeatBody,
        Gravity,
        Plasma,
        RockBody
    }
}
