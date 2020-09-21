using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1f;
    // Update is called once per frame
    void Update()
    {

        float xAxis = Input.GetAxis("Horizontal");

        float xMovement = Speed * xAxis * Time.deltaTime;
        Vector3 movement = Vector3.right * xMovement;
        transform.position += movement;

    }
}
