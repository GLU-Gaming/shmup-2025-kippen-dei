using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public List<GameObject> healthPrefabs; // Prefab van een HP-vakje

    public Transform healthContainer; // Zet hier je UI-container in
    
    private List<GameObject> healthIcons = new List<GameObject>();


    private void Awake()
    {
        if (FindObjectsOfType<PlayerHealthUI>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateHealthUI();
    }

    private void Update()
    {
            UpdateHealthUI();
    }

    public void CreateHealthUI()
    {
        // voor iedere hp komt er een leven bij
        for (int i = 0; i < Player.instance.maxHealth; i++)
        {
            GameObject prefabToUse = healthPrefabs[i % healthPrefabs.Count];
            GameObject icon = Instantiate(prefabToUse, healthContainer);
            healthIcons.Add(icon);
        }
    }
    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].SetActive(i < Player.instance.playerHp);
        }
    }

}
