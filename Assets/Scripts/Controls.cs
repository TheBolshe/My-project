using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour {
    Rigidbody rb;
    Vector2 movDir;
    Vector2 aimDir;
    [SerializeField]
    float speed = 5f;

    void Awake() {
        
    }

    void Start() {
        this.rb = this.GetComponent<Rigidbody>();
    }

    public void OnMovement(InputValue movementInput) {
        this.movDir = movementInput.Get<Vector2>();
    }

    public void OnAim(InputValue aimInput) {
        this.aimDir = aimInput.Get<Vector2>().normalized;
    }

    void OnTeleport() {
        Vector3 start = this.rb.position;
        Vector3 target = this.rb.transform.forward * 5 + start;
        Vector3 finish = new Vector3(target.x, 2, target.z);
        Debug.Log("target : " + finish);
        this.rb.transform.position = target;
        Debug.Log("position : " + this.rb.position);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 playerMovement = new Vector3(movDir.x, 0, movDir.y);
        this.rb.MovePosition(transform.position + playerMovement * Time.deltaTime * speed);

        Vector3 playerAim = new Vector3(aimDir.x, 0, aimDir.y);
        if (playerAim.magnitude > 0) {
            Quaternion rotation = Quaternion.LookRotation(playerAim);
            rotation = Quaternion.RotateTowards(rb.rotation, rotation, 480 * Time.deltaTime);
            this.rb.MoveRotation(rotation); 
        } 
        if (playerMovement.magnitude > 0) {
            Quaternion rotation = Quaternion.LookRotation(playerMovement);
            rotation = Quaternion.RotateTowards(rb.rotation, rotation, 480 * Time.deltaTime);
            this.rb.MoveRotation(rotation);
        }
    }
}
