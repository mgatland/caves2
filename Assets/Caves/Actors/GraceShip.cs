using UnityEngine;
using System.Collections;

public class GraceShip : MonoBehaviour {

	private Rigidbody2D m_Rigidbody2D;
	private static float m_MaxSpeed = 300f;
	private bool m_FacingRight = true;
	private GraceGun gun;
	
	void Awake () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		gun = GetComponent<GraceGun>();
	}

	public void Move(float moveX, float moveY, bool shoot)
	{			
		// Move the character
		m_Rigidbody2D.velocity = new Vector2(moveX*m_MaxSpeed, moveY*m_MaxSpeed);
		
		// If the input is moving the player right and the player is facing left...
		if (moveX > 0 && !m_FacingRight)
		{
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveX < 0 && m_FacingRight)
		{
			Flip();
		}

		if (shoot) {
			gun.Shoot();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
