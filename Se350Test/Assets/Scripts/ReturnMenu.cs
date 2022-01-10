using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnMenu : MonoBehaviour
{
   public void Return()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2 );
   }
}

