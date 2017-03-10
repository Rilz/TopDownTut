using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour 
{

	public Transform experienceBar;
	public TextMesh levelText;

	public TextMesh ammoText;

	public void SetPlayerExperience(float percentToLevel, int playerLevel)
	{
		levelText.text = "level: " + playerLevel;
		experienceBar.localScale = new Vector3 (percentToLevel, 1, 1);
	}

	public void SetAmmoInfo(int totalAmmo, int ammoInMag)
	{
		ammoText.text = ammoInMag+ "/" + totalAmmo;
	}
}
