using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new player")]

public class PlayerBase : ScriptableObject
{

    [SerializeField] int mass;
    [SerializeField] int maxHP;
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


    public void IncMass() {
        mass = mass * 2;
    }

    public void IncSpeed() {
        speed = speed * 2;
    }
    

    public int Speed {
        get { return speed; }
    }

    public void SetSpeed(int value) {
        speed = value;
    }

    public void SetMass(int value) {
        mass = value;
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

    

