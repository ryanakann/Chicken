﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class TriggerOcean : MonoBehaviour {

	public MorphTerrain morphTerrain;
	public CinemachineVirtualCamera virtualCamera;
	private CinemachineTrackedDolly dolly;
	public float cutsceneLength;

	private float currentVelocity;
	public bool cutsceneStarted;

	public PostProcessVolume defaultVolume;
	public PostProcessVolume oceanVolume;

	private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player") && !cutsceneStarted) {
			if (morphTerrain) {
				morphTerrain.Lerp(0.75f / cutsceneLength);
				StartCoroutine(FollowDolly());
				StartCoroutine(FadeVolumes());
			}
		}

	}

	private void Start () {
		if (virtualCamera.enabled) {
			virtualCamera.enabled = false;
		}
		dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		cutsceneStarted = false;
       
	}

	public IEnumerator FadeVolumes () {
		float t = 0f;
		while (t < 1f) {
			print("Ocean T:" + t);
			defaultVolume.weight = 1 - t;
			oceanVolume.weight = t;
			t += 0.25f * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	public IEnumerator FollowDolly () {
        PlayerInput.allowMovement = false;
		cutsceneStarted = true;
		virtualCamera.enabled = true;
		while (dolly.m_PathPosition < 0.75f) {
			dolly.m_PathPosition = Mathf.SmoothDamp(dolly.m_PathPosition, 1f, ref currentVelocity, cutsceneLength);
			yield return new WaitForEndOfFrame();
		}
		virtualCamera.enabled = false;
		cutsceneStarted = false;
        PlayerInput.allowMovement = true;
	}
}
