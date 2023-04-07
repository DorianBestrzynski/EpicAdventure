using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NextCutscene : MonoBehaviour
{
    public PlayableDirector director;

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
        Debug.Log("OnEnable");
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            if (StaticVariables.is2Levels && SceneManager.GetActiveScene().buildIndex == 8)
            {
                SceneManager.LoadScene(11);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }

        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        director.stopped -= OnPlayableDirectorStopped;
    }
}