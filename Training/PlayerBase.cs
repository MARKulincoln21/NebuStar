using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new player")]

public class PlayerBase : ScriptableObject
{

    [SerializeField] int mass;
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

    MoveBase moveBase_;
    public MoveBase Base_ {
        get { return moveBase_; }
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

    public void SetDefence(int value) {
        defence = value;
    }

    public void IncDefence(int value) {
        defence += value;
    }

    public int Speed {
        get { return speed; }
    }

    public void DecSpeed(int value) {
        defence += value;
    }


    [System.Serializable]
    public class LearnableMove 
    {
    [SerializeField] MoveBase moveBase;

    public MoveBase Base {
        get { return moveBase; }
    }
    }
    }

    

