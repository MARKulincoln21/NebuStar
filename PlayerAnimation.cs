using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public string[] staticD = {"Static N", "Static W", "Static S", "Static E"};
    public string[] run = {"Run N", "Run W", "Run S", "Run E"};

    int lastDirection;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetDirection(Vector2 _direction) {

        string[] directionArray = null;

        if(_direction.magnitude < 0.01) {
            directionArray = staticD;
        }
        else {
            directionArray = run;

            lastDirection = DirectionToIndex(_direction);
        }

        anim.Play(directionArray[lastDirection]);
    }

    public int DirectionToIndex(Vector2 _direction) {

        Vector2 norDir = _direction.normalized;

        float step = 360 / 4;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;

        if(angle < 0) {
            angle+=360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
