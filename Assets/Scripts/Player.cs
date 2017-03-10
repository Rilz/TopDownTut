using UnityEngine;
using System.Collections;

public class Player : Entity 
{
	private int level;
	private float currentLevelExperience;
	private float experienceToLevel;

	private GameGUI gui;

	void Start ()
	{
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
		LevelUp ();
	}

	public void AddExperience(float exp)
	{
		currentLevelExperience += exp;
		if (currentLevelExperience >= experienceToLevel) 
		{
			currentLevelExperience -= experienceToLevel;
			LevelUp ();
		}

		gui.SetPlayerExperience (currentLevelExperience / experienceToLevel, level);
	}


		private void LevelUp()
		{
			level++;
			experienceToLevel = level * 50 + Mathf.Pow(level *2,2);

			AddExperience(0);
		}

}



