using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Object[] _objects;

    void OnEnable()
    {
        _objects = FindObjectsOfType<Object>();
    }

    void Update()
    {
        if (ObjectsAreAllGone())
            GoToNextLevel();
    }

    private bool ObjectsAreAllGone()
    {
        _objects = FindObjectsOfType<Object>();
        if (_objects.Length > 0)
            return false;
        return true;
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
