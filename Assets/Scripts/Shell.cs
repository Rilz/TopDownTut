using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour 
{

	public float lifeTime = 5;

	private Material mat;
	private Color originalCol;
	private float fadePercent;
	private float deathTime;
	private bool fading;




	// Use this for initialization
	void Start () 
	{
		mat = GetComponent<Renderer>().material;
		originalCol = mat.color;
		deathTime = Time.time + lifeTime;
		//deathTime = lifeTime;

		StartCoroutine ("Fade");
	}

	IEnumerator Fade()
	{
		while (true)
		{
			yield return new WaitForSeconds (.2f);
			if (fading)
			{
				fadePercent += Time.deltaTime * 5;
				mat.color = Color.Lerp (originalCol, Color.clear, fadePercent);

				if (fadePercent >= 1)
				{
					Destroy (gameObject);
				}
			}
			else
			{

				if (Time.time > deathTime)
				{
				fading = true;
				}
			}
		}//while truu dat
	}//IEnumerator bräketti

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Ground")
		{
			GetComponent<Rigidbody>().Sleep ();
		}
	}


}
