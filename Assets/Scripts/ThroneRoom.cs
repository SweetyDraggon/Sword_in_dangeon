using System;
using TMPro;
using UnityEngine;

public class ThroneRoom : MonoBehaviour
{
	public TextMeshPro kingText;

	private tk2dTextMesh kingTextShadow;

	public TextMeshPro guardText;

	private tk2dTextMesh guardTextShadow;

	public int kingDialogLine;

	public int guardDialogLine;

	public float idleTimer;

	public float idleTimerMax;

	public float alpha;

	public int state;

	public string[] kingDialog;

	public string[] guardDialog;

	private void Start()
	{
        this.kingDialog = new string[]
        {
            "I'll_feed_you_to_my_dogs!",
            "Juggle_faster_you_fool!",
            "Worst_jester_ever!",
            "Boring!!"
        };
        this.guardDialog = new string[]
		{
            "I_used_to_be_a_nadventurer_like_you...",
            "Good_luck_down_there!",
			"Glad_I'm_not_the_court_jester..."
		};
		this.state = 0;
		this.alpha = 0f;
		this.idleTimer = 0f;
		this.idleTimerMax = 2f;
		this.kingDialogLine = 0;
		this.guardDialogLine = 0;
		this.kingText.text = Localisation.GetString( this.kingDialog[this.kingDialogLine]);
		this.guardText.text =  Localisation.GetString(this.guardDialog[this.guardDialogLine]);
		this.kingTextShadow = this.kingText.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
		this.guardTextShadow = this.guardText.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (this.state == 0)
		{
			this.alpha += 1f * deltaTime;
			if (this.alpha >= 1f)
			{
				this.alpha = 1f;
				this.state = 1;
			}
		}
		else if (this.state == 1)
		{
			this.idleTimer += deltaTime;
			if (this.idleTimer >= this.idleTimerMax)
			{
				this.idleTimer = 0f;
				this.state = 2;
			}
		}
		else if (this.state == 2)
		{
			this.alpha -= 1f * deltaTime;
			if (this.alpha <= 0.05f)
			{
				this.alpha = 0f;
				this.state = 3;
				this.idleTimer = 0f;
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += deltaTime;
			if (this.idleTimer >= 1f)
			{
				this.state = 0;
				this.idleTimer = 0f;
				this.kingDialogLine++;
				if (this.kingDialogLine >= this.kingDialog.Length)
				{
					this.kingDialogLine = 0;
				}
				this.kingText.text = Localisation.GetString(this.kingDialog[this.kingDialogLine]);
                //this.kingText.Commit();
                this.guardDialogLine++;
				if (this.guardDialogLine >= this.guardDialog.Length)
				{
					this.guardDialogLine = 0;
				}
				this.guardText.text = Localisation.GetString(this.guardDialog[this.guardDialogLine]);
				//this.guardText.Commit();
			}
		}
		this.kingText.color = new Color(1f, 1f, 1f, this.alpha);
		this.guardText.color = new Color(1f, 1f, 1f, this.alpha);
//		this.kingTextShadow.color = new Color(0f, 0f, 0f, this.alpha * 0.5f);
//		this.guardTextShadow.color = new Color(0f, 0f, 0f, this.alpha * 0.5f);
	}
}
