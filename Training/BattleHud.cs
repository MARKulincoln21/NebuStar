using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI nameText;
   [SerializeField] EnemyHPBar enemhpBar;
   [SerializeField] PlayerHPBar playerhpBar;

   Enemy _enemy;
   Player _player;

   public void SetEnemyData(Enemy enemy) {
    _enemy = enemy;
    nameText.text = enemy._base.Name;
    enemhpBar.SetHP((float) enemy.HP / enemy.MaxHP);
   }

   public void SetPlayerData(Player player) {
    _player = player;
    playerhpBar.SetHP((float) player.HP / player.MaxHP);
   }

   public void UpdateEnemyHP() {
      enemhpBar.SetHP((float) _enemy.HP / _enemy.MaxHP);

   }

   public void UpdatePlayerHP() {
      playerhpBar.SetHP((float) _player.HP / _player.MaxHP);

   }
}
