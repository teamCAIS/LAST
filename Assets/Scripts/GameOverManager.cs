using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{

  public void goToLobby()
  {
    SceneManager.LoadScene("MainScene");
  }
}