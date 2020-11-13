using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _remainingText;

    Object[] _objects;

    void OnEnable()
    {
        _objects = FindObjectsOfType<Object>();
    }

    void Update()
    {
        if (ObjectsAreAllGone())
            GoToNextLevel();
        _remainingText.text = "Remaining: " + _objects.Length;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
