using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        //ResetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize(state));
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize(state));
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one!");
        }
    }

    public bool isColorOwned(int index)
    {
        if(index <= 0)
        {
            return true;
        }
        else
        {
            return (state.colorOwned & (1 << index)) != 0;
        }
    }

    public bool isTrailOwned(int index)
    {
        if(index <= 0)
        {
            return true;
        }
        else
        {
            return (state.trailOwned & (1 << index)) != 0;
        }
    }

    public void UnlockColor(int index)
    {
        state.colorOwned |= 1 << index;
    }

    public void UnlockTrail(int index)
    {
        state.trailOwned |= 1 << index;
    }

    public bool BuyColor(int index, int cost)
    {
        if(state.currency >= cost)
        {
            state.currency -= cost;
            UnlockColor(index);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }   

    public bool BuyTrail(int index, int cost)
    {
        if (state.currency >= cost)
        {
            state.currency -= cost;
            UnlockTrail(index);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}