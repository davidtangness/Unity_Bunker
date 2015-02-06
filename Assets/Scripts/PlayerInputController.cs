using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{

		public GameObject reticlePrefab;
		public GameObject moveSelectorPrefab;
		public int[] prioritizedLayers;
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

				/*

				//First try. It works but it's unreadable.


				if (Input.GetMouseButtonDown (0)) {
						Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						//Check layer 13: move selector
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
												targetReticle();
										}		
								}
						}
				}
	*/



				//Second try, more readable but still duplicating more code than I'd like.

				/*
		 * if (Input.GetMouseButtonDown (0)) {
						LayerMask mask = 0;
						Collider2D hitCollider = null;


						//Check moveSelector
						mask = 1 << 13;
						hitCollider = Physics2D.OverlapPoint (mousePosition, mask);
						if (hitCollider != null) {
								selectMove (mousePosition);
								return;
						}

						//Check enemies
						mask = 1 << 9;
						hitCollider = Physics2D.OverlapPoint (mousePosition, mask);
						if (hitCollider != null) {
								targetEnemy ();
								return;
						}

						//Check bunkers
						mask = 1 << 10;
						hitCollider = Physics2D.OverlapPoint (mousePosition, mask);
						if (hitCollider != null) {
								spawnMoveSelector (hitCollider.transform.position);
								return;
						}

						//Check ground
						mask = 1 << 14;
						hitCollider = Physics2D.OverlapPoint (mousePosition, mask);
						if (hitCollider != null) {
								SpawnReticle (mousePosition);
								targetReticle ();
								return;
						}
			}
		*/


				//Third try, moved the duplicate code into its own function. Much happier with this solution.
				//I might be able to improve it by using Unity's layer name/number enumeration, so that I could pass in names and convert names -> numbers instead of having to pass in numbers.

				if (Input.GetMouseButtonDown (0)) {
					
						Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						Collider2D hitCollider = findFirstCollider (mousePosition, prioritizedLayers);

						if (hitCollider != null) {
								switch (hitCollider.tag) {

								case "MoveSelector":
										selectMove (mousePosition);
										break;

								case "Enemy":
										targetEnemy ();
										break;

								case "Bunker":
										spawnMoveSelector (hitCollider.transform.position);
										break;

								case "Ground":
										SpawnReticle (mousePosition);
										targetReticle ();
										break;
								}
						}
				}
		}

		Collider2D findFirstCollider (Vector2 mousePosition, int[] prioritizedLayers)
		{
				foreach (int layer in prioritizedLayers) {
						LayerMask mask = 1 << layer;
						Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition, mask);
						if (hitCollider != null) {
								return hitCollider;
						}
				}
				return null;
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
		}

		void targetReticle ()
		{
				if (liveReticle != null) {
						playerGun.target = liveReticle;
				} else {
						Debug.LogError ("ERROR: Tried to target reticle when no reticle exists");
				}
		}




		
		



}
