using System;
using UnityEngine;

[Serializable]
public class FixedTimer
{
	public float dt;

	public double currentTime;

	public double accumulator;

	public FixedTimer(int fps = 30)
	{
		this.currentTime = (double)Time.time;
		this.accumulator = 0.0;
		this.dt = 1f / (float)fps;
	}

	public void update()
	{
		double num = (double)Time.time;
		double num2 = num - this.currentTime;
		this.currentTime = num;
		this.accumulator += num2;
	}

	public bool step()
	{
		return this.accumulator >= (double)this.dt;
	}

	public void decrement()
	{
		this.accumulator -= (double)this.dt;
	}
}
