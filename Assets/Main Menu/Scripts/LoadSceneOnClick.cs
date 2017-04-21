using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadSceneOnClick : MonoBehaviour {

	public void loadScenebyIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void loadScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
