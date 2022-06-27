using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public string sceneName;

   public void ChangeS()
   {
       SceneManager.LoadScene(sceneName);
   }

   public void Quit()
   {
       Application.Quit();
   }
}
