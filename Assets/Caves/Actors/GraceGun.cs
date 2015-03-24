using UnityEngine;
using System.Collections;

[RequireComponent(typeof (GraceShip))]
public class GraceGun : MonoBehaviour {

	public GameObject projectilePrefab;

	public void Shoot() {
		GameObject clone = (GameObject)(Instantiate (projectilePrefab, transform.position, transform.rotation));
		clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(2000 * Mathf.Sign(transform.localScale.x), 0), ForceMode2D.Force);
	}

}
