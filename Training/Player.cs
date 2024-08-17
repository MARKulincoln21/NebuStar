using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<Move> Moves { get; set; }
    public PlayerBase _base { get; set; }

    public EnemyBase _base1 { get; set; }

    public int HP { get; set; }

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

     public int Attack {
        get { return Mathf.FloorToInt((_base.Attack)) + 5;}
    }

    public int Defence {
        get { return Mathf.FloorToInt((_base.Defence)) + 5;}
    }

    public int Speed {
        get { return Mathf.FloorToInt((_base.Speed)) + 5;}
    }

    public int MaxHP {
        get { return (_base.MaxHP) + 5;}
    }

    public int Mass {
        get { return (_base.Mass);}
    }

    public bool TakeEnemyDamage(Move move,  Enemy attacker) {
        HP -= move.Base.Power + (attacker.Attack / 2);

        if ( HP <= 0 ) {
            HP = 0;
            return true;
        }
        return false;
    }

    public void ActGravityField() {
        Debug.Log(_base1);
        if (_base.Mass < _base1.Mass){
            _base1.DecSpeed(10);
        }
    }
}
