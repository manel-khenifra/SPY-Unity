﻿using UnityEngine;
using FYFY;
using System.Collections;
using FYFY_plugins.TriggerManager;
using System;

/// <summary>
/// Manage collision between player agents and Coins
/// </summary>
public class CoinManager : FSystem {
    private Family robotcollision_f = FamilyManager.getFamily(new AllOfComponents(typeof(Triggered3D)), new AnyOfTags("Player"));
	private GameData gameData;
    private bool activeCoin;

	public CoinManager(){
		if (Application.isPlaying)
		{
			activeCoin = true;
			GameObject go = GameObject.Find("GameData");
			if (go != null)
				gameData = go.GetComponent<GameData>();
			robotcollision_f.addEntryCallback(onNewCollision);
		}
    }

    private void onNewCollision(GameObject robot){
		if(activeCoin){
			Triggered3D trigger = robot.GetComponent<Triggered3D>();
			foreach(GameObject target in trigger.Targets){
				//Check if the player collide with a coin
                if(target.CompareTag("Coin")){
                    gameData.totalCoin++;
                    target.GetComponent<AudioSource>().Play();
                    MainLoop.instance.StartCoroutine(coinDestroy(target));
					Debug.Log("Une pièce a été ramassée!");
				}
			}			
		}
    }

	// See ExecuteButton, StopButton and ReloadState buttons in editor
	public void detectCollision(bool on){
		activeCoin = on;
	}

	private IEnumerator coinDestroy(GameObject go){
		go.GetComponent<ParticleSystem>().Play();
		go.GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(1f); // let time for animation
		GameObjectManager.setGameObjectState(go, false); // then disabling GameObject
	}
}