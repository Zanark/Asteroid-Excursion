using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { set; get; }

    public Material playerMaterial;
    public Color[] playerColor = new Color[10];
    public GameObject[] playerTrails = new GameObject[10];

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int currentLevel = 0;
    public int menuFocus = 0;

}
