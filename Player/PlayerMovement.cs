using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movespeed = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }

        
        else if (Input.GetKey(KeyCode.K)) {
            movespeed = 1.25f;
        }

        else if (Input.GetKey(KeyCode.L)) {
            movespeed = 3.0f;
        }


        float XmovementIn = Input.GetAxis("Horizontal") * movespeed;
        
        float YmovementIn = Input.GetAxis("Vertical") * movespeed;

        Vector3 direction = new Vector3(XmovementIn, YmovementIn, 0);


        FindObjectOfType<PlayerAnimation>().SetDirection(direction);

        transform.Translate(direction * Time.deltaTime);
        
    }
}
