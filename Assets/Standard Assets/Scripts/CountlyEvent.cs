using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CountlyEvent
{
	private double? _sum;

	private Dictionary<string, string> _segmentation;

	private string _Key_k__BackingField;

	private int _Count_k__BackingField;

	private bool _UsingSum_k__BackingField;

	private bool _UsingSegmentation_k__BackingField;

	public string Key
	{
		get;
		set;
	}

	public int Count
	{
		get;
		set;
	}

	public bool UsingSum
	{
		get;
		private set;
	}

	public double? Sum
	{
		get
		{
			return this._sum;
		}
		set
		{
			this.UsingSum = true;
			this._sum = value;
		}
	}

	public bool UsingSegmentation
	{
		get;
		private set;
	}

	public Dictionary<string, string> Segmentation
	{
		get
		{
			return this._segmentation;
		}
		set
		{
			this.UsingSegmentation = true;
			this._segmentation = value;
		}
	}

	public CountlyEvent()
	{
		this.Key = string.Empty;
		this.UsingSum = false;
		this.UsingSegmentation = false;
	}
}
