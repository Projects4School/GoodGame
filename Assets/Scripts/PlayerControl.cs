using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public float MoveSmoothTime;
    public float Gravity = -9.81f;

    public AudioSource IngotAudio;

    private Vector3 CurrentVelocity;
    private CharacterController Controller;
    private Animator AnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        AnimatorController = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        GameController.instance.AddScore(1);
        if(!GameController.instance.IsWin())
        {
            IngotAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.IsFinished())
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
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, targetAngle, 0);

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
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            if(GameController.instance.IsWin())
            {
                AnimatorController.SetBool("Dancing", true);
            }
            else if(GameController.instance.IsGameOver())
            {
                AnimatorController.SetBool("Death", true);
            }
        }
    }
}
