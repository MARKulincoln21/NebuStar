using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public List<Move> Moves { get; set; }
    public PlayerBase _base { get; set; }

    public EnemyBase _base1 { get; set; }

    public int HP { get; set; }

    private void Awake() {
        _base.SetSpeed(90);
        _base.SetMass(150);
    }

    public Player(PlayerBase pBase, EnemyBase qBase) {
        _base = pBase;
        _base1 = qBase;
        HP = MaxHP;

        Moves = new List<Move>();
        foreach ( var move in _base.LearnableMoves) {
            Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4) {
                break;
            }
        }
    }


    public int Speed {
        get { return (_base.Speed) + 5;}
    }


    public int MaxHP {
        get { return (_base.MaxHP) + 5;}
    }

    public int Mass {
        get { return (_base.Mass);}
    }

    public bool TakeEnemyDamage(Move move, Enemy attacker) {
        HP -= move.Base.Power;

        if ( HP <= 0 ) {
            HP = 0;
            return true;
        }
        return false;
    }
}
