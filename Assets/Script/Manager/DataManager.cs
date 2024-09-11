using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

[System.Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
    public int mapScene;
}
public class DataManager : MonoBehaviour
{
    public GameObject Player;
    public int map;
    private static string saveFilePath;

    private static DataManager instance;
    private static readonly object _lock = new object();

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<DataManager>();
                        if (instance == null)
                        {
                            // Singleton instance가 존재하지 않을 때의 처리
                        }
                        else
                        {
                            DontDestroyOnLoad(instance.gameObject);
                        }
                    }
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Plaaaa()
    {
        Player = GameObject.Find("Player");
        if (Player != null)
        {
            Debug.Log("오브젝트가 성공적으로 할당되었습니다.");
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void GameSave()
    {
        if (Player == null)
        {
            Plaaaa();
        }

        SaveData data = new SaveData
        {
            playerX = Player.transform.position.x,
            playerY = Player.transform.position.y,
            mapScene = map
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("게임이 저장되었습니다: " + saveFilePath);
    }

    public void GameLoad()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("저장된 파일이 없습니다.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        StartCoroutine(LoadInitialSceneAndThenMoveToSavedScene(data.mapScene, data.playerX, data.playerY));
    }

    private IEnumerator LoadInitialSceneAndThenMoveToSavedScene(int savedMap, float playerX, float playerY)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("1.part0");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(LoadSceneAndSetPlayerPosition(savedMap, playerX, playerY));
    }

    private IEnumerator LoadSceneAndSetPlayerPosition(int map, float x, float y)
    {
        AsyncOperation asyncLoad;

        switch (map)
        {
            case 1:
                asyncLoad = SceneManager.LoadSceneAsync("1.part0");
                break;
            case 2:
                asyncLoad = SceneManager.LoadSceneAsync("2.part1");
                break;
            case 3:
                asyncLoad = SceneManager.LoadSceneAsync("4.part3");
                break;
            case 4:
                asyncLoad = SceneManager.LoadSceneAsync("5.part4");
                break;
            case 5:
                asyncLoad = SceneManager.LoadSceneAsync("6.part5");
                break;
            case 6:
                asyncLoad = SceneManager.LoadSceneAsync("7.part6");
                break;
            default:
                Debug.LogWarning("잘못된 맵 번호입니다.");
                yield break;
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Plaaaa();
        if (Player != null)
        {
            Player.transform.position = new Vector3(x, y, 0);
            Debug.Log($"플레이어 위치가 ({x}, {y})로 이동되었습니다.");
        }
    }

    public void NewGame()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("이전 저장 데이터가 삭제되었습니다.");
        }

        SceneManager.LoadScene("InitialScene");
    }
}

