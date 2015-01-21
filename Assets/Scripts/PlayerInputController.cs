using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{

		//private GameObject Player;

		// Use this for initialization
		void Start ()
		{

		}

		void Update ()
		{

				if (Input.GetMouseButtonDown (0)) {
					Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						
					//Either get all hits and try to find the topmost with some sort of priority
					//Or use layermasks and successive calls to OverlapPoint, again ordered by some priority
					//Order should be Enemy -> Bunker -> Ground
					Collider2D[] hitCollider = Physics2D.OverlapPointAll (mousePosition);			
					Debug.Log (hitCollider.Length);


					//Debug.Log ("Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " + hitCollider.transform.position.y);    
					//If tag == Bunker, set destination to bunker position
					//else if tag == ground, spawn a location icon and set target = that
					//else if tag == Enemy despawn AND target is not enemy, despawn the location icon and set target = enemy
					//else do nothing
			
					
				}
		}
}
