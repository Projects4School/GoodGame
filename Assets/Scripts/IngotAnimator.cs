using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotAnimator : MonoBehaviour
{
    public float bounceHeight = 1.0f; // Hauteur du rebond
    public float bounceSpeed = 2.0f; // Vitesse du rebond

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = originalPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
        transform.Rotate(new Vector3(0f, 0f, 250f) * Time.deltaTime);
    }
}
