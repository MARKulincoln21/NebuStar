using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public string[] staticD = {"Static N", "Static W", "Static S", "Static E"};
    public string[] run = {"Run N", "Run W", "Run S", "Run E"};

    public string[] walk = {"Walk N", "Walk W", "Walk S", "Walk E"};

    string[] directionArray = null;

    int lastDirection;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        directionArray = staticD;
        anim.Play(directionArray[2]);
    }


    // Update is called once per frame
    public void SetDirection(Vector2 _direction) {

        if (_direction.magnitude < 0.2f) {
            directionArray = staticD;

        }

        else if (_direction.magnitude >= 0.2f  && _direction.magnitude < 3.0f) {
            directionArray = walk;

            lastDirection = DirectionToIndex(_direction);
            Debug.Log(lastDirection);
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
