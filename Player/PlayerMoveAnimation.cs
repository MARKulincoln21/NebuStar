using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnimation : MonoBehaviour
{
    private Animator anim;

    private static PlayerMoveAnimation instance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

    public static PlayerMoveAnimation GetInstance() {
        if (instance != null) {
        return instance;
        }
        else {
            Debug.LogError("instance is Null!");
            return null;
        }
    }
 
    public void PlayAnimationMove(string animation) {
        if (animation == ("Gravity Field (T)")|| animation == ("Gravity Field")) {
            anim.SetTrigger(animation);
        }

        else if (animation == ("Mega Mass (T)") || animation == ("Mega Mass")) {
            anim.SetTrigger(animation);
        }

        else if (animation == ("Super Kinetic (T)") || animation == ("Super Kinetic")) {
            anim.SetTrigger(animation);
        }

        else if (animation == ("Meteor Crash (T)") || animation == ("Meteor Crash")) {
            anim.SetTrigger(animation);
        }


    }
}
