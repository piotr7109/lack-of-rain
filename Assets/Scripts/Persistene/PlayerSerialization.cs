using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSerialization {

    public class PlayerSerialization : MonoBehaviour {
        public static SerializedPlayer GetSerialized() {
            GameObject player = GameObject.Find("Player");
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

            return new SerializedPlayer(player, gameManager);
        }

    }

    [Serializable]
    public class SerializedPlayer {
        Position position;

        //PlayerStats.cs
        int maxHealth;
        Stat damage;
        Stat armour;
        Stat radiationResistance;
        Stat movementSpeed;

        //PlayerShooting.cs
        string weaponName;

        SerializedGameManager gameManager;

        public SerializedPlayer(GameObject player, GameObject gameMaster) {
            position = new Position(player.transform.position);
            CharacterStats stats = player.GetComponent<CharacterStats>();
            WeaponShooting shooting = player.GetComponentInChildren<WeaponShooting>();

            maxHealth = stats.currentHealth;
            damage = stats.damage;
            armour = stats.armour;
            radiationResistance = stats.radiationResistance;
            movementSpeed = stats.movementSpeed;

            gameManager = new SerializedGameManager(gameMaster);
        }

        public void CreateInstance(Transform transform, Transform gameMaster) {
            GameObject gameObject = transform.gameObject;
            
            gameObject.transform.position = position.GetVector();

            SetStats(gameObject.GetComponent<CharacterStats>());

            Weapon weapon = transform.GetComponentInChildren<Equipment>().weapon;
            weaponName = weapon != null ? weapon.name : "";

            gameManager.CreateInstance(gameMaster);
        }
        
        void SetStats(CharacterStats stats) {
            stats.maxHealth = maxHealth;
            stats.damage = damage;
            stats.armour = armour;
            stats.radiationResistance = radiationResistance;
            stats.movementSpeed = movementSpeed;
        }

    }

    [Serializable]
    public class SerializedGameManager {

        //Inventory.cs
        List<string> itemNames;

        //EquipmentManager.cs
        string armourName;
        string weaponName;

        //QuestManager.cs
        List<string> questNames;
        List<QuestStatus> questStatuses;

        //LevelsManager.cs
        int currentLevel;
        int experience;

        public SerializedGameManager(GameObject gameObject) {
            Inventory inventory = gameObject.GetComponent<Inventory>();
            //EquipmentManager equipment = gameObject.GetComponent<EquipmentManager>();
            QuestManager quests = gameObject.GetComponent<QuestManager>();
            LevelsManager levels = gameObject.GetComponent<LevelsManager>();

            itemNames = new List<string>();
            inventory.items.ForEach(item => itemNames.Add(item.name));

            //armourName = equipment.armour != null ? equipment.armour.name : null;
            //weaponName = equipment.weapon != null ? equipment.weapon.name : null;

            questNames = new List<string>();
            questStatuses = new List<QuestStatus>();
            quests.quests.ForEach(quest => {
                questNames.Add(quest.title);
                questStatuses.Add(quest.status);
            });

            currentLevel = levels.currentLevel;
            experience = levels.experience;
        }

        public void CreateInstance(Transform gameMaster) {
            Inventory inventory = gameMaster.GetComponentInChildren<Inventory>();
            //EquipmentManager equipment = gameMaster.GetComponentInChildren<EquipmentManager>();
            LevelsManager levels = gameMaster.GetComponentInChildren<LevelsManager>();
            QuestManager quests = gameMaster.GetComponent<QuestManager>();

            inventory.items = new List<Item>();
            itemNames.ForEach(itemName => inventory.Add(GameObject.Instantiate(AssetsManager.GetItem(itemName) as Item)));
            
            if (weaponName != null) {
                //inventory.FindItem(weaponName).Use();
            }

            if (armourName != null) {
                //inventory.FindItem(armourName).Use();
            }

            quests.quests = new List<Quest>();
            for (int i = 0; i < questNames.Count; i++) {
                Quest quest = AssetsManager.GetQuest(questNames[i]);
                quest.status = questStatuses[i];
                quests.quests.Add(quest);
            }

            levels.currentLevel = currentLevel;
            levels.experience = experience;
        }
    }
}