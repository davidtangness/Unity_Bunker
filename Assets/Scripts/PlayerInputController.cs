using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{

		public GameObject reticlePrefab;
		public GameObject moveSelectorPrefab;
		private GameObject liveMoveSelector;
		private GameObject liveReticle;
		private CharacterMovementController playerMover;
		private PlayerShootController playerGun;

		void Start ()
		{
				playerMover = (CharacterMovementController)gameObject.GetComponent ("CharacterMovementController");
				playerGun = (PlayerShootController)gameObject.GetComponent ("PlayerShootController");
		}

		void Update ()
		{
				//TODO Restructure this monstrosity? Maybe create a while loop and just break when a raycast succeeds.
				if (Input.GetMouseButtonDown (0)) {
						Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						LayerMask mask = 1 << 13;
						Collider2D[] hitCollider = Physics2D.OverlapPointAll (mousePosition, mask);
						if (hitCollider.Length == 1) {
								selectMove (mousePosition);
						} else {
								//check layer 9: enemies
								mask = 1 << 9;
								hitCollider = Physics2D.OverlapPointAll (mousePosition, mask);
								if (hitCollider.Length == 1) {
										targetEnemy ();
								} else {
										//Check layer 10: bunkers
										mask = 1 << 10;
										hitCollider = Physics2D.OverlapPointAll (mousePosition, mask);
										if (hitCollider.Length == 1) {
												spawnMoveSelector (hitCollider [0].transform.position);	
										} else {
												SpawnReticle (mousePosition);

										}		
								}
						}
				}
		}


		//TODO: Prevent moveSelector from overlapping bunkers. Maybe just move it to a lower layer and move it down the click hierarchy?
		void spawnMoveSelector (Vector2 spawnLocation)
		{
				if (liveMoveSelector != null) {
						Destroy (liveMoveSelector);
				}
				liveMoveSelector = (GameObject)Instantiate (moveSelectorPrefab, spawnLocation, Quaternion.identity);
	
	
		}
		
		void selectMove (Vector2 destination)
		{
				if (liveMoveSelector != null) {
						Destroy (liveMoveSelector);
				}
				playerMover.destination = destination;
		}

		void targetEnemy ()
		{

				if (liveReticle != null) {
						Destroy (liveReticle);
				}
				playerGun.target = GameObject.FindWithTag ("Enemy");
		}

		void SpawnReticle (Vector2 spawnLocation)
		{
				//If previous reticle exists, destroy it
				if (liveReticle != null) {
						Destroy (liveReticle);
				}

				//Spawn new reticle at spawnLocation
				liveReticle = (GameObject)Instantiate (reticlePrefab, spawnLocation, Quaternion.identity);

				//Aim at the new reticle
				playerGun.target = liveReticle;
		}
}
