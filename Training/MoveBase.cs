using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Move/Create new move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] EnemyType type;
    [SerializeField] int power;

    [SerializeField] int accuracy;

    [SerializeField] int pp;

    [SerializeField] ActionType actype;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public EnemyType Type {
        get { return type; }
    }

    public int Power {
        get { return power; }
    }

    public int Accuracy {
        get { return accuracy; }
    }

    public int PP {
        get { return pp; }
    }

    public ActionType ActType {
        get { return actype; }
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

    public enum ActionType {
        Attack,
        StatusAtt,
        StatusDef,
        StatusSpe,
        Healing,
        GravityF,
        Train
    }
}
    
