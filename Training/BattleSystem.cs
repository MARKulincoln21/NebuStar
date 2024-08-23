using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    int numbOfEnemTurn;

    int numbOfPlayTurn;

    int roundOfStun;

    int roundOfRockDMG;
    bool semiGravityStun;
    bool gravityStun;

    bool playerGravityStun;

    int playerRoundOfStun;

    bool ringRockyDamage;

    private DialogueVariables dialogueVariables;

    MoveMechanics moveMech; 

    private void Start() {
        numbOfEnemTurn = 0;
        numbOfPlayTurn = 0;

        moveMech = new MoveMechanics();
        roundOfStun = 0;
        roundOfRockDMG = 0;

        playerRoundOfStun = 0;
    }

    public IEnumerator SetupEnemyBattle() {
        enemyUnit.SetupEnem();
        enemyHud.SetEnemyData(enemyUnit.Enemy);

        dialogueBox.SetDialogue($"Welcome");
        Debug.Log("welcome");
        yield return dialogueBox.TypeDialogue($"You are in battle!");
        Debug.Log("welcome");
        yield return new WaitForSeconds(1);

        if (enemyUnit.Enemy._base.Speed > playerUnit.Player._base.Speed) {
            EnemyMove();
        }

        PlayerAction();
    }

    public IEnumerator ContinueEnemyBattle() {
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
        // else if (semiGravityStun == true) {
        //    int chance = UnityEngine.Random.Range(0, 2);
        
        //    if (chance == 0 || chance == 1)
        //    {
        //        StartCoroutine(EnemyMove());
        //        roundOfStun--;
        //        if (roundOfStun == 0) {
        //            semiGravityStun = false;
        //        }
        //    }
        // }

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
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name);
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
        }

        else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 1) {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name);
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
        }

        else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 2) {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name);
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
        }

         else if (move.Base.ActType == MoveBase.ActionType.Train && stateOfTraining == 3) {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name);
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);

            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Mysterious Man was defeated.");
            }
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            DialogueManager.GetInstance().Update();
            stateOfTraining++;
        }

       else if (move.Base.Name == "Mega Mass") {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name + " (T)");
            
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
            
            moveMech.MegaMassFunction(playerUnit);
            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Maximus was defeated.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Well done! you have completed the game.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Thank you for playing");
                yield return new WaitForSeconds(1f);
                dialogueBox.EnableActionSelector(false);
                DialogueManager.GetInstance().trainBattleActivation = false;
                SceneManager.LoadSceneAsync("Main Menu");
            }

            if (playerGravityStun == true) {
                playerRoundOfStun--;
                PlayerAction();
            } 

            else {
            dialogueBox.EnableActionSelector(false);
            
            DialogueManager.GetInstance().Update();
            
            StartCoroutine(EnemyMove());
            }
        }

        else if (move.Base.Name == "Super Kinetic") {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name + " (T)");
            
            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
            
            moveMech.SuperKineticFunction(playerUnit);
            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Maximus was defeated.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Well done! you have completed the game.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Thank you for playing");
                yield return new WaitForSeconds(1f);
                dialogueBox.EnableActionSelector(false);
                DialogueManager.GetInstance().trainBattleActivation = false;
                SceneManager.LoadSceneAsync("Main Menu");
            }
            
            if (playerGravityStun == true) {
                playerRoundOfStun--;
                PlayerAction();
            } 

            else {
            dialogueBox.EnableActionSelector(false);
            
            DialogueManager.GetInstance().Update();
            
            StartCoroutine(EnemyMove());
            }
        }

        else {
            PlayerMoveAnimation.GetInstance().PlayAnimationMove(move.Base.Name + " (T)");
            if (move.Base.Name == "Gravity Field") {
                if (moveMech.GravityFieldPlayStunFunction(playerUnit, enemyUnit) == true) {
                    playerGravityStun = true;
                    playerRoundOfStun = 5;
                    yield return dialogueBox.TypeDialogue($" Maximus has been stunned! ");
                    yield return new WaitForSeconds(1f);
                }
            }

            bool isFainted = enemyUnit.Enemy.TakePlayerDamage(move, playerUnit.Player);
            
            moveMech.DamageAttackFunction(playerUnit, enemyUnit, move);

            if (ringRockyDamage == true) {
                yield return dialogueBox.TypeDialogue($" The ring of rocks inflict damage ");
                yield return new WaitForSeconds(1f);
                var moveProcess = enemyUnit.Enemy.MoveChooser("Ring of Rocky Destruction");
                moveMech.RingRockFunction(enemyUnit, playerUnit, moveProcess);
                playerHud.UpdatePlayerHP();
                roundOfRockDMG--; 
                if (roundOfRockDMG == 0) {
                    ringRockyDamage = false;
                }
            }
            enemyHud.UpdateEnemyHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Maximus was defeated.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Well done! you have completed the game.");
                yield return new WaitForSeconds(1f);
                yield return dialogueBox.TypeDialogue($"Thank you for playing");
                yield return new WaitForSeconds(1f);
                dialogueBox.EnableActionSelector(false);
                DialogueManager.GetInstance().trainBattleActivation = false;
                SceneManager.LoadSceneAsync("Main Menu");
            }

            if (playerGravityStun == true) {
                playerRoundOfStun--;
                PlayerAction();
            } 

            else {
            dialogueBox.EnableActionSelector(false);
            
            DialogueManager.GetInstance().Update();
            
            StartCoroutine(EnemyMove());
            }
            
        }
    } 

    IEnumerator EnemyMove() {
        state = BattleState.EnemyMove;
        if (enemyUnit.Enemy.HP < (enemyUnit.Enemy.MaxHP - ((enemyUnit.Enemy.MaxHP / 10) * 9))) {
            var move = enemyUnit.Enemy.MoveChooser("Hypothermic Rejuvination");
            yield return dialogueBox.TypeDialogue($" {enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);
            moveMech.HypothermicFunction(enemyUnit);
            enemyHud.UpdateEnemyHP();

            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            yield return new WaitForSeconds(1f);
            yield return dialogueBox.TypeDialogue($"Sorry, you lost. Try the fight again.");
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            SceneManager.LoadSceneAsync("Stage 2");
            }

            else if (gravityStun == true) {
                StartCoroutine(EnemyMove());
                roundOfStun--; 
                if (roundOfStun == 0) {
                    gravityStun = false;
                }
            }
            else { 
                PlayerAction();
            }
         }

        if (numbOfEnemTurn == 0) {
            numbOfEnemTurn++;
            var move = enemyUnit.Enemy.MoveChooser("Gravity Field");
            yield return dialogueBox.TypeDialogue($" {enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);
            if (moveMech.GravityFieldEnemStunFunction(enemyUnit, playerUnit) == true && gravityStun == false) {
                roundOfStun = 5;
                gravityStun = true;
                yield return dialogueBox.TypeDialogue($" Hero has been stunned! ");
                yield return new WaitForSeconds(1f);
                Debug.Log(gravityStun);
            }
            
            else if (moveMech.GravityFieldSemiEnemStunFunction(enemyUnit, playerUnit) == true && semiGravityStun == false) {
                roundOfStun = 3;
                semiGravityStun = true;
            }

            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            yield return new WaitForSeconds(1f);
            yield return dialogueBox.TypeDialogue($"Sorry, you lost. Try the fight again.");
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            SceneManager.LoadSceneAsync("Stage 2");
            }

            else if (gravityStun == true) {
                StartCoroutine(EnemyMove());
                roundOfStun--; 
                if (roundOfStun == 0) {
                    gravityStun = false;
                }
            }
            else { 
                PlayerAction();
            }
        }

        else if (enemyUnit.Enemy.HP < ((enemyUnit.Enemy.MaxHP / 10) * 5)) {
            if (ringRockyDamage == true) {
            var move = enemyUnit.Enemy.GetRandomNotGravityNotRockMove();
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);


            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

             if (isFainted) {
            yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            yield return new WaitForSeconds(1f);
            yield return dialogueBox.TypeDialogue($"Sorry, you lost. Try the fight again.");
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            SceneManager.LoadSceneAsync("Stage 2");
            }

            else if (gravityStun == true) {
            StartCoroutine(EnemyMove());
            roundOfStun--; 
            if (roundOfStun == 0) {
                gravityStun = false;
            }
            }

            else { 
                PlayerAction();
            }
        }
            else if (ringRockyDamage == false) {
            var move = enemyUnit.Enemy.MoveChooser("Ring of Rocky Destruction");
            yield return dialogueBox.TypeDialogue($" {enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(3f);
            ringRockyDamage = true;
            roundOfRockDMG = 5;

            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            yield return new WaitForSeconds(1f);
            yield return dialogueBox.TypeDialogue($"Sorry, you lost. Try the fight again.");
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            SceneManager.LoadSceneAsync("Stage 2");
            }

            else if (gravityStun == true) {
                StartCoroutine(EnemyMove());
                roundOfStun--; 
                if (roundOfStun == 0) {
                    gravityStun = false;
                }
            }

            else { 
                PlayerAction();
            }
         }
        }

        else {
        if (ringRockyDamage == true) {
            var move = enemyUnit.Enemy.GetRandomNotGravityNotRockMove();
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);

            if (move.Base.Name == "Hypothermic Rejuvination") {
            yield return dialogueBox.TypeDialogue($" {enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);
            moveMech.HypothermicFunction(enemyUnit);
            enemyHud.UpdateEnemyHP();
            }

            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

             if (isFainted) {
            yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            yield return new WaitForSeconds(1f);
            yield return dialogueBox.TypeDialogue($"Sorry, you lost. Try the fight again.");
            dialogueBox.EnableActionSelector(false);
            DialogueManager.GetInstance().trainBattleActivation = false;
            SceneManager.LoadSceneAsync("Stage 2");

            }

            else if (gravityStun == true) {
            StartCoroutine(EnemyMove());
            roundOfStun--; 
            if (roundOfStun == 0) {
                gravityStun = false;
            }
            }

            else { 
                PlayerAction();
            }
        }

        else if (ringRockyDamage == false) {
            var move = enemyUnit.Enemy.GetRandomNotGravityMove();
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);
            if (move.Base.Name == "Ring of Rocky Destruction") {
                ringRockyDamage = true;
                roundOfRockDMG = 5;
            }

            if (move.Base.Name == "Hypothermic Rejuvination") {
            yield return dialogueBox.TypeDialogue($" {enemyUnit.Enemy._base.Name} used {move.Base.Name}");
            yield return new WaitForSeconds(1f);
            moveMech.HypothermicFunction(enemyUnit);
            enemyHud.UpdateEnemyHP();
            }

            bool isFainted = playerUnit.Player.TakeEnemyDamage(move, enemyUnit.Enemy);

            playerHud.UpdatePlayerHP();

            if (isFainted) {
                yield return dialogueBox.TypeDialogue($"Hero was defeated.");
            }

            else if (gravityStun == true) {
                StartCoroutine(EnemyMove());
                roundOfStun--; 
                if (roundOfStun == 0) {
                    gravityStun = false;
            }   
            }

        else { 
            PlayerAction();
            }
        }
        }
    }

    private void Update() {
        if (state == BattleState.PlayerAction) {
            HandleActionSelection();
        }

        else if (state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
        
        if (roundOfStun == 0) {
            gravityStun = false;
            semiGravityStun = false;
        }

        if (roundOfRockDMG == 0) {
            ringRockyDamage = false;
        }

        if (playerRoundOfStun == 0) {
            playerGravityStun = false;
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

        if (enemyUnit.Enemy._base.Name == "Mysterious Man") {
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
        else {
            if (InputManager.GetInstance().GetSubmitPressed()) {
                if (currentMove == 0) {
                    dialogueBox.EnableMoveSelector(false);
                    dialogueBox.EnableDialogueText(true);
                    StartCoroutine(PerformPlayerMove());
                }

                else if (currentMove == 1) {
                    dialogueBox.EnableMoveSelector(false);
                    dialogueBox.EnableDialogueText(true);
                    StartCoroutine(PerformPlayerMove());
                }

                else if (currentMove == 2) {
                    dialogueBox.EnableMoveSelector(false);
                    dialogueBox.EnableDialogueText(true);
                    StartCoroutine(PerformPlayerMove());
                }

                else if (currentMove == 3) {
                    dialogueBox.EnableMoveSelector(false);
                    dialogueBox.EnableDialogueText(true);
                    StartCoroutine(PerformPlayerMove());
                }
            }
        }   
        
    }
}
