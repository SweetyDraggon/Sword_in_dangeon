using System;

public class MissionsMenu : ShopWindow
{
	public int state = 1;

	public MissionRow row1;

	public MissionRow row2;

	public MissionRow row3;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
		base.onSlideInStart += new CustomWindowEvent(this.activate);
		this.updateWindow();
		this.handleResolutions();
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
	}

	public void activate()
	{
		this.updateWindow();
	}

	public override void closeClicked()
	{
		this.deactivate();
	}

	public void deactivate()
	{
		WindowManager.Instance.HideMenu(this);
	}

	public void updateWindow()
	{
		this.row1.updateRow(Game.Instance.questHandler.quest1);
		this.row2.updateRow(Game.Instance.questHandler.quest2);
		this.row3.updateRow(Game.Instance.questHandler.quest3);
	}
}
