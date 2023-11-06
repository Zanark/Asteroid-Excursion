using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { set; get; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int currentLevel = 0;
    public int menuFocus = 0;

}
