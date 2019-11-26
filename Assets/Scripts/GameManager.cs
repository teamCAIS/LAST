using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool gameOver = false;
    private bool gameStarted = false;
    
    void Awake ()
	{
        Debug.Log("Game manager awake");
		if (instance != null)
		{
			Debug.LogError("More than one GameManager in scene.");
		} else
		{
			instance = this;
		}
	}

    private const string PLAYER_ID_PREFIX = "Player ";

    private Dictionary<string, Player> players = new Dictionary<string, Player>();

    public void RegisterPlayer (string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        instance.players.Add(_playerID, _player);

        //add second player as assassin
        if(instance.players.Count == 2) 
        {
            _player.becomeAssassin();
        }

        _player.transform.name = _playerID;

        if(instance.players.Count > 2)
        {
            gameStarted = true;
        }
    }

    public void UnRegisterPlayer (string _playerID)
    {
        instance.players.Remove(_playerID);
        if(instance.players.Count <= 2)
        {
            gameOver = true;
        }
    }

    public static Player GetPlayer (string _playerID)
    {
        return instance.players[_playerID];
    }

    /* public void shouldRestart()
    {
        if(instance.players.Count)
    } */

    void Update()
    {
        if(gameStarted && gameOver)
        {
            instance.players = new Dictionary<string, Player>();
            foreach (var player in players)
            {
                Destroy(player.Value);
            }
            gameOver = false;
            gameStarted = false;
        }

        
    }

}
