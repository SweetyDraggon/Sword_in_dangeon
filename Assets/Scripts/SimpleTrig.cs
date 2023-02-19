using System;
using UnityEngine;

public class SimpleTrig
{
	public float getAngle(float startX, float startY, float endX, float endY)
	{
		int sector = this.getSector(startX, startY, endX, endY);
		float vectorX = this.getVectorX(startX, endX);
		float vectorY = this.getVectorY(startY, endY);
		float angle;
		if (sector == 1)
		{
			angle = Mathf.Atan(vectorX / vectorY);
		}
		else if (sector == 2)
		{
			angle = Mathf.Atan(vectorY / vectorX) + 1.57079637f;
		}
		else if (sector == 3)
		{
			angle = Mathf.Atan(vectorX / vectorY) + 3.14159274f;
		}
		else
		{
			angle = Mathf.Atan(vectorY / vectorX) + 3.14159274f + 1.57079637f;
		}
		return this.getDegrees(angle);
	}

	public float toRadians(float angle)
	{
		return angle * 0.0174532924f;
	}

	public float getDegrees(float angle)
	{
		return angle * 57.2957764f;
	}

	public int getSector(float startX, float startY, float endX, float endY)
	{
		if (endX > startX && endY <= startY)
		{
			return 1;
		}
		if (endX > startX && endY > startY)
		{
			return 2;
		}
		if (endX <= startX && endY > startY)
		{
			return 3;
		}
		if (endX <= startX && endY <= startY)
		{
			return 4;
		}
		return 0;
	}

	public float getXvel(float startX, float startY, float endX, float endY, float velocity)
	{
		float angle = this.getAngle(startX, startY, endX, endY);
		int sector = this.getSector(startX, startY, endX, endY);
		float result = 0f;
		if (sector == 1)
		{
			float angle2 = angle;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Sin(f) * velocity);
		}
		else if (sector == 2)
		{
			float angle2 = angle - 90f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Cos(f) * velocity);
		}
		else if (sector == 3)
		{
			float angle2 = angle - 180f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Sin(f) * velocity * -1f);
		}
		else if (sector == 4)
		{
			float angle2 = angle - 270f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Cos(f) * velocity * -1f);
		}
		return result;
	}

	public float getYvel(float startX, float startY, float endX, float endY, float velocity)
	{
		float angle = this.getAngle(startX, startY, endX, endY);
		int sector = this.getSector(startX, startY, endX, endY);
		float result = 0f;
		if (sector == 1)
		{
			float angle2 = angle;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Cos(f) * velocity * -1f);
		}
		else if (sector == 2)
		{
			float angle2 = angle - 90f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Sin(f) * velocity);
		}
		else if (sector == 3)
		{
			float angle2 = angle - 180f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Cos(f) * velocity);
		}
		else if (sector == 4)
		{
			float angle2 = angle - 270f;
			float f = this.toRadians(angle2);
			result = Mathf.Round(Mathf.Sin(f) * velocity * -1f);
		}
		return result;
	}

	public float getVectorX(float startX, float endX)
	{
		if (startX < endX)
		{
			return endX - startX;
		}
		return startX - endX;
	}

	public float getVectorY(float startY, float endY)
	{
		if (startY < endY)
		{
			return endY - startY;
		}
		return startY - endY;
	}
}
