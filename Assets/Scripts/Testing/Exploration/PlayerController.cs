using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the player controller in the exploration phase
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask mask;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Get exact location of click
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            if(Physics.Raycast(ray, out RaycastHit hitInfo,100f,mask))
            {
                Vector3 place = hitInfo.point;
                place.y = rb.position.y;

                target.position = place;

                Vector3 direction = place - rb.position;

                if(direction.magnitude > 0.5f)
                {
                    rb.linearVelocity = direction.normalized * playerSpeed;
                }
            }
        }
        else
        {
            target.position = new Vector3(0,-50,0);
        }
    }
}
