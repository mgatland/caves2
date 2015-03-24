using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GraceShip))]
public class GraceUserControl : MonoBehaviour {
	
	private GraceShip m_Character;
	private bool m_Shoot;
	
	
	private void Awake()
	{
		m_Character = GetComponent<GraceShip>();
	}
	
	private void Update()
	{
		if (!m_Shoot)
		{
			// Read the shoot input in Update so button presses aren't missed.
			m_Shoot = CrossPlatformInputManager.GetButtonDown("Fire1");
		}
	}
	
	
	private void FixedUpdate()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		m_Character.Move(h, v, m_Shoot);
		m_Shoot = false;
	}

}
