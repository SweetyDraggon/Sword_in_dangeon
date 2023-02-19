using TMPro;
using UnityEngine;

public class AlertMenu : CustomWindow
{
	public TextMeshPro descriptionTxt;
	public TextMeshPro titleTxt;
	
	public GameObject target;

	public string onButton1Clicked;

	public string onButton2Clicked;

	public GameObject button1;

	public GameObject button2;

	public tk2dTextMesh txtTitle;

	public tk2dTextMesh txtDescription;

	public tk2dTextMesh txtButton1;

	public tk2dTextMesh txtButton2;

	public string title = "Alert";

	public string description = "Lorem ipsum";

	public string button1Text = "Cancel";

	public string button2Text = "OK";

	public TextMeshPro button1Tmp;
	public TextMeshPro button2Tmp;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateWindow);
		base.onSlideInStart += new CustomWindowEvent(this.updateWindow);
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateWindow);
	}

	public void button1Clicked()
	{
		if (this.target != null && this.onButton1Clicked.Length > 0)
		{
			this.target.SendMessage(this.onButton1Clicked);
		}
		WindowManager.Instance.HideMenu(this);
	}

	public void button2Clicked()
	{
		if (this.target != null && this.onButton2Clicked.Length > 0)
		{
			this.target.SendMessage(this.onButton2Clicked);
		}
		WindowManager.Instance.HideMenu(this);
	}

	public void updateWindow()
	{
		this.txtTitle.text = this.title;
		this.titleTxt.text = this.title;
		this.descriptionTxt.text = this.description;
		this.txtDescription.text = this.description;
		this.txtButton1.text = this.button1Text;
		this.txtButton2.text = this.button2Text;
		button1Tmp.text= this.button1Text;
		button2Tmp.text = this.button2Text;
		if (this.button1Text == string.Empty)
		{
			this.button1.SetActive(false);
			Vector3 localPosition = this.button2.transform.localPosition;
			localPosition.x = 0f;
			this.button2.transform.localPosition = localPosition;
		}
		else if (this.button2Text == string.Empty)
		{
			this.button2.SetActive(false);
			Vector3 localPosition2 = this.button1.transform.localPosition;
			localPosition2.x = 0f;
			this.button1.transform.localPosition = localPosition2;
		}
	}
}
