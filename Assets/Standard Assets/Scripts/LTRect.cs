using System;
using UnityEngine;

[Serializable]
public class LTRect
{
	public Rect _rect;

	public float alpha;

	public float rotation;

	public Vector2 pivot;

	public bool rotateEnabled;

	[HideInInspector]
	public bool rotateFinished;

	public bool alphaEnabled;

	public static bool colorTouched;

	public float x
	{
		get
		{
			return this._rect.x;
		}
		set
		{
			this._rect.x = value;
		}
	}

	public float y
	{
		get
		{
			return this._rect.y;
		}
		set
		{
			this._rect.y = value;
		}
	}

	public Rect rect
	{
		get
		{
			if (LTRect.colorTouched)
			{
				LTRect.colorTouched = false;
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
			}
			if (this.rotateEnabled)
			{
				if (this.rotateFinished)
				{
					this.rotateFinished = false;
					this.rotateEnabled = false;
					this._rect.x = this._rect.x + this.pivot.x;
					this._rect.y = this._rect.y + this.pivot.y;
					this.pivot = Vector2.zero;
					GUI.matrix = Matrix4x4.identity;
				}
				else
				{
					Matrix4x4 identity = Matrix4x4.identity;
					identity.SetTRS(this.pivot, Quaternion.Euler(0f, 0f, this.rotation), Vector3.one);
					GUI.matrix = identity;
				}
			}
			if (this.alphaEnabled)
			{
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
				LTRect.colorTouched = true;
			}
			return this._rect;
		}
		set
		{
			this._rect = value;
		}
	}

	public LTRect()
	{
		this.reset();
		this.rotateEnabled = (this.alphaEnabled = true);
		this._rect = new Rect(0f, 0f, 1f, 1f);
	}

	public LTRect(Rect rect)
	{
		this._rect = rect;
		this.reset();
	}

	public LTRect(float x, float y, float width, float height)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	public LTRect(float x, float y, float width, float height, float alpha)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	public LTRect(float x, float y, float width, float height, float alpha, float rotation)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = rotation;
		this.rotateEnabled = (this.alphaEnabled = false);
		if (rotation != 0f)
		{
			this.rotateEnabled = true;
			this.resetForRotation();
		}
	}

	public void reset()
	{
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	public void resetForRotation()
	{
		if (this.pivot == Vector2.zero)
		{
			this.pivot = new Vector2(this._rect.x + this._rect.width * 0.5f, this._rect.y + this._rect.height * 0.5f);
			this._rect.x = this._rect.x + -this.pivot.x;
			this._rect.y = this._rect.y + -this.pivot.y;
		}
	}
}
