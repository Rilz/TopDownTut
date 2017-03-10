using UnityEngine;
using System.Collections;

public class PainTrain : MonoBehaviour 
{

	public Entity entity;

	public int damageToGive;

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			entity.health -= damageToGive;
		}
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}
}
