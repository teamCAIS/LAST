using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
  [SerializeField]
  Behaviour[] componentsToDisable;
  [SerializeField]
	string remoteLayerName = "RemotePlayer";

  [SerializeField]
  GameObject gun;

  [SerializeField]
  Behaviour shootScript;

  Camera sceneCamera;

  void Start()
  {
    Debug.Log("Player setup started");
    if (!isLocalPlayer)
    {
      DisableComponents();
			AssignRemoteLayer();
    } else 
    {
      sceneCamera = Camera.main;
      if(sceneCamera != null)
      {
        sceneCamera.gameObject.SetActive(false);
      }
      
    }
    
    //RegisterPlayer ();

  }

  public override void OnStartClient()
    {
      Debug.Log("Client started");
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        transform.name = _netID;
        Player _player = GetComponent<Player>();
        _player.setName(_netID);

        GameManager.instance.RegisterPlayer(_netID, _player);

        if(!_player.isAssassin)
        {
          //desabilitar arma
          gun.SetActive(false);
          shootScript.enabled = false;
        }
    }

  public override void OnDeserialize(NetworkReader reader, bool initialState)
  {
    Debug.Log("OnNetworkDestroy");
  }

  /* void RegisterPlayer ()
	{
		string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
		transform.name = _ID;
	} */

	void AssignRemoteLayer ()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void DisableComponents ()
	{
		for (int i = 0; i < componentsToDisable.Length; i++)
		{
			componentsToDisable[i].enabled = false;
		}
	}

  void OnDisable()
  {
    //GameManager.instance.shouldRestart();
    Player _player = GetComponent<Player>();
    GameManager.instance.UnRegisterPlayer(_player.playerName);
    if(sceneCamera != null)
    {
      sceneCamera.gameObject.SetActive(true);
    }
  }

}
