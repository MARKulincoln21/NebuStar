using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movespeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XmovementIn = Input.GetAxis("Horizontal") * movespeed;
        
        float YmovementIn = Input.GetAxis("Vertical") * movespeed;

        Vector3 direction = new Vector3(XmovementIn, YmovementIn, 0);

        FindObjectOfType<PlayerAnimation>().SetDirection(direction);

        transform.Translate(direction * Time.deltaTime);
        
    }
}
