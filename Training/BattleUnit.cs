using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private static BattleUnit instance;

    
 
    [SerializeField] EnemyBase _base;
    [SerializeField] PlayerBase _base2;

    public Enemy Enemy { get; set; }
    public Player Player { get; set; }

    public void SetupEnem() {
        Enemy = new Enemy(_base);
       // Player = new Player(_base2);
    }

    public void SetupPlayer() {
        Player = new Player(_base2, _base);
    }
}
