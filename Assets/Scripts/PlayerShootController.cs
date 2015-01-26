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
				//if no target exists, skip the rest of this update.
				if (target == null) {
						//Do nothing
				} else {
						//If character has LOS to target, fire at target

						float xDifference = target.transform.position.x - transform.position.x;
						float yDifference = target.transform.position.y - transform.position.y;

						Vector3 targetVector = new Vector3 (xDifference, yDifference, 0);

						//TODO: Add check for aim reticle once those are implemented.
						int sightLayerMask = 3 << 9; //Check only layers 9 and 10 (Enemy and Bunkers)
						RaycastHit2D hit = Physics2D.Raycast (transform.position, targetVector, Mathf.Infinity, sightLayerMask);

						if (hit.collider == null) {
								//TODO: say sight blocked?
						} else {
								if (hit.rigidbody.gameObject == target && Time.time > nextShotTime) {
										nextShotTime = Time.time + (1 / shotsPerSecond);	
										FireAtTarget ();

								}
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
