using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	public int startingHealth = 100;

	[SyncVar]
	public int currentHealth = 100;
	public Text healthText;
	//public Slider healthSlider;

	float shakingTimer = 0;
	public float timeToShake = 1.0f;
	public float shakeIntensity = 3.0f;
	bool isShaking = false;

	public bool isDead = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		//healthSlider.value = startingHealth;
	}

	// Update is called once per frame
	void Update () {

		SetHealthText();
		healthText.text = "HP: " + currentHealth.ToString ();
		//healthText.text = "HP:";
		//healthSlider.value = currentHealth;

		

		if (isDead)
        {
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Death();
			}
		}
		

		

	}


	public void TakeDamage(int amount){
		if (!isServer)
			return;

		if (isDead)
			return;

		//ShakeCamera ();

		currentHealth -= amount;
		
	}

	public void Death(){
		if (isDead)
			return;

        //anim.SetTrigger ("Death");
        //agent.enabled = false;
        //isDead = true;
        //currentHealth = 0;
        //Destroy (gameObject, 2.5f);

        if (isServer)
        {
			RpcDeath();
        }
        else
        {
			Destroy(gameObject, 2.5f);
			isDead = true;
		}
	}

	[ClientRpc]
	public void RpcDeath()
    {
		if (isDead)
			return;

		Destroy(gameObject, 2.5f);
		isDead = true;
	}

	//void ShakeCamera(){
	//	shakingTimer = 0;
	//	isShaking = true;
		
	//}

	void SetHealthText()
    {
        if (isLocalPlayer)
        {
			healthText.text = "HP: " + currentHealth.ToString();
        }
    }

}
