using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movespeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XmovementIn = Input.GetAxis("Horizontal");
        
        float YmovementIn = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(XmovementIn, YmovementIn, 0);

        transform.Translate(direction * movespeed * Time.deltaTime);
        
    }
}
