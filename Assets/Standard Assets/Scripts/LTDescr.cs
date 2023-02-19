using System;
using System.Collections;
using UnityEngine;

public class LTDescr
{
	public bool toggle;

	public Transform trans;

	public LTRect ltRect;

	public Vector3 from;

	public Vector3 to;

	public Vector3 diff;

	public Vector3 point;

	public Vector3 axis;

	public Vector3 origRotation;

	public LTBezierPath path;

	public float time;

	public float lastVal;

	public bool useEstimatedTime;

	public bool useFrames;

	public bool hasInitiliazed;

	public bool hasPhysics;

	public float passed;

	public TweenAction type;

	public Hashtable optional;

	public float delay;

	public LeanTweenType tweenType;

	public AnimationCurve animationCurve;

	private int _id;

	public LeanTweenType loopType;

	public int loopCount;

	public float direction;

	public Action<float> onUpdateFloat;

	public Action<float, object> onUpdateFloatObject;

	public Action<Vector3> onUpdateVector3;

	public Action<Vector3, object> onUpdateVector3Object;

	public Action onComplete;

	public Action<object> onCompleteObject;

	public object onCompleteParam;

	public object onUpdateParam;

	public int uniqueId
	{
		get
		{
			return this._id | (int)((int)this.type << 24);
		}
	}

	public int id
	{
		get
		{
			return this.uniqueId;
		}
	}

	public override string ToString()
	{
		return string.Concat(new object[]
		{
			(!(this.trans != null)) ? "gameObject:null" : ("gameObject:" + this.trans.gameObject),
			" toggle:",
			this.toggle,
			" passed:",
			this.passed,
			" time:",
			this.time,
			" delay:",
			this.delay,
			" from:",
			this.from,
			" to:",
			this.to,
			" type:",
			this.type,
			" useEstimatedTime:",
			this.useEstimatedTime,
			" id:",
			this.id
		});
	}

	public LTDescr cancel()
	{
		LeanTween.removeTween(this._id);
		return this;
	}

	public void reset()
	{
		this.toggle = true;
		this.optional = null;
		this.passed = (this.delay = 0f);
		this.useEstimatedTime = (this.useFrames = (this.hasInitiliazed = false));
		this.animationCurve = null;
		this.tweenType = LeanTweenType.linear;
		this.loopType = LeanTweenType.once;
		this.direction = 1f;
		this.onUpdateFloat = null;
		this.onUpdateVector3 = null;
		this.onUpdateFloatObject = null;
		this.onUpdateVector3Object = null;
		this.onComplete = null;
		this.onCompleteObject = null;
		this.onCompleteParam = null;
		this.point = Vector3.zero;
	}

	public LTDescr pause()
	{
		this.lastVal = this.direction;
		this.direction = 0f;
		return this;
	}

	public LTDescr resume()
	{
		this.direction = this.lastVal;
		return this;
	}

	public LTDescr setAxis(Vector3 axis)
	{
		this.axis = axis;
		return this;
	}

	public LTDescr setDelay(float delay)
	{
		this.delay = delay * Time.timeScale;
		return this;
	}

	public LTDescr setEase(LeanTweenType easeType)
	{
		this.tweenType = easeType;
		return this;
	}

	public LTDescr setEase(AnimationCurve easeCurve)
	{
		this.animationCurve = easeCurve;
		return this;
	}

	public LTDescr setFrom(Vector3 from)
	{
		this.from = from;
		this.hasInitiliazed = true;
		this.diff = this.to - this.from;
		return this;
	}

	public LTDescr setId(int id)
	{
		this._id = id;
		return this;
	}

	public LTDescr setRepeat(int repeat)
	{
		this.loopCount = repeat;
		if ((repeat > 1 && this.loopType == LeanTweenType.once) || (repeat < 0 && this.loopType == LeanTweenType.once))
		{
			this.loopType = LeanTweenType.clamp;
		}
		return this;
	}

	public LTDescr setLoopType(LeanTweenType loopType)
	{
		this.loopType = loopType;
		return this;
	}

	public LTDescr setUseEstimatedTime(bool useEstimatedTime)
	{
		this.useEstimatedTime = useEstimatedTime;
		return this;
	}

	public LTDescr setUseFrames(bool useFrames)
	{
		this.useFrames = useFrames;
		return this;
	}

	public LTDescr setLoopCount(int loopCount)
	{
		this.loopCount = loopCount;
		return this;
	}

	public LTDescr setLoopOnce()
	{
		this.loopType = LeanTweenType.once;
		return this;
	}

	public LTDescr setLoopClamp()
	{
		this.loopType = LeanTweenType.clamp;
		return this;
	}

	public LTDescr setLoopPingPong()
	{
		this.loopType = LeanTweenType.pingPong;
		return this;
	}

	public LTDescr setOnComplete(Action onComplete)
	{
		this.onComplete = onComplete;
		return this;
	}

	public LTDescr setOnComplete(Action<object> onComplete)
	{
		this.onCompleteObject = onComplete;
		return this;
	}

	public LTDescr setOnCompleteParam(object onCompleteParam)
	{
		this.onCompleteParam = onCompleteParam;
		return this;
	}

	public LTDescr setOnUpdate(Action<float> onUpdate)
	{
		this.onUpdateFloat = onUpdate;
		return this;
	}

	public LTDescr setOnUpdateObject(Action<float, object> onUpdate)
	{
		this.onUpdateFloatObject = onUpdate;
		return this;
	}

	public LTDescr setOnUpdateVector3(Action<Vector3> onUpdate)
	{
		this.onUpdateVector3 = onUpdate;
		return this;
	}

	public LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateFloatObject = onUpdate;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3, object> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateVector3Object = onUpdate;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3> onUpdate, object onUpdateParam = null)
	{
		this.onUpdateVector3 = onUpdate;
		if (onUpdateParam != null)
		{
			this.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdateParam(object onUpdateParam)
	{
		this.onUpdateParam = onUpdateParam;
		return this;
	}

	public LTDescr setOrientToPath(bool doesOrient)
	{
		if (this.path == null)
		{
			this.path = new LTBezierPath();
		}
		this.path.orientToPath = doesOrient;
		return this;
	}

	public LTDescr setRect(LTRect rect)
	{
		this.ltRect = rect;
		return this;
	}

	public LTDescr setRect(Rect rect)
	{
		this.ltRect = new LTRect(rect);
		return this;
	}

	public LTDescr setPath(LTBezierPath path)
	{
		this.path = path;
		return this;
	}

	public LTDescr setPoint(Vector3 point)
	{
		this.point = point;
		return this;
	}
}
