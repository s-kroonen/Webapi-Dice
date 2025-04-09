using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnvScreenLogic : MonoBehaviour
{
    [Header("Env2D")]
    public List<Button> envButtons;
    public Button createEnv;
    public TMP_InputField envNameInput;
    public  List<Environment2D> environment2Ds;
    public string currentEnvId;
    [Header("Dependencies")]
    public DiceApp diceApp;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        Debug.Log($"environment count{environment2Ds.Count}");
        for (int i = 0; i < envButtons.Count; i++)
        {
            if (i < environment2Ds.Count)
            {
                envButtons[i].gameObject.SetActive(true);
                envButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = environment2Ds[i].name;
                int index = i;
                envButtons[i].onClick.RemoveAllListeners();
                envButtons[i].onClick.AddListener(() => OnEnvironmentButtonClicked(environment2Ds[index]));
            }
            else
            {
                envButtons[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < envButtons.Count; i++)
        {
            if (i < environment2Ds.Count)
            {
                envButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                int index = i;
                envButtons[i].transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                envButtons[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnDeleteEnvironmentButtonClicked(environment2Ds[index]));
            }
            else
            {
                envButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        if (environment2Ds.Count >= envButtons.Count)
        {
            createEnv.gameObject.SetActive(false);
        }
        else
        {
            createEnv.gameObject.SetActive(true);
        }
    }

    private void OnEnvironmentButtonClicked(Environment2D environment)
    {
        Debug.Log("Environment button clicked: " + environment.name);
        currentEnvId = environment.id;
        diceApp.ReadObject2Ds();
    }

    private void OnDeleteEnvironmentButtonClicked(Environment2D environment)
    {
        Debug.Log("Environment button clicked: " + environment.name);
        diceApp.DeleteEnvironment2D(environment.id);
    }
    public void OnCreateEnvironmentButtonClicked()
    {
        Debug.Log("Environment create button clicked: " + envNameInput.text);
        diceApp.CreateEnvironment2D(envNameInput.text);
    }
}
