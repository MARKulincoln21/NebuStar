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

            if (Moves.Count > 6) {
                break;
            }
        }
    }

    public Move MoveChooser(string nameOfMove) {
        for (int i = 0; i < Moves.Count; i++) {
            if (Moves[i].Base.Name == nameOfMove) {
                return Moves[i];
            }
        }
        return Moves[0];
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

    public Move GetRandomMove() {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

    public bool TakePlayerDamage(Move move, Player attacker) {
        HP -= move.Base.Power;

        if ( HP <= 0 ) {
            HP = 0;
            return true;
        }
        return false;
    }

    public Move GetRandomNotGravityMove() {
        int r = Random.Range(0, Moves.Count);
        if (Moves[r].Base.Name == "Gravity Field") {
            r++;
        }
        return Moves[r];
    }

    public Move GetRandomNotGravityNotRockMove() {
        int r = Random.Range(0, Moves.Count);
        if (Moves[r].Base.Name == "Gravity Field" || Moves[r].Base.Name == "Ring of Rocky Destruction") {
            r++;
        }
        return Moves[r];
    }
}
