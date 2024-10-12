using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CropManager : MonoBehaviour
{
    // Wheat Buttons
    public Button plantWheatButton;
    public Button waterWheatButton;
    public Button fertilizeWheatButton;
    public Button harvestWheatButton;

    // Corn Buttons
    public Button plantCornButton;
    public Button waterCornButton;
    public Button fertilizeCornButton;
    public Button harvestCornButton;

    public TextMeshProUGUI wheatInfoText;
    public TextMeshProUGUI cornInfoText;
    public TextMeshProUGUI harvestedText;
    public GameObject wheatObject;
    public GameObject cornObject;

    public Material[] wheatMaterials;
    public Material[] cornMaterials;

    private Wheat wheatCrop;
    private Corn cornCrop;

    private void Start()
    {
        wheatCrop = new Wheat(wheatMaterials);
        cornCrop = new Corn(cornMaterials);

        // Wheat Button Listeners
        plantWheatButton.onClick.AddListener(() => PlantCrop(wheatCrop, wheatObject, wheatInfoText));
        waterWheatButton.onClick.AddListener(() => WaterCrop(wheatCrop, wheatObject, wheatInfoText));
        harvestWheatButton.onClick.AddListener(() => HarvestCrop(wheatCrop, wheatObject, wheatInfoText));
        fertilizeWheatButton.onClick.AddListener(() => FertilizeCrop(wheatCrop, wheatObject, wheatInfoText));

        // Corn Button Listeners
        plantCornButton.onClick.AddListener(() => PlantCrop(cornCrop, cornObject, cornInfoText));
        waterCornButton.onClick.AddListener(() => WaterCrop(cornCrop, cornObject, cornInfoText));
        harvestCornButton.onClick.AddListener(() => HarvestCrop(cornCrop, cornObject, cornInfoText));
        fertilizeCornButton.onClick.AddListener(() => FertilizeCrop(cornCrop, cornObject, cornInfoText));
    }

    private void Update()
    {
        wheatCrop.Grow(Time.deltaTime);
        cornCrop.Grow(Time.deltaTime);

        UpdateCropInfo(wheatCrop, wheatObject, wheatInfoText);
        UpdateCropInfo(cornCrop, cornObject, cornInfoText);
    }

    private void WaterCrop(Crop crop, GameObject cropObject, TextMeshProUGUI infoText)
    {
        crop.Water();
        UpdateCropInfo(crop, cropObject, infoText);
    }

    private void FertilizeCrop(Crop crop, GameObject cropObject, TextMeshProUGUI infoText)
    {
        crop.Fertilize();
        UpdateCropInfo(crop, cropObject, infoText);
    }

    private void HarvestCrop(Crop crop, GameObject cropObject, TextMeshProUGUI infoText)
    {
        float yield = crop.Harvest();
        if (yield > 0)
        {
            Debug.Log($"Harvested {yield} units of {crop.Name}.");
            harvestedText.text = $"Harvested {yield} units of {crop.Name}.";
            UpdateCropInfo(crop, cropObject, infoText);
        }
        else
        {
            harvestedText.text = "";
        }
    }

    private void PlantCrop(Crop crop, GameObject cropObject, TextMeshProUGUI infoText)
    {
        crop.Plant();
        UpdateCropInfo(crop, cropObject, infoText);
    }

    private void UpdateCropInfo(Crop crop, GameObject cropObject, TextMeshProUGUI infoText)
    {
        infoText.text = $"Name: {crop.Name}\nHealth: {crop.Health:F1}\nReady to Harvest: {crop.IsReadyToHarvest}";
        cropObject.GetComponent<Renderer>().material = crop.GetCurrentMaterial();
    }
}
