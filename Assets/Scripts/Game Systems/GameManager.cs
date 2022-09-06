using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static bool _firstLoadComplete;
    [SerializeField] private Button _nextSceneButton;
    [SerializeField] private TextMeshProUGUI _fpsCounter;
    [SerializeField] private int[] frameRates = { 30, 60, 120 };

    // Start is called before the first frame update
    void Start()
    {
        if (!_firstLoadComplete)
        {
            QualitySettings.vSyncCount = 0;
            SetFrameRate(frameRates[0]);
            FPS.MaxFrames = frameRates[0];
            _firstLoadComplete = true;
        }
        _nextSceneButton.interactable = false;
    }

    private void Update()
    {
        _fpsCounter.text = "FPS: " + Mathf.Round(FPS.GetCurrentFPS()).ToString();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void NextScene()
    {
        if (SceneManager.GetSceneByBuildIndex(gameObject.scene.buildIndex + 1) != null)
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }

    public void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void EnableNextScene()
    {
        if (SceneManager.GetSceneByBuildIndex(gameObject.scene.buildIndex + 1) != null)
            _nextSceneButton.interactable = true;
    }

    public void GetFrameRateFromDropdown(int selection)
    {
        SetFrameRate(frameRates[selection]);
    }

    public void SetFrameRate(int rate)
    {
        Application.targetFrameRate = rate;
        FPS.MaxFrames = rate;
    }

    public static bool ValidateCollisionWithPlayer(Collision collision, out Ragdoll ragdoll)
    {
        ragdoll = collision.gameObject.GetComponentInParent<Ragdoll>();
        return ragdoll != null && !ragdoll.Impacted;
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
