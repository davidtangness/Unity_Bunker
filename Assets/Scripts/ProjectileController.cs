using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{



		public float maxSpinFactor;

		// Use this for initialization
		void Start ()
		{

				float randomX = Random.Range (-1, 1) * maxSpinFactor;
				float randomY = Random.Range (-1, 1) * maxSpinFactor;
				rigidbody2D.AddRelativeForce (new Vector2 (randomX, randomY));
		}
	
		void OnTriggerExit2D (Collider2D other)
		{
				if (other.tag == "Ground") {
						Destroy (gameObject);
				}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Bunker") {
						Destroy (gameObject);
				}

				if (other.tag == "Enemy") {
					Destroy (other.gameObject);
					//TODO: End game, win.
				}

		}
}


