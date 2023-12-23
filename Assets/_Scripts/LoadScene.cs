using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string m_sceneName;
    public void LoadSceneByName() => SceneManager.LoadScene(m_sceneName);
}
