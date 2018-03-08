using UnityEngine;
using System.Collections.Generic;

namespace GameSerialization {

    public class SerializeNpcs {

        public static List<SerializedNpc> GetSerialized() {
            List<GameObject> npcs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            List<SerializedNpc> serializedNpcs = new List<SerializedNpc>();

            npcs.ForEach(gameObject => {
                serializedNpcs.Add(new SerializedNpc(gameObject));
            });

            return serializedNpcs;
        }
    }

    [System.Serializable]
    public class SerializedNpc {
        Position position;

        //NpcStats.cs
        int maxHealth;
        Stat damage;
        Stat armour;
        Stat radiationResistance;
        Stat movementSpeed;
        List<Item> items;
        int experienceGained;

        //EnemyShooting.cs
        string weaponName;
        float attackSpeed;

        //Npc.cs
        float radius;
        string dialogueFilename;
        NpcReaction reaction;
        List<string> questNames;
        List<QuestStatus> questStatuses;

        //EnemyNpc.cs
        float chaseRadius;
        float attackRadius;
        float meleeRadius;

        public SerializedNpc(GameObject gameObject) {
            position = new Position(gameObject.transform.position);
            WeaponShooting enemyShooting = gameObject.GetComponentInChildren<WeaponShooting>();
            EnemyAI enemyNpc = gameObject.GetComponentInChildren<EnemyAI>();
            NpcStats stats = gameObject.GetComponent<NpcStats>();
            Npc npc = gameObject.GetComponentInChildren<Npc>();

            maxHealth = stats.maxHealth;
            damage = stats.damage;
            armour = stats.armour;
            radiationResistance = stats.radiationResistance;
            movementSpeed = stats.movementSpeed;
            //stats.items = stats.items;
            experienceGained = stats.experienceGained;

            radius = npc.radius;
            dialogueFilename = npc.dialogueFilename;
            reaction = npc.reaction;
            questNames = new List<string>();
            questStatuses = new List<QuestStatus>();
            npc.quests.ForEach(quest => {
                questNames.Add(quest.title);
                questStatuses.Add(quest.status);
            });

            attackSpeed = enemyShooting.meleeAttackSpeed;
            //weaponName = enemyShooting.weapon.name;

            chaseRadius = enemyNpc.chaseRadius;
            attackRadius = enemyNpc.attackRadius;
            meleeRadius = enemyNpc.meleeRadius;
        }

        public void CreateInstance(Transform transform) {
            GameObject gameObject = GameObject.Instantiate(transform.gameObject, transform.position, transform.rotation);

            gameObject.transform.position = position.GetVector();
            SetStats(gameObject.GetComponent<NpcStats>());
            SetNpc(gameObject.GetComponentInChildren<Npc>());
            SetEnemyShooting(gameObject.GetComponentInChildren<WeaponShooting>());
            SetEnemyNpc(gameObject.GetComponentInChildren<EnemyAI>());

        }

        void SetStats(NpcStats stats) {
            stats.maxHealth = maxHealth;
            stats.damage = damage;
            stats.armour = armour;
            stats.radiationResistance = radiationResistance;
            stats.movementSpeed = movementSpeed;
            //stats.items = items;
            stats.experienceGained = experienceGained;
        }

        void SetNpc(Npc npc) {
            npc.radius = radius;
            npc.dialogueFilename = dialogueFilename;
            npc.reaction = reaction;

            npc.quests = new List<Quest>();
            questNames.ForEach(questTitle => npc.quests.Add(AssetsManager.GetQuest(questTitle)));
        }

        void SetEnemyShooting(WeaponShooting shooting) {
            shooting.meleeAttackSpeed = attackSpeed;
            //shooting.weapon = AssetsManager.GetItem(weaponName) as Weapon;
        }

        void SetEnemyNpc(EnemyAI enemy) {
            enemy.chaseRadius = chaseRadius;
            enemy.attackRadius = attackRadius;
            enemy.meleeRadius = meleeRadius;
        }

    }
}