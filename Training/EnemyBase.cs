using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new enemy")]
public class EnemyBase : ScriptableObject
{
    // private int hP;

    // public int HP {
    //     get { return hP; }
    //     set { hP = Mathf.Clamp(value, 0, MaxHP); } // Example of clamping HP between 0 and MaxHP
    // }

    // private void OnEnable() {
    //     HP = MaxHP;
    // }
    [SerializeField] string name;

    [TextArea]
    
    // [SerializeField] string description;

    // [SerializeField] Sprite frontSprite;

    [SerializeField] EnemyType type;

    [SerializeField] int maxHP;

    [SerializeField] int mass;

    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name {
        get { return name; }
    }

    // public string Description {
    //    get { return description; }
    // }

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

    public int MaxHP {
        get { return maxHP; }
    }

     public int Mass {
        get { return mass; }
    }

    public int Attack {
        get { return attack; }
    }

    public int Defence {
        get { return defence; }
    }

    public int Speed {
        get { return speed; }
    }
    public enum EnemyType {
        None,
        IceBody,
        GasGiant,
        HeatBody,
        Gravity,
        Galaxy
    }

    public void DecSpeed(int value) {
        defence += value;
    }
}

[System.Serializable]
public class LearnableMove 
{
    [SerializeField] MoveBase moveBase;

    public MoveBase Base {
        get { return moveBase; }
    }
}



