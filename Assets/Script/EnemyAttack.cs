﻿using UnityEngine;
using System.Collections;
using StarterAssets;
using UnityEngine.Networking;

public class EnemyAttack : NetworkBehaviour {

	public int attackDamage = 60;
	public float timeBetweenAttacks = 0.5f;
	// todo: grab the player's health script
	bool playerInRange = false;
	float timer;


	Animator anim;
	GameObject player;
	//EnemyHealth enemyHealth;
	PlayerHealth playerHealth;
	Animator playerAnim;
	bool isEnabled = true;
	UnityEngine.AI.NavMeshAgent agent;
	//PlayerShoot playerShoot;
   ThirdPersonController playerMovement;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		//enemyHealth = GetComponent<EnemyHealth> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		//playerShoot = player.GetComponent<PlayerShoot> ();
		playerMovement = player.GetComponent<ThirdPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isEnabled == false)
			return;
		
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange == true ) {
			Attack ();
		}

		if (playerHealth.currentHealth <= 0) {
			playerHealth.Death();

			isEnabled = false;
			anim.enabled = false;
			//anim.SetTrigger ("idle");
			agent.enabled = false;
			//playerShoot.DisableShooting ();
			//playerMovement.DisableMovement ();
		}

	
	}

	// whenever we get close to the player, we can attack
	void OnTriggerEnter(Collider other){
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}


	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}


	void Attack(){
		timer = 0f;
		anim.SetTrigger ("Attack");
		if (playerHealth.currentHealth > 0) {
			//playerHealth.TakeDamage (attackDamage);
			CmdTellServerWhoGotShot(player.name, attackDamage);
		}
	}

	[Command]
	void CmdTellServerWhoGotShot(string uniqueID, int damage)
	{
		GameObject obj = GameObject.Find(uniqueID);
		obj.GetComponent<PlayerHealth>().TakeDamage(damage);
	}
}
