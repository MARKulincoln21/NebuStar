using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
   [SerializeField] GameObject enemyHealth;

   public void SetHP(float hpNormalized) {
      enemyHealth.transform.localScale = new Vector2(hpNormalized, 0.3235785f);
   }
}