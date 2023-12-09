using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    //Awake
    public NetworkRunner SessionRunner { get; private set; }

    [SerializeField]
    private GameObject _runnerPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void CreataRunner()
    {
        SessionRunner = Instantiate(_runnerPrefab, transform).GetComponent<NetworkRunner>();
    }

    private async Task connect()
    {
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "TestSession",
            SceneManager = GetComponent <NetworkSceneManagerDefault>()
        };

        var result = await SessionRunner.StartGame(args);

        if (result.Ok)
        {
            Debug.Log("StartGame Succesfull");
        }
        else
        {
            Debug.LogError(result.ErrorMessage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreataRunner();
    }
    void Update()
    {
        
    }
}