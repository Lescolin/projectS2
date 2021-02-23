using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -40f;           // Réalisme = -9.81, mais besoin dynamique du jeu = -40
    public float jump = 3f;
    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public float isfloatingvelocity = -4f; // Doit toujours être strictement inférieur à -2, sinon on décolle pas
    public int multiplejump = 2;           // Pour double jump, mettre à 2 sauf pour test
    public LayerMask groundmask;
    bool isgrounded;
    bool isfloating = false;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        isfloating = Input.GetButton("Jump") && !isgrounded && velocity.y <= isfloatingvelocity;

        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            multiplejump = 2;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && (isgrounded || multiplejump > 0))
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            multiplejump -= 1;
        }

        if (isfloating && multiplejump == 0)
        {
            velocity.y = isfloatingvelocity;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
