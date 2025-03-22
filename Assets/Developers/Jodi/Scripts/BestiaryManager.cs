using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BestiaryManager : MonoBehaviour
{
    public GameObject EnemyButtonPrefab;
    public Transform EnemyListContent;
    public TextMeshProUGUI detailsName, detailsDescription;
    public Image enemyIconDisplay; 
    public TextMeshProUGUI detailsHealth, detailsDamage;
    public Transform modelPreviewPanel; 
    public GameObject detailsPanel; // Assign your DetailsPanel GameObject here

    void Start()
    {
        // Hide the details panel at startup
        detailsPanel.SetActive(false);

        // Load all EnemyData assets
        EnemyData[] enemies = Resources.LoadAll<EnemyData>("Enemies");

        // Create buttons for each enemy
        foreach (EnemyData enemy in enemies)
        {
            GameObject button = Instantiate(EnemyButtonPrefab, EnemyListContent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = enemy.enemyName;
            button.GetComponent<Button>().onClick.AddListener(() => 
            {
                ShowEnemyDetails(enemy); // Show details when clicked
                detailsPanel.SetActive(true); // Unhide the panel
            });
        }
    }


    void ShowEnemyDetails(EnemyData enemy)
    {
        // Clear old models
        foreach (Transform child in modelPreviewPanel)
        {
            Destroy(child.gameObject);
        }
        detailsName.text = enemy.enemyName;
        detailsDescription.text = enemy.description;
        enemyIconDisplay.sprite = enemy.enemyIcon;
        detailsHealth.text = "HEALTH: " + enemy.health.ToString();
        detailsDamage.text = "DAMAGE: " + enemy.damage.ToString();
        
        if (enemy.enemyModel != null)
        {
            GameObject model = Instantiate(enemy.enemyModel, modelPreviewPanel);
            model.transform.localPosition = Vector3.zero; 
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one * 50; 
        }
    }
}