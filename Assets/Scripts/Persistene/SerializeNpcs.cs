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
        float attackDamage;

        //Npc.cs
        float radius;
        string dialogueFilename;
        NpcReaction reaction;
        List<string> questNames;

        //EnemyNpc.cs
        float chaseRadius;
        float attackRadius;
        float meleeRadius;

        public SerializedNpc(GameObject gameObject) {
            position = new Position(gameObject.transform.position);
            EnemyShooting enemyShooting = gameObject.GetComponentInChildren<EnemyShooting>();
            EnemyNpc enemyNpc = gameObject.GetComponentInChildren<EnemyNpc>();
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
            npc.quests.ForEach(quest => questNames.Add(quest.title));

            attackSpeed = enemyShooting.meleeAttackSpeed;
            attackDamage = enemyShooting.meleeAttackDamage;
            weaponName = enemyShooting.weapon.name;

            chaseRadius = enemyNpc.chaseRadius;
            attackRadius = enemyNpc.attackRadius;
            meleeRadius = enemyNpc.meleeRadius;
        }

        public void CreateInstance(Transform transform) {
            transform.position = position.GetVector();
            SetStats(transform.GetComponent<NpcStats>());
            SetNpc(transform.GetComponentInChildren<Npc>());
            SetEnemyShooting(transform.GetComponentInChildren<EnemyShooting>());
            SetEnemyNpc(transform.GetComponentInChildren<EnemyNpc>());

            GameObject gameObject = GameObject.Instantiate(transform.gameObject, transform.position, transform.rotation);

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

        void SetEnemyShooting(EnemyShooting shooting) {
            shooting.meleeAttackSpeed = attackSpeed;
            shooting.meleeAttackDamage = attackDamage;
            shooting.weapon = AssetsManager.GetItem(weaponName) as Weapon;
        }

        void SetEnemyNpc(EnemyNpc enemy) {
            enemy.chaseRadius = chaseRadius;
            enemy.attackRadius = attackRadius;
            enemy.meleeRadius = meleeRadius;
        }

    }
}