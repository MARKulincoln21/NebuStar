using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMechanics : MonoBehaviour
{

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool GravityFieldPlayStunFunction(BattleUnit user, BattleUnit target) {
        if ((user.Player._base.Mass - target.Enemy._base.Mass) >= 100) {
            return true;
        }
        return false;
    }

    public bool GravityFieldSemiPlayStunFunction(BattleUnit user, BattleUnit target) {
        if (((user.Player._base.Mass - target.Enemy._base.Mass) > 50) && (user.Player._base.Mass - target.Enemy._base.Mass) < 100) {
            return true;
        }
        return false;
    }

    public bool GravityFieldEnemStunFunction(BattleUnit user, BattleUnit target) {
        if ((user.Enemy._base.Mass - target.Player._base.Mass) >= 100) {
            return true;
        }
        return false;
    }

    public bool GravityFieldSemiEnemStunFunction(BattleUnit user, BattleUnit target) {
        if (((user.Enemy._base.Mass - target.Player._base.Mass) > 50) && (user.Enemy._base.Mass - target.Player._base.Mass) < 100) {
            return true;
        }
        return false;
    }

    public void DamageAttackFunction(BattleUnit user, BattleUnit target, Move move) {
    if (move.Base.Power > 0) {
        int power = ((user.Player._base.Mass * user.Player._base.Speed) / 1000) + move.Base.Power;

        target.Enemy.HP = target.Enemy.HP - power; 

        if ( target.Enemy.HP <= 0 ) {
            target.Enemy.HP = 0;
        }
    }

    else {
        target.Enemy.HP = target.Enemy.HP - 0; 

        if ( target.Enemy.HP <= 0 ) {
            target.Enemy.HP = 0;
        }
    }
    }

    public void MegaMassFunction(BattleUnit user) {
        user.Player._base.IncMass();
    }

    public void SuperKineticFunction(BattleUnit user) {
        user.Player._base.IncSpeed();
    }

    public void RingRockFunction(BattleUnit user, BattleUnit target, Move move) {
        int power = move.Base.Power + ((user.Enemy._base.Mass * user.Enemy._base.Speed) / 1000);

        target.Player.HP = target.Player.HP - power; 

        if ( target.Player.HP <= 0 ) {
            target.Player.HP = 0;
        }
    }

    public void GlacialCataclFunction(BattleUnit user, BattleUnit target, Move move) {
        int power = move.Base.Power;

        target.Player.HP = target.Player.HP - power; 

        if ( target.Player.HP <= 0 ) {
            target.Player.HP = 0;
        }
    }

    public void HypothermicFunction(BattleUnit user) {
        user.Enemy.HP = user.Enemy.HP + 1000;
        if (user.Enemy.HP > user.Enemy.MaxHP) {
            user.Enemy.HP = user.Enemy.MaxHP;
        }
        
    }





}
