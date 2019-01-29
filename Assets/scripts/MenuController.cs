using UnityEngine.SceneManagement;

using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public State state;
    void Start()
    {
        state.currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quit");

            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("cutscene");

        }
    }
}
