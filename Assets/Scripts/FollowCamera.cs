using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public float dampTime = 0.15f;

	public Transform entityTransform;

	public bool useFixedUpdate = true;

	public bool useBounds;

	private Vector3 velocity = Vector3.zero;

	private tk2dCamera tkCamera;

	private Rect screenExtents;

	private Vector2 minExtents;

	private Vector2 maxExtents;

	private float offsetX;

	private float offsetY;

	public void Awake()
	{
		this.tkCamera = Camera.main.GetComponent<tk2dCamera>();
	}

	public void setBounds(Vector3 upExtent, Vector3 rightExtent, Vector3 downExtent, Vector3 leftExtent)
	{
		this.minExtents = new Vector2(leftExtent.x, downExtent.y);
		this.maxExtents = new Vector2(rightExtent.x, upExtent.y);
		this.screenExtents = this.tkCamera.ScreenExtents;
		this.useBounds = true;
	}

	public void setBounds(Bounds bounds)
	{
		this.minExtents = new Vector2(bounds.min.x, bounds.min.y);
		this.maxExtents = new Vector2(bounds.max.x, bounds.max.y);
		this.screenExtents = this.tkCamera.ScreenExtents;
		this.useBounds = true;
	}

	public void setBounds(GameObject map)
	{
		if (map != null)
		{
			BoxCollider boxCollider = map.GetComponent<Collider>() as BoxCollider;
			if (boxCollider)
			{
				this.setBounds(boxCollider.bounds);
			}
		}
	}

	public void setBounds(Map map)
	{
		this.setBounds(map.gameObject);
	}

	private void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.updateCamera();
		}
	}

	private void FixedUpdate()
	{
		if (this.useFixedUpdate)
		{
			this.updateCamera();
		}
	}

	public void addOffset(float oX, float oY)
	{
		this.offsetX = oX;
		this.offsetY = oY;
	}

	private void updateCamera()
	{
		if (this.entityTransform == null)
		{
			return;
		}
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
		float num = this.entityTransform.position.x - vector.x;
		float num2 = this.entityTransform.position.y - vector.y;
		num += this.offsetX;
		num2 += this.offsetY;
		Vector3 b = new Vector3(num, num2, 0f);
		Vector3 position = Vector3.SmoothDamp(Camera.main.transform.position, Camera.main.transform.position + b, ref this.velocity, this.dampTime);
		Camera.main.transform.position = position;
		if (this.useBounds)
		{
			this.constrainCamera();
		}
		this.offsetX = 0f;
		this.offsetY = 0f;
	}

	private void constrainCamera()
	{
		Vector3 position = this.tkCamera.transform.position;
		position.x = Mathf.Clamp(position.x, this.minExtents.x - this.screenExtents.xMin, this.maxExtents.x - this.screenExtents.xMax);
		position.y = Mathf.Clamp(position.y, this.minExtents.y - this.screenExtents.yMin, this.maxExtents.y - this.screenExtents.yMax);
		this.tkCamera.transform.position = position;
	}
}
