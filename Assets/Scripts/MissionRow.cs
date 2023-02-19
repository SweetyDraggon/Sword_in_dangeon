using System;
using TMPro;
using UnityEngine;

public class MissionRow : MonoBehaviour
{
	public GameObject dataGameObject;

	public tk2dSprite background;

	public tk2dSprite icon;

	public tk2dTextMesh txtDescription;

	public tk2dTextMesh txtProgress;

	public tk2dTextMesh txtReward;

	public GameObject overlay;
	[SerializeField] private TextMeshPro _questDescription;

	public void updateRow(int questNumber)
	{
		if (questNumber == 0)
		{
			this.overlay.SetActive(true);
			return;
		}
		this.overlay.SetActive(false);
		this.background.SetSprite("quest_display1");
		this.dataGameObject.SetActive(true);
		this.icon.SetSprite("quest_icon" + questNumber);
		//_questDescription.text = Localisation.GetString(Game.Instance.questHandler.quests[questNumber - 1, 0]);
		this.txtDescription.text =  Game.Instance.questHandler.quests[questNumber - 1, 0];
		_questDescription.text = Localisation.GetString(this.txtDescription.text);
		//this.txtDescription.text = Localisation.GetString(this.txtDescription.text);
		int num = Convert.ToInt32(Game.Instance.questHandler.quests[questNumber - 1, 1]);
		this.txtProgress.text = Game.Instance.questHandler.tracker[num] + "/" + Game.Instance.questHandler.quests[questNumber - 1, 2];
		this.txtReward.text = "$" + Game.Instance.questHandler.quests[questNumber - 1, 3];
	}
}
