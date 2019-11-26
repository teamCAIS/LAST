using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

	public GameObject impact;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	void Start ()
	{
		if (cam == null)
		{
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}
	}

	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	[Client]
	void Shoot ()
	{
		if(!isLocalPlayer) return;

		CmdOnShoot ();
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask) )
		{

			//Quando jogador for morto, gameManager precisa ser notificado
			// recebendo o jogador e retirando-o da lista de ativos
			// quando sobrar 2 jogadores, o jogo acaba
      //CmdPlayerShot(_hit.collider.name);
			if (_hit.collider.tag == PLAYER_TAG)
			{
				CmdPlayerShot(_hit.collider.name);
			}

			CmdOnHit(_hit.point, _hit.normal);
		}
	}

	[Command]
	void CmdOnShoot ()
	{
		RpcDoShootEffect();
    }

	//Is called on all clients when we need to to
	// a shoot effect
	[ClientRpc]
	void RpcDoShootEffect ()
	{
		weapon.flash.Play();
	}

	//Is called on the server when we hit something
	//Takes in the hit point and the normal of the surface
	[Command]
	void CmdOnHit (Vector3 _pos, Vector3 _normal)
	{
		RpcDoHitEffect(_pos, _normal);
    }

	//Is called on all clients
	//Here we can spawn in cool effects
	[ClientRpc]
	void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
	{
		GameObject impactGO = Instantiate(impact, _pos, Quaternion.LookRotation(_normal));
		Destroy(impactGO, 2.0f);
	}

	/*[Command]
	 void CmdPlayerShot (string _playerID, int _damage, string _sourceID)
	{
		Debug.Log(_playerID + " has been shot.");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, _sourceID);
	} */

 	[Command]
	void CmdPlayerShot (string _ID)
	{
		Debug.Log(_ID + " has been shot.");
		Player _player = GameManager.GetPlayer(_ID);
		_player.RpcDie();
    //GameObject.Find(_ID).SetActive(false);
	} 

}