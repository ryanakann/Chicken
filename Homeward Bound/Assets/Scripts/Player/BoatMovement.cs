﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
	public Transform player;
    Rigidbody rb;
    public Transform boatPoint;

    Animator anim;

    public bool move, start;   //If true, allow boat to move and rotate

    public float speed = 500f;
    [HideInInspector] public float turningSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		move = false;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            rb.velocity = transform.forward * speed;
            float hz = Input.GetAxisRaw("Horizontal");
            Vector3 rotate = new Vector3(0.0f, hz * turningSpeed, 0.0f);
            transform.Rotate(rotate);
            Debug.Log("MKAY");
		}
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (player && start)
        {
            player.position = boatPoint.position;
            player.rotation = boatPoint.rotation;
        }
    }

    public void InitializeBoat()
    {
        transform.up = Vector3.up;
        move = false;
    }

	public void Sink () {
        start = false;
        move = false;
        anim.SetTrigger("Crash");
        DeathMachine.instance.Kill(Vector3.zero);
	}
}
