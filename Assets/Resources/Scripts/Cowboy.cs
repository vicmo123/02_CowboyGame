using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 initialPos;
    public Vector3 initialRotation;
    public AudioSource gunSound;
    private Vector3 gunShotForce;
    public Vector3 direction;

    private void Awake()
    {
        initialPos = rb.transform.position;
        initialRotation = rb.transform.eulerAngles;
        gunShotForce = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        rb.AddForce(gunShotForce, ForceMode.Force);
    }

    public void CowboyDies(Vector3 force)
    {
        Debug.Log(rb.gameObject.name + " is dead!");

        rb.isKinematic = false;
        gunShotForce = force;
    }

    public void CowboyShoots()
    {
        Debug.Log(rb.gameObject.name + " fires a bullet!");
        gunSound.Play();
    }

    public void ResetCowboy()
    {
        rb.isKinematic = true;
        rb.transform.position = initialPos;
        rb.transform.eulerAngles = initialRotation;
    }
}
