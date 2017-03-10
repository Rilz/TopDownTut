using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour 
{

	public float health;


	public virtual void TakeDamage (float dmg)
	{
		health -= dmg;


		if (health <= 0)
		{
			Die ();
		}
	


	}

	public virtual void Die()
	{
		Debug.Log ("Dead");
		Destroy(gameObject);
	}
}
