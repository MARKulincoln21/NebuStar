using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    // Start is called before the first frame update

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void NextLevel() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
