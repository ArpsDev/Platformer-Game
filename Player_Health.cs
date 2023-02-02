using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
	private Rigidbody2D rb;

	public int maxHealth = 4;
	public int currentHealth;

	public Health_Bar healthBar;

	[Header("Knockback")]
	[SerializeField] private float hurtforce = 10f;

	Animator Anim;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (currentHealth <= 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			Collectables.theScore = 0;
		}
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		//Debug.Log("Hit");

		if (other.tag == "Enemy")
        {
			TakeDamage(1);

			if (other.gameObject.transform.position.x > transform.position.x)
			{
				rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
			}
			else
			{
				rb.velocity = new Vector2(hurtforce, rb.velocity.y);
			}
		}
   
	    if (other.tag == "Death")
        {
			TakeDamage(5);
        }
	}
}
