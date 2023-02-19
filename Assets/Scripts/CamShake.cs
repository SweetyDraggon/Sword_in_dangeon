using System;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	public bool Shaking;

	public float shakeDecay;

	public float shakeIntensity;

	private float ShakeDecay;

	private float ShakeIntensity;

	private Quaternion OriginalRot;

	private FollowCamera camFollowScript;

	private void Start()
	{
		this.Shaking = false;
		this.camFollowScript = base.GetComponent<FollowCamera>();
	}

	private void Update()
	{
		if (this.ShakeIntensity > 0f && this.Shaking)
		{
			Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
			Vector3 vector = new Vector3(insideUnitSphere.x, insideUnitSphere.y, 0f);
			Vector3 vector2 = vector.normalized * this.ShakeIntensity;
			this.camFollowScript.addOffset(vector2.x, vector2.y);
			this.ShakeIntensity -= this.ShakeDecay;
		}
		else if (this.Shaking)
		{
			this.Shaking = false;
			base.transform.rotation = Quaternion.identity;
		}
	}

	public void DoShake()
	{
		this.OriginalRot = base.transform.rotation;
		this.ShakeIntensity = this.shakeIntensity;
		this.ShakeDecay = this.shakeDecay;
		this.Shaking = true;
	}
}
