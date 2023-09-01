using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject playerObject;
    PlayerStats playerStats;
    GameObject directionalLight;
    Light dl;
    GameObject pollutionSlider;
    GameObject[] trashCanIcons;
    GameObject forestAreaIcon;
    GameObject sowArea1Icon;
    GameObject sowArea2Icon;
    GameObject credits;
    GameObject canvasUI;
    public Slider slider;

    [SerializeField] public AudioSource ambient;
    [SerializeField] public AudioSource notifications;
    [SerializeField] public Material day;
    [SerializeField] public Material night;
    [SerializeField] private TextMeshProUGUI trashScore;
    [SerializeField] private TextMeshProUGUI pollutionScore;
    [SerializeField] private TextMeshProUGUI MissionText;
    [SerializeField] private TextMeshProUGUI ScoreGoal;
    public int score = 5;

    public bool retoFinalMissions;
    public bool onPollution;
    public bool canReforest;
    public bool canTakeOldTrees;
    public int treesCollected;
    public int treesSown;
    public bool canSow;
    public bool trashDrop;

    private void Awake()
    {
        directionalLight = GameObject.Find("Directional Light");
        dl = directionalLight.GetComponent<Light>();
        playerObject = GameObject.Find("Player");
        playerStats = playerObject.GetComponent<PlayerStats>();
        pollutionSlider = GameObject.Find("Slider");
        forestAreaIcon = GameObject.Find("ForestAreaText");
        sowArea1Icon = GameObject.Find("SowArea1");
        sowArea2Icon = GameObject.Find("SowArea2");
        credits = GameObject.Find("CreditsCanvas");
        trashCanIcons = GameObject.FindGameObjectsWithTag("TrashCanIcon");
        slider = pollutionSlider.GetComponent<Slider>();
        //canvasUI = GameObject.Find("Canvas");
    }

    private void Start()
    {
        Cursor.visible = false;
        onPollution = true;
        treesCollected = 0;
    }

    private void Update()
    {
        if (onPollution)
        {
            OnPollution();
        }
        else
        {
            CleanPollution();
        }

        pollutionScore.text = slider.value.ToString();
        trashScore.text = score.ToString();

        if (slider.value == 100 && score == 5)
        {
            foreach (GameObject icon in trashCanIcons)
            {
                icon.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        if (slider.value == 0 )
        {
            if (trashDrop)
            {
                trashDrop = false;
                TrashDrop();
            }
        }
        else
        {
            canReforest = false;
        }

        if (treesCollected == 9)
        {
            canSow = true;
            MissionText.text = "Debes sembrar 2 árboles para reforestar la zona afectada.";
            ScoreGoal.text = "/2";
            forestAreaIcon.GetComponent<MeshRenderer>().enabled = false;
            sowArea1Icon.GetComponent<MeshRenderer>().enabled = true;
            sowArea2Icon.GetComponent<MeshRenderer>().enabled = true;
        }

        if (treesSown == 2)
        {
            ScoreGoal.text = "";
            trashScore.text = "";
            MissionText.text = "!Nuestro parque esta libre de contaminación!";
            sowArea1Icon.GetComponent<MeshRenderer>().enabled = false;
            sowArea2Icon.GetComponent<MeshRenderer>().enabled = false;
            //credits.GetComponent<Canvas>().enabled = true;
            //canvasUI.GetComponent<Canvas>().enabled = false;
            retoFinalMissions = true;
            RenderSettings.skybox = night;
            dl.intensity = 0.25f;
        }
    }

    private void TrashDrop()
    {
        notifications.Play();
        canReforest = true;
        canTakeOldTrees = true;
        MissionText.text = "Ve a la zona de reforestación y tala los arboles muertos.";
        ScoreGoal.text = "/9";
        trashCanIcons = GameObject.FindGameObjectsWithTag("TrashCanIcon");
        foreach (GameObject icon in trashCanIcons)
        {
            icon.GetComponent<MeshRenderer>().enabled = false;
        }
        forestAreaIcon.GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnPollution()
    {
        if (slider.value <= 99.9)//Increase Slider
        {
            slider.value += 0.1f;
        }
        
        if (RenderSettings.fogDensity < 0.01f)//Increase Fog
        {
            RenderSettings.fogDensity += 0.0001f;
        }

        playerStats.TakeDamage();
    }

    public void CleanPollution()
    {
        if (slider.value > 0) //Decrease Slider
        {
            slider.value -= 0.1f;
        }
        else if(slider.value == 0)
        {
            if (RenderSettings.fogDensity > 0)//Decrease Fog
            {
                RenderSettings.fogDensity -= 0.0001f;
            }
        }
        playerStats.LifeRestore();
    }
}
