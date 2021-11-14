using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject leftThruster;
    public GameObject rightThruster;

    float leftThrusterSpin = 0;
    float leftThrusterRotation = 0;
    float rightThrusterSpin = 0;
    float rightThrusterRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = this.gameObject.AddComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetWidth(0.1f, 0.1f);
        // we want the lines to use local space and not world space
        line.useWorldSpace = false;
        line.useLightProbes = false;
        line.receiveShadows = false;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.leftThrusterSpin -= 0.1f;
            this.rightThrusterSpin -= 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.leftThrusterSpin += 0.1f;
            this.rightThrusterSpin += 0.1f;
        }

        this.leftThrusterRotation += this.leftThrusterSpin;
        this.rightThrusterRotation += this.rightThrusterSpin;

        this.leftThrusterSpin *= 0.95f;
        this.leftThrusterRotation *= 0.94f;
        this.rightThrusterSpin *= 0.95f;
        this.rightThrusterRotation *= 0.94f;

        leftThruster.transform.localEulerAngles = new Vector3(0, leftThrusterRotation, 0);
        rightThruster.transform.localEulerAngles = new Vector3(0, rightThrusterRotation, 0);

        var sideOffset = this.transform.right * 0.445316f;
        var forwardOffset = this.transform.forward * 0.00629252f;
        var topOffset = this.transform.up * 0.002624031f;

        var rb = GetComponent<Rigidbody>();
        //rb.AddRelativeTorque(1, 0, 0);
        //rb.AddRelativeTorque(-1, 0, 0);
        rb.AddForceAtPosition(leftThruster.transform.up, this.transform.position + forwardOffset - sideOffset + topOffset, ForceMode.Force); // Left thruster
        rb.AddForceAtPosition(rightThruster.transform.up, this.transform.position + forwardOffset + sideOffset + topOffset, ForceMode.Force); // Right thruster

        //rb.AddForceAtPosition(this.transform.up * -1, new Vector3(-1, 0, 0), ForceMode.Acceleration);
        //rb.AddForceAtPosition(this.transform.up * -1, new Vector3(0, -1, 0), ForceMode.Acceleration);
        //rb.AddForceAtPosition(this.transform.up * -1, new Vector3(0, 1, 0), ForceMode.Acceleration);
        //rb.AddForce()
    }

    void FixedUpdate()
    {
        // begin the line at the cubes position
        Vector3 Start = new Vector3(0.00629252f, 0.445316f, 0.002624031f);
        // to that the line ends at the center of the ball
        Vector3 End = new Vector3(0, 10, 0);
        //Set the begin and the end of the line renderer
        this.gameObject.GetComponent<LineRenderer>().SetPosition(0, Start);
        this.gameObject.GetComponent<LineRenderer>().SetPosition(1, End);

    }
}
