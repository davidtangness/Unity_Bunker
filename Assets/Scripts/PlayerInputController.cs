using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{

		private CharacterMovementController playerMover;
		// Use this for initialization
		void Start ()
		{
				playerMover = (CharacterMovementController)gameObject.GetComponent ("CharacterMovementController");
				Debug.Log (playerMover.destination);
		}

		void Update ()
		{

				if (Input.GetMouseButtonDown (0)) {
						Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						
						//First check if we clicked an enemy
						//check layer 9: enemies
						LayerMask mask = 1 << 9;
						Collider2D[] hitCollider = Physics2D.OverlapPointAll (mousePosition, mask);
						if (hitCollider.Length == 1) {
								//TODO: Despawn old reticle, set target = enemy.
								Debug.Log ("Clicked Enemy");
						} else {
								//If we didn't click an enemy, check if we clicked a bunker
								//Check layer 10: bunkers
								mask = 1 << 10;
								hitCollider = Physics2D.OverlapPointAll (mousePosition, mask);
								if (hitCollider.Length == 1) {
										//TODO: Spawn move-selector around bunker, check for subsequent click, or maybe click-and-drag?
										playerMover.destination = mousePosition;//hitCollider [0].transform.position;	
										Debug.Log ("Clicked Bunker");
								} else {
										//If we didn't click an enemy or a bunker, we must have clicked the ground
										//TODO: Despawn old reticle, spawn new reticle at mouseposition, set target = reticle
										Debug.Log ("Clicked Ground");
								}		
						}
				}
				
		}
}
