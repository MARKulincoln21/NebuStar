using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public List<Move> Moves { get; set; }
    public EnemyBase _base { get; set; }

    public int HP { get; set; }

    public Enemy(EnemyBase pBase) {
        _base = pBase;
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

    public bool TakePlayerDamage(Move move, Player attacker) {
        HP -= move.Base.Power;

        if ( HP <= 0 ) {
            HP = 0;
            return true;
        }
        return false;
    }

    public Move GetRandomMove() {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
