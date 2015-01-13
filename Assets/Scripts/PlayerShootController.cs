using UnityEngine;
using System.Collections;

public class PlayerShootController : MonoBehaviour
{

		public GameObject target;
		public GameObject ammo;

		// Use this for initialization
		void Start ()
		{
				target = GameObject.FindWithTag ("Enemy");
		}
	
		// Update is called once per frame
		void Update ()
		{
				//TODO: If player clicks on enemy, set enemy as target?


				//TODO: Restrict rate-of-fire


				//If character can see target, fire at target

				float xDifference = target.transform.position.x - transform.position.x;
				float yDifference = target.transform.position.y - transform.position.y;

				Vector2 targetVector = new Vector2 (xDifference, yDifference);

				int sightLayerMask = 3 << 9; //Check only layers 9 and 10 (Enemy and Bunkers)
				RaycastHit2D hit = Physics2D.Raycast (transform.position, targetVector, Mathf.Infinity, sightLayerMask);

				if (hit.collider == null) {
						//TODO: say sight blocked?
				} else {
						if (hit.rigidbody.gameObject == target) {
								FireAtTarget ();
						}
				}


		}

		void FireAtTarget ()
		{
				//Determine aim vector
				//TODO: Consider leading the target?
				float xDifference = target.transform.position.x - transform.position.x;
				float yDifference = target.transform.position.y - transform.position.y;
				Vector2 aimVector = new Vector2 (xDifference, yDifference);

				//TODO: Apply inaccuracy to firing vector
				Vector2 firingVector = aimVector;

				//Spawn projectile prefab and set direction

				GameObject projectile = (GameObject)Instantiate (ammo, transform.position, Quaternion.identity);
				projectile.rigidbody2D.velocity = firingVector;

		}

}
