using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public float FlySpeed = 5;
    public float TurnSpeed = 120;
    private Rigidbody thisRigidbody = null;

    private float Turn;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FlyMovement();
        thisRigidbody.velocity = Vector3.zero;
    }

    void FlyMovement()
    {
        //press Space to accelerate (move forward)
        if (Input.GetKey(KeyCode.Space))
            transform.position += transform.forward * FlySpeed * Time.deltaTime;

        //W S
        float horizontalInput = Input.GetAxis("Horizontal");
        //A D
        float verticalInput = Input.GetAxis("Vertical");

        //turn
        Turn += horizontalInput * TurnSpeed * Time.deltaTime;
        //rotate vertical
        float pitch = Mathf.Lerp(this.transform.localRotation.x, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        //rotate horizontal
        float roll = Mathf.Lerp(this.transform.localRotation.y, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //apply rotation
        transform.localRotation = Quaternion.Euler(Vector3.up * Turn + Vector3.right * pitch + Vector3.forward * roll);
        
        //transform.localRotation = Quaternion.Euler(Vector3.up * Turn + Vector3.right * (pitch + this.transform.localRotation.x) + Vector3.forward * (roll + this.transform.localRotation.y));
    }
}