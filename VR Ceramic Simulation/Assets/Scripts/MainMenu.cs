using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Home()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("newMainMenu"); 
    }

    public void PlayGame()
    {
        // get the the current scene displayed using GetActiveScene and + 1 to go the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //go to a specific scene "the_scene_name"
        //SceneManager.LoadScene("Main Menu"); 
    }

    public void ShapingScene ()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("01 Shaping"); 
    }

    public void GlazeScene ()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("02 Glazing"); 
    }

    public void FiringScene()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("03 Firing"); 
    }


    public void ShapingSceneS ()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("01 Shaping S"); 
    }

    public void GlazeSceneS ()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("02 Glazing S"); 
    }

    public void FiringSceneS()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("03 Firing S"); 
    }

    public void freeForm()
    {
        //go to a specific scene "the_scene_name"
        SceneManager.LoadScene("FREEFORM MODE"); 
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
