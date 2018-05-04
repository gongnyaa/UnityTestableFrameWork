using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using TastableFrameFrameWork.CodeGenerator.Module;
using TastableFrameFrameWork.CodeGenerator.SystemInfra;
using TastableFrameFrameWork.CodeGenerator.Util;
using UnityEditor;
using UnityEngine;

namespace TastableFrameFrameWork.CodeGenerator.Editor
{
    public static class MvpGenerator
    {

        private static string GetFileScript(string path)
        {
            var stringBuilder = new StringBuilder();
            var sr = new StreamReader(path);
            while (sr.EndOfStream == false) {
                var line = sr.ReadLine();
                stringBuilder.Append( line);
                stringBuilder.AppendLine( );
            }

            stringBuilder.Length -= 1;
            sr.Close();
            return stringBuilder.ToString();
        }
        
        private static void AddUnityEditorMode(string path)
        {
            var fileScript = GetFileScript(path);
            if (fileScript.StartsWith("#if UNITY_EDITOR"))
            {
                return;
            }

            var modifyFileScript = "#if UNITY_EDITOR\n" + fileScript + "\n#endif";
            var sw = new StreamWriter(path,false, new UTF8Encoding(true));
            sw.WriteLine(modifyFileScript);
            sw.Flush();
            sw.Close();
        }

        [MenuItem("Assets/MVP/EscapeForTest")]
        public static void EscapeForTest()
        {
            foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
            {
                Debug.Log(assetPath);
                if (assetPath.Contains("Test.cs"))
                {
                    AddUnityEditorMode(assetPath);
                }
            }
           
            
        }
        
        [MenuItem("Assets/MVP/Generate Folder")]
        public static void GenerateFolder()
        {
            var rootPath = Application.dataPath ;
            //Infra
            Directory.CreateDirectory(rootPath + "/My");
            Directory.CreateDirectory(rootPath + "/My/Infra");
            var infraAssembly = new AssemblyDefinitionObject("Assembly-My-Infra",rootPath + "/My/Infra");
            GenerateAssemblyFile(infraAssembly);
            
            //System
            Directory.CreateDirectory(rootPath + "/My/System");
            var systemAssembly = new AssemblyDefinitionObject("Assembly-My-System",rootPath + "/My/System");
            systemAssembly.AddReference("Assembly-My-Infra");
            GenerateAssemblyFile(systemAssembly);
            
            //MVP
            Directory.CreateDirectory(rootPath + "/My/MVP");
            var mvpAssembly = new AssemblyDefinitionObject("Assembly-My-MVP",rootPath + "/My/MVP");
            mvpAssembly.AddReference("Assembly-My-Infra");;
            mvpAssembly.AddReference("Assembly-My-System");;
            GenerateAssemblyFile(mvpAssembly);
            
            //Public
            Directory.CreateDirectory(rootPath + "/My/Public");
            
            AssetDatabase.Refresh();
        }
        
        private static void GenerateAssemblyFile(AssemblyDefinitionObject assemblyDefinitionObject)
        {
            File.WriteAllText(assemblyDefinitionObject.GetPath() + ".asmdef",
                JsonUtility.ToJson(assemblyDefinitionObject));
        }

        [MenuItem("Assets/MVP/Generate MVP Scripts for Prefab")]
        public static void GenerateMvpScriptsForPrefab()
        {
            FileIoLocater.Set(new FileIoManager());
            var prefab = Selection.gameObjects[0];
            var gameObjectFieldInfo =
                new GameObjectFieldInfo(prefab, AssetDatabase.GetAssetPath(Selection.activeObject));
            new ModuleViewScriptBuilder().GenerateScript(gameObjectFieldInfo);
            new ModulePresenterScriptBuilder().GenerateScript(gameObjectFieldInfo);
            new ModuleModelScriptBuilder().GenerateScript(gameObjectFieldInfo);
            new ModuleModelTestScriptBuilder().GenerateScript(gameObjectFieldInfo);            
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/MVP/Generate System Folder and Scripts")]
        public static void GenerateSystemScripts()
        {                        
            var layerName = "System";
            GenerateSystemOrInfraScript(layerName);
        }
        
        [MenuItem("Assets/MVP/Generate Infra Folder and Scripts")]
        public static void GenerateInfraScripts()
        {                        
            var layerName = "Infra";
            GenerateSystemOrInfraScript(layerName);
        }

        private static void GenerateSystemOrInfraScript(string layerName)
        {
            FileIoLocater.Set(new FileIoManager());
            var directory = AssetDatabase.GetAssetPath(Selection.activeObject);
            Directory.CreateDirectory(directory + "/Impl");
            Directory.CreateDirectory(directory + "/Interface");
            
            new SystemLocatorScriptBuilder().GenerateScript(directory,layerName);
            new SystemIManagerScriptBuilder().GenerateScript(directory,layerName);
            new SystemManagerScriptBuilder().GenerateScript(directory,layerName);
            new SystemManagerTestScriptBuilder().GenerateScript(directory,layerName);
            AssetDatabase.Refresh();
        }
        


        [MenuItem("Assets/MVP/Generate PartView Scripts for Prefab")]
        public static void GeneratePartViewScriptsForPrefab()
        {
            var prefab = Selection.gameObjects[0];
            var gameObjectFieldInfo =
                new GameObjectFieldInfo(prefab, AssetDatabase.GetAssetPath(Selection.activeObject));
            new ModulePartModuleViewScriptBuilder().GenerateScript(gameObjectFieldInfo);
            AssetDatabase.Refresh();
        }

       

        [MenuItem("Assets/MVP/Add View Script for Prefab")]
        public static void AddViewScriptforPrefab()
        {
            AddScriptToView("View");
        }

        [MenuItem("Assets/MVP/Add PartView Script for Prefab")]
        public static void AddPartViewScriptforPrefab()
        {
            AddScriptToView("PartView");
        }

        private static void AddScriptToView(string viewType)
        {
            var prefab = Selection.gameObjects[0];
            var directory = AssetDatabase.GetAssetPath(prefab);

            // ReSharper disable once PossibleNullReferenceException
            var assemblyName = "Assembly-My-MVP";

            var types = Assembly.Load(assemblyName).GetTypes();
            foreach (var type in types)
            {
                if (type.Name != Selection.gameObjects[0].name + viewType) continue;

                var component = prefab.GetComponent(type);
                if(component == null){
                    component = prefab.AddComponent(type);
                }                
                var fieldInfos = component.GetType().GetFields(BindingFlags.NonPublic |
                                                               BindingFlags.Instance|BindingFlags.Public);
                foreach (var field in fieldInfos)
                {
                    if (field.Name == "Presenter") continue;
                    var gameObjectName = NameConverter.ToPascalFromPrivate(field.Name);

                    if (field.Name.Contains("ToggleGroup"))
                    {
                        var toggleGroup = prefab.transform.Find(gameObjectName);

                        var value = (IList) field.GetValue(component);
                        var newArray = Array.CreateInstance(field.FieldType.GetElementType(), toggleGroup.childCount);
                        
                        for (int i = 0; i < toggleGroup.childCount; i++)
                        {
                            var componentReference = toggleGroup.GetChild(i).GetComponent(field.FieldType.GetElementType());
                            if(componentReference != null)
                            {
                                newArray.SetValue(componentReference,i);
                            }                              
                        }
                        field.SetValue(component, newArray);
                    }
                    else if(field.Name.Contains("Text")||field.Name.Contains("Image")||field.Name.Contains("Button"))
                    {
                        var componentReference = FindFromAllChild(prefab.transform,gameObjectName).GetComponent(field.FieldType);
                        field.SetValue(component, componentReference);   
                    }
                    
                    AssetDatabase.Refresh();
                }
            }

            AssetDatabase.Refresh();
        }
        private static Transform FindFromAllChild(Transform targetTransform,string targetName)
        {
            var transform = targetTransform.Find(targetName);
            if (transform != null)
            {
                return transform;
            }

            if (targetTransform.childCount == 0)
            {
                return null;
            }

            for (int i = 0; i < targetTransform.childCount; i++)
            {
                transform = FindFromAllChild(targetTransform.GetChild(i), targetName);
                if (transform != null)
                {
                    return transform;
                }
            }

            return null;
        }
    }
    
}