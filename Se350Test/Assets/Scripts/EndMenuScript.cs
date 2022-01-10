using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuScript : MonoBehaviour
{
    public Text DiamondsCollected;
    public static string ResultText;
    private void Update()
    {
    DiamondsCollected.text = "Diamonds Collected:"+ResultText;
    }

   public void Retry()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1 );
   }
}
