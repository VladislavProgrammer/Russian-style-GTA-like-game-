using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public int LevelIndex;

    private void Start()
    {
        Debug.Log(LevelIndex);

        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    
}
