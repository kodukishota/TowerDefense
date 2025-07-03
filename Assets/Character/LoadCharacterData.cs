using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class LoadCharacterData : AssetPostprocessor
{
	static void OnPostprocessAllAssets(
		string[] importedAssets,
		string[] deletedAssets,
		string[] movedAssets,
		string[] movedFromAssetPaths)
	{
		foreach (string str in importedAssets)
		{
			if(str.IndexOf("characterData.csv") != -1)
			{
				//Asset直下から読み取る
				TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
				//同名のScriptableObjectファイルを読み込む。ない場合は新しく作る。
				string assetfile = str.Replace(".csv", ".asset");
			
				CharacterDataBase characterDataBase = AssetDatabase.LoadAssetAtPath<CharacterDataBase>(assetfile);

				if (characterDataBase == null)
				{
					characterDataBase = new CharacterDataBase();
					AssetDatabase.CreateAsset(characterDataBase, assetfile);
				}

				//characterDataBase.datas = CSVSerializer.Deserialize<CharacterDataBase>(textAsset.text);
				EditorUtility.SetDirty(characterDataBase);
				AssetDatabase.SaveAssets();
			}
		}
	}
}
#endif