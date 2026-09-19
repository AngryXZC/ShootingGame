using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

class ProcessModel : AssetPostprocessor
{
    void OnPostprocessModel(GameObject input)
    {
        if (!input.name.Contains("Enemy2b"))
            return;

        input.tag = "Enemy";

        foreach (Transform obj in input.GetComponentsInChildren<Transform>())
        {
            if (obj.name.CompareTo("col") == 0)
            {
                MeshRenderer r = obj.GetComponent<MeshRenderer>();
                if (r != null)
                {
                    r.enabled = false;
                }

                if (obj.GetComponent<MeshCollider>() == null)
                {
                    obj.gameObject.AddComponent<MeshCollider>();
                }

                obj.tag = "Enemy";
            }
        }

        Rigidbody rigid = input.GetComponent<Rigidbody>();
        if (rigid == null)
        {
            rigid = input.AddComponent<Rigidbody>();
        }
        rigid.useGravity = false;
        rigid.isKinematic = true;

        ModelImporter importer = assetImporter as ModelImporter;
        GameObject tar = AssetDatabase.LoadAssetAtPath<GameObject>(importer.assetPath);
        if (tar == null)
            return;

        string prefabPath = "Assets/Prefabs/Enemy2c.prefab";
        string prefabDir = Path.GetDirectoryName(prefabPath);
        if (!Directory.Exists(prefabDir))
        {
            Directory.CreateDirectory(prefabDir);
        }

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(tar, prefabPath);
        if (prefab == null)
            return;

        if (prefab.GetComponent<AudioSource>() == null)
        {
            prefab.AddComponent<AudioSource>();
        }

        GameObject rocket = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyRocket.prefab");
        GameObject fx = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FX/Explosion.prefab");

        SuperEnemy enemy = prefab.GetComponent<SuperEnemy>();
        if (enemy == null)
        {
            enemy = prefab.AddComponent<SuperEnemy>();
        }

        enemy.m_life = 50;
        enemy.m_point = 50;
        if (rocket != null)
        {
            enemy.m_rocket = rocket.transform;
        }
        if (fx != null)
        {
            enemy.m_explosionFX = fx.transform;
        }
    }
}
