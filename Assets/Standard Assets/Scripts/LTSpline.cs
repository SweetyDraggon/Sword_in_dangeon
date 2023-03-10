using System;
using UnityEngine;

[Serializable]
public class LTSpline
{
	public Vector3[] pts;

	private float[] lengthRatio;

	private float[] lengths;

	private int numSections;

	private int currPt;

	private float totalLength;

	public LTSpline(params Vector3[] pts)
	{
		this.pts = new Vector3[pts.Length];
		Array.Copy(pts, this.pts, pts.Length);
		this.numSections = pts.Length - 3;
		int num = 20;
		this.lengthRatio = new float[this.numSections];
		this.lengths = new float[num];
		Vector3 b = new Vector3(float.PositiveInfinity, 0f, 0f);
		this.totalLength = 0f;
		for (int i = 0; i < num; i++)
		{
			float t = (float)i * 1f / (float)num;
			Vector3 vector = this.interp(t);
			if (i >= 1)
			{
				this.lengths[i] = (vector - b).magnitude;
			}
			this.totalLength += this.lengths[i];
			b = vector;
		}
		for (int j = 0; j < this.lengths.Length; j++)
		{
			float num2 = (float)j * 1f / (float)this.lengths.Length;
			this.currPt = Mathf.Min(Mathf.FloorToInt(num2 * (float)this.numSections), this.numSections - 1);
			this.lengthRatio[this.currPt] += this.lengths[j] / this.totalLength;
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"lengthRatio[",
				this.currPt,
				"]:",
				this.lengthRatio[this.currPt],
				" lengths[",
				j,
				"]:",
				this.lengths[j]
			}));
		}
	}

	public float map(float t)
	{
		float num = 0f;
		for (int i = 0; i < this.lengthRatio.Length; i++)
		{
			if (num + this.lengthRatio[i] >= t)
			{
				return num + (t - num) / this.lengthRatio[i] * this.lengthRatio[i];
			}
			num += this.lengthRatio[i];
		}
		return 1f;
	}

	public Vector3 interp(float t)
	{
		this.currPt = Mathf.Min(Mathf.FloorToInt(t * (float)this.numSections), this.numSections - 1);
		float num = t * (float)this.numSections - (float)this.currPt;
		Vector3 a = this.pts[this.currPt];
		Vector3 a2 = this.pts[this.currPt + 1];
		Vector3 vector = this.pts[this.currPt + 2];
		Vector3 b = this.pts[this.currPt + 3];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num * num * num) + (2f * a - 5f * a2 + 4f * vector - b) * (num * num) + (-a + vector) * num + 2f * a2);
	}

	public Vector3 point(float ratio)
	{
		return this.interp(ratio);
	}

	public void gizmoDraw(float t = -1f)
	{
		if (this.lengthRatio != null && this.lengthRatio.Length > 0)
		{
			Gizmos.color = Color.white;
			Vector3 to = this.point(0f);
			for (int i = 1; i <= 120; i++)
			{
				float ratio = (float)i / 120f;
				Vector3 vector = this.point(ratio);
				Gizmos.DrawLine(vector, to);
				to = vector;
			}
			if (t >= 0f)
			{
				Gizmos.color = Color.blue;
				Vector3 vector2 = this.point(t);
				Gizmos.DrawLine(vector2, vector2 + this.Velocity(t));
			}
		}
	}

	public Vector3 Velocity(float t)
	{
		t = this.map(t);
		int num = this.pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 a = this.pts[num2];
		Vector3 a2 = this.pts[num2 + 1];
		Vector3 a3 = this.pts[num2 + 2];
		Vector3 b = this.pts[num2 + 3];
		return 1.5f * (-a + 3f * a2 - 3f * a3 + b) * (num3 * num3) + (2f * a - 5f * a2 + 4f * a3 - b) * num3 + 0.5f * a3 - 0.5f * a;
	}
}
