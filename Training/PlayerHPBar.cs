using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
   [SerializeField] GameObject playerHealth;

   public void SetHP(float hpNormalized) {
      playerHealth.transform.localScale = new Vector2(hpNormalized, 0.3235785f);
   }
}
