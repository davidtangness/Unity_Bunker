using UnityEngine;
using System.Collections;

public class PlayerShootController : MonoBehaviour
{

		public GameObject target;
		public GameObject ammo;
		public float shotsPerSecond;
		private float nextShotTime;
		public float shotSpeed;


		// Use this for initialization
		void Start ()
		{
				target = GameObject.FindWithTag ("Enemy");
		}
	
		// Update is called once per frame
		void Update ()
		{
				//if no target exists, wait for PlayerInputController to set one.
				if (target != null){
						//Determine vector from player to target
						float xDifference = target.transform.position.x - transform.position.x;
						float yDifference = target.transform.position.y - transform.position.y;
						Vector3 targetVector = new Vector3 (xDifference, yDifference, 0);

						//Raycast to find obstacles between the player and target		
						int sightLayerMask = 1 << 10; //Check layer 10: Bunkers
						RaycastHit2D hit = Physics2D.Raycast (transform.position, targetVector, Mathf.Infinity, sightLayerMask);

						//If no obstacles are found, fire at the target
						if (hit.collider == null & Time.time > nextShotTime) {
								nextShotTime = Time.time + (1 / shotsPerSecond);	
								FireAtTarget ();			
						}
				}
		}

		void FireAtTarget ()
		{
				//Determine shot velocity
				//TODO: Consider leading the target?
				float xDifference = target.transform.position.x - transform.position.x;
				float yDifference = target.transform.position.y - transform.position.y;
				Vector3 vectorToTarget = new Vector3 (xDifference, yDifference, 0);
				Vector3 firingVector = vectorToTarget * (shotSpeed / (Vector3.Magnitude (vectorToTarget)));
				
				//Spawn projectile prefab and set velocity
				GameObject projectile = (GameObject)Instantiate (ammo, transform.position, Quaternion.identity);
				projectile.rigidbody2D.velocity = firingVector;

				//Set timer until next shot
				nextShotTime = Time.time + (1 / shotsPerSecond);
		}

}
