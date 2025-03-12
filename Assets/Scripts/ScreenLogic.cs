using UnityEngine;

public class ScreenLogic : MonoBehaviour
{
    public GameObject LoginObject;
    public GameObject RegisterObject;
    public GameObject MainObject;
    public GameObject EnvObject;

    public void loginActive()
    {
        LoginObject.SetActive(true);
        RegisterObject.SetActive(false);
        MainObject.SetActive(false);
        EnvObject.SetActive(false);
    }
    public void registerActive()
    {
        LoginObject.SetActive(false);
        RegisterObject.SetActive(true);
        MainObject.SetActive(false);
        EnvObject.SetActive(false);
    }
    public void gameActive()
    {
        LoginObject.SetActive(false);
        RegisterObject.SetActive(false);
        MainObject.SetActive(true);
        EnvObject.SetActive(false);
    }
    public void envActive()
    {
        LoginObject.SetActive(false);
        RegisterObject.SetActive(false);
        MainObject.SetActive(false);
        EnvObject.SetActive(true);
    }

}
