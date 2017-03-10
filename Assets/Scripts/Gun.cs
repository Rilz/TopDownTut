using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour 
{
	public enum GunType {Semi, Burst, Auto};

	public LayerMask collisionMask;
	public float gunID;
	public GunType gunType;
	public float rpm;
	public float damage = 1;

	public int totalAmmo = 40;
	public int ammoPerMag = 10;


	//Components
	public Transform spawn;
	public Transform shellEjectionPoint;
	public Rigidbody shell;
	private LineRenderer tracer;

	[HideInInspector]
	public GameGUI gui;



	//system
	private float secondsBetweenShots;
	private float nextPossibleShootTime;
	private int currentAmmoInMag;
	private bool reloading;

	void Start()
	{
		secondsBetweenShots = 60 / rpm;
		if(GetComponent<LineRenderer>())
		{
			tracer = GetComponent<LineRenderer> ();
		}

		currentAmmoInMag = ammoPerMag;
		if (gui)
		{
			gui.SetAmmoInfo (totalAmmo, currentAmmoInMag);
		}

	}


	public void Shoot ()
	{
		if (CanShoot())
		{

			Ray ray = new Ray (spawn.position,spawn.forward);
			RaycastHit hit;

			float shotDistance = 20;

			if (Physics.Raycast (ray, out hit, shotDistance, collisionMask)) 
			{
				shotDistance = hit.distance;

				if (hit.collider.GetComponent<Entity>())
				{
					hit.collider.GetComponent<Entity> ().TakeDamage (damage);
				}
			}

			nextPossibleShootTime = Time.time + secondsBetweenShots;
			currentAmmoInMag --;

			if (gui)
			{
				gui.SetAmmoInfo (totalAmmo, currentAmmoInMag);
			}

			GetComponent<AudioSource>().Play ();

			if (tracer)
			{
				StartCoroutine ("RenderTracer", ray.direction * shotDistance);
			}

			Rigidbody newShell = Instantiate (shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
			newShell.AddForce (shellEjectionPoint.forward * Random.Range (100f, 200f) + spawn.forward * Random.Range (-10f, 10f));
		}

	}

	public void ShootContinuous()
	{
		if (gunType == GunType.Auto)
		{
			Shoot ();
		}
	}

	private bool CanShoot()
	{
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime)
		{
			canShoot = false;
		}

		if (currentAmmoInMag == 0)
		{
			canShoot = false;
		}

		if(reloading)
		{
			canShoot = false;
		}

		return canShoot;
	}

	public bool reload()
	{
		if(totalAmmo != 0 && currentAmmoInMag != ammoPerMag)
		{
			reloading = true;
			return true;
		}

		return false;

	}

	public void FinishReload()
	{	
		
		reloading = false;
		currentAmmoInMag = ammoPerMag;
		totalAmmo -= ammoPerMag;

		if(totalAmmo < 0)
		{
			currentAmmoInMag += totalAmmo;
			totalAmmo = 0;
		}
		if (gui)
		{
			gui.SetAmmoInfo (totalAmmo, currentAmmoInMag);
		}

	}

	IEnumerator RenderTracer(Vector3 hitPoint)
	{
		tracer.enabled = true;
		tracer.SetPosition (0, spawn.position);
		tracer.SetPosition (1, spawn.position + hitPoint);

		yield return null;
		tracer.enabled = false;
	}

}



