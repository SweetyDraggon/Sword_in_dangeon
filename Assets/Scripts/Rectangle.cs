using System;

[Serializable]
public class Rectangle
{
	public int x;

	public int y;

	public int width;

	public int height;

	public Rectangle(int nX, int nY, int nW, int nH)
	{
		this.x = nX;
		this.y = nY;
		this.width = nW;
		this.height = nH;
	}

	public Rectangle(float nX, float nY, int nW, int nH)
	{
		this.x = (int)nX;
		this.y = (int)nY;
		this.width = nW;
		this.height = nH;
	}

	public bool Intersects(Rectangle rect)
	{
		return this.x + this.width >= rect.x && rect.x + rect.width >= this.x && this.y + this.height >= rect.y && rect.y + rect.height >= this.y;
	}
}
