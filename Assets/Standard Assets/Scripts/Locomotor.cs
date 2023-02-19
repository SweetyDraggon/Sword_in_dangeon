using System;
using UnityEngine;

public class Locomotor : MonoBehaviour
{
	private const float CAMERA_DISTANCE_FROM_PLAYER = 8f;

	private const float CAMERA_Y_POSITION = 5f;

	private const float PLAYER_SPEED = 0.2f;

	public Transform spawnPoint;

	private bool isGrounded;

	private Touch touch;

	private float touchDownX;

	private float horizontal;

	private void Awake()
	{
		TutorialController.OnPhaseChange += new TutorialController.TutorialPhaseChange(this.HandleTutorialControllerOnPhaseChange);
		GetComponent<Rigidbody>().freezeRotation = (this.isGrounded = true);
		this.Respawn();
		this.UpdateCamera();
	}

	private void OnDestroy()
	{
		TutorialController.OnPhaseChange -= new TutorialController.TutorialPhaseChange(this.HandleTutorialControllerOnPhaseChange);
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount > 0)
		{
			this.touch = UnityEngine.Input.GetTouch(0);
			if (this.touchDownX == 0f)
			{
				this.touchDownX = this.touch.position.x;
			}
			this.horizontal = Mathf.Sign(this.touch.position.x - this.touchDownX) * 0.2f;
			if (this.isGrounded && UnityEngine.Input.touchCount == 2)
			{
				this.Jump();
			}
		}
		else
		{
			this.touchDownX = 0f;
		}
		this.UpdateCamera();
	}

	private void FixedUpdate()
	{
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * this.horizontal);
		this.horizontal = 0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		ContactPoint[] contacts = collision.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint contactPoint = contacts[i];
			if (contactPoint.otherCollider.name == "Platform")
			{
				this.isGrounded = true;
				return;
			}
			if (contactPoint.otherCollider.name == "RespawnCollider")
			{
				this.Respawn();
				return;
			}
		}
	}

	private void HandleTutorialControllerOnPhaseChange(bool isShowing)
	{
		base.enabled = !isShowing;
		GetComponent<Rigidbody>().isKinematic = isShowing;
	}

	private void Jump()
	{
		GetComponent<Rigidbody>().AddForce(Vector3.up * 8f, ForceMode.Impulse);
		this.isGrounded = false;
	}

	private void UpdateCamera()
	{
		Camera.main.transform.position = new Vector3(base.transform.position.x + 8f, 5f, base.transform.position.z);
		Camera.main.transform.LookAt(base.transform);
	}

	private void Respawn()
	{
		base.transform.position = this.spawnPoint.position;
	}
}
