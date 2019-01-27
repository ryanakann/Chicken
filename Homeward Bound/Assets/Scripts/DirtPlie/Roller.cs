﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Roller : MonoBehaviour
{
    SphereCollider sphere;

    private void Start() {
        sphere = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player")) {
            Debug.Log("BAMO!");
        }
    }

}