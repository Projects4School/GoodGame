using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public float MoveSmoothTime;
    public float Gravity = -9.81f;

    private Vector3 CurrentVelocity;
    private CharacterController Controller;
    private Animator AnimatorController;
    private MeshCollider Collider;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        AnimatorController = GetComponent<Animator>();
        Collider = GetComponent<MeshCollider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        Debug.Log(collider.gameObject.name);
        Debug.Log("triggered");
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
