﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public bool isDead = false;
    public bool isAssassin = false;

    public string playerName = "Player ";

    [SerializeField]
    private GameObject playerGO;

    public void setName(string name)
    {
        playerName += name;
    }

    [SerializeField]
	private Behaviour[] disableOnDeath;

    public void becomeAssassin()
    {
        isAssassin = true;
    }


    [ClientRpc]
    public void RpcDie()

    {
        if(isAssassin)
            return;
        
        isDead = true;
        GameManager.instance.UnRegisterPlayer(playerName);

        Destroy(playerGO);

        //disable components
        /* for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = false;
		}

        Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = false; */

        //mensagem Game Over

    }
   
}
