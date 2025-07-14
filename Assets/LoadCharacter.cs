using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class LoadCharacter : AssetPostprocessor
{
	private static object textasset;

	static void OnPostprocessAllAssets(
		string[] importedAssets,
		string[] deletedAssets,
		string[] movedAssets,
		string[] movedFromAssetPaths)
	{
		foreach (string assetPath in importedAssets)
		{
			if (assetPath.IndexOf("/characterData.csv") != -1)
			{
				TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);

				string assetFile = assetPath.Replace(".csv", ".asset");
				 
				CharacterDataBase characterDataBase = AssetDatabase.LoadAssetAtPath<CharacterDataBase>(assetFile);

				if (characterDataBase != null)
				{
					characterDataBase = new CharacterDataBase();
					AssetDatabase.CreateAsset(characterDataBase, assetFile);
				}

				//characterDataBase.datas = CsvSerializer.DeserializeFromString<CharacterData>(textasset.text);
				EditorUtility.SetDirty(characterDataBase);
				AssetDatabase.SaveAssets();
			}
		}
	}
}
#endif