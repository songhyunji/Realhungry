using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FoodDatabaseEditor
{
    public static string rootDataPath = "Ahyeong/Database";

    [MenuItem("Database/Build Ingredient DB")]
    public static void BuildIngredientDatabase()
    {
        // CSV 데이터 파일 체크 및 파싱
        var dataDic = CSVParser.Read(rootDataPath + "/ingredientDB");

        if (dataDic == null)
        {
            Debug.LogError("[DB Build] CSV parsing result is null");
            return;
        }

        // 데이터베이스 파일 생성 또는 초기화
        FoodIngredientDatabase db = Resources.Load<FoodIngredientDatabase>(rootDataPath + "/IngredientDatabase");
        if (db == null)
        {
            db = ScriptableObject.CreateInstance<FoodIngredientDatabase>();

            string databasePath = "Assets/Resources/" + rootDataPath + "/IngredientDatabase.asset";
            AssetDatabase.CreateAsset(db, databasePath);
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("[Ing.DB Build] SO is created at " + databasePath);
        }
        else
        {
            db.Clear();
        }

        // 서브 데이터 디렉토리 체크
        string dataDirectoryPath = "Assets/Resources/" + rootDataPath + "/Ingredients";
        if (!Directory.Exists(dataDirectoryPath))
        {
            Directory.CreateDirectory(dataDirectoryPath);
        }

        // 기존 파일 탐색
        FoodIngredient[] unconnectedData = Resources.LoadAll<FoodIngredient>(rootDataPath + "/Ingredients");
        for (int i = 0; i < unconnectedData.Length; i++)
        {
            db.ingredients.Add(unconnectedData[i]);
            db.ingredients[i].SetData(dataDic[i]);
            EditorUtility.SetDirty(db.ingredients[i]);
        }

        // 재사용할 데이터 파일 없으면 생성
        int charCount = dataDic.Count;
        for (int i = unconnectedData.Length; i < charCount; i++)
        {
            FoodIngredient newData = ScriptableObject.CreateInstance<FoodIngredient>();
            newData.SetData(dataDic[i]);

            string subDataPath = dataDirectoryPath + "/ing_" + i.ToString("0") + ".asset";
            AssetDatabase.CreateAsset(newData, subDataPath);
            EditorUtility.SetDirty(newData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            db.ingredients.Add(newData);
        }
        EditorUtility.SetDirty(db);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("[Ing.DB Build] Success.");
    }
}
