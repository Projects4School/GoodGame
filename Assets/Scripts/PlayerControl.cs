using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public float MoveSmoothTime;
    public float Gravity = -9.81f;

    private bool isWalking;
    private Vector3 CurrentVelocity;
    private Vector3 MoveDampVelocity;
    private CharacterController Controller;
    private Animator AnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        Controller = GetComponent<CharacterController>();
        AnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 PlayerInput = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            y = 0f,
            z = Input.GetAxis("Vertical")
        };

        if(PlayerInput.magnitude > 1f)
        {
            PlayerInput.Normalize();
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerInput);

        CurrentVelocity = Vector3.SmoothDamp(CurrentVelocity, MoveVector * Speed, ref MoveDampVelocity, MoveSmoothTime);

        if(CurrentVelocity.magnitude > 0f)
        {
            AnimatorController.SetBool("Walking", true);
        }
        else
        {
            AnimatorController.SetBool("Walking", false);
        }
        
        Controller.Move(CurrentVelocity * Time.deltaTime);*/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Déterminer la direction de déplacement
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        direction = direction.normalized;

        float currentSpeed = direction.magnitude * Speed;

        AnimatorController.SetFloat("Speed", currentSpeed);

        if (direction.magnitude >= 0.1f)
        {
            // Calculer l'angle cible en fonction de la direction de déplacement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Tourner le joueur vers l'angle cible
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Déplacer le joueur dans la direction
            Controller.Move(direction * Speed * Time.deltaTime);
        }

        if (Controller.isGrounded)
        {
            CurrentVelocity.y = 0f;
        }
        else
        {
            CurrentVelocity.y += Gravity * Time.deltaTime;
        }

        Controller.Move(CurrentVelocity);

        /*if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if(isWalking)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }*/
    }
}
