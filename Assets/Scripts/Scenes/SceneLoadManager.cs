using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public int idScene;
    //public string sceneName;
    public float timeToChangeScene;
    public Animator loadScene;
   
    private void Start()
    {
        Apear();        
    }
    //public void LoadScene(string name)
    //{
    //    sceneName = name;
    //    SceneManager.LoadScene(name);
    //}
    public void LoadScene(int id)
    {
        idScene = id;
        SceneManager.LoadScene(idScene);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(idScene);
    }
    public void ChangeScene()
    {
        StartCoroutine(InFade());
    }
    private IEnumerator InFade()
    {
        While();
        Disapear();
        yield return new WaitForSecondsRealtime(timeToChangeScene);     
        LoadScene();
    }


    void Apear()
    {
        loadScene.SetTrigger("Apear");
    }
    void Disapear()
    {
        loadScene.SetTrigger("Disapear");
    }
    void While()
    {
        loadScene.SetTrigger("While");
    }
}
