#if UNITY_EDITOR
using System.IO;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using TastableFrameFrameWork.CodeGenerator.Module;
using TastableFrameFrameWork.CodeGenerator.SystemInfra;
using TastableFrameFrameWork.CodeGenerator.Util;
using UnityEngine;

namespace TastableFrameFrameWork.CodeGenerator.Editor
{
    public class ModuleScriptBuilderTest
    {
        private IFileIoManager _fileIoManager;
        private IGameObjectFieldInfo _gameObjectFieldInfo;

        [SetUp]
        public void SetUp()
        {
            _fileIoManager = Substitute.For<IFileIoManager>();
            FileIoLocater.Set(_fileIoManager);
            _gameObjectFieldInfo = new GameObjectFieldInfoFake();
        }
        [Test]
        public void TestCreateScriptView()
        {            
            new ModuleViewScriptBuilder().GenerateScript(_gameObjectFieldInfo);
            _fileIoManager.Received().WriteFile("TestPath", GetFileString("MVP","View"));                        
        }
        [Test]
        public void TestCreateScriptPresenter()
        {            
            new ModulePresenterScriptBuilder().GenerateScript(_gameObjectFieldInfo);
            _fileIoManager.Received().WriteFile("TestPath", GetFileString("MVP","Presenter"));                        
        }
        [Test]
        public void TestCreateScriptModel()
        {            
            new ModuleModelScriptBuilder().GenerateScript(_gameObjectFieldInfo);
            _fileIoManager.Received().WriteFile("TestPath", GetFileString("MVP","Model"));                        
        }
        [Test]
        public void TestCreateScriptModelTest()
        {            
            new ModuleModelTestScriptBuilder().GenerateScript(_gameObjectFieldInfo);
            _fileIoManager.Received().WriteFile("TestPath", GetFileString("MVP","ModelTest"));                        
        }
        [TestCase("System")]
        [TestCase("Infra")]
        public void TestCreateScriptSystemLocator(string layerName)
        {            
            new SystemLocatorScriptBuilder().GenerateScript("TestPath/Sample",layerName);
            _fileIoManager.Received().WriteFile("TestPath/Sample/Interface/SampleLocator.cs", GetFileString(layerName,"Locator"));                        
        }        
        [TestCase("System")]
        [TestCase("Infra")]
        public void TestCreateScriptISystemManager(string layerName)
        {            
            new SystemIManagerScriptBuilder().GenerateScript("TestPath/Sample",layerName);
            _fileIoManager.Received().WriteFile("TestPath/Sample/Interface/ISampleManager.cs", GetFileString(layerName+"I","Manager"));                        
        }
        [TestCase("System")]
        [TestCase("Infra")]
        public void TestCreateScriptSystemManager(string layerName)
        {            
            new SystemManagerScriptBuilder().GenerateScript("TestPath/Sample",layerName);
            _fileIoManager.Received().WriteFile("TestPath/Sample/Impl/SampleManager.cs", GetFileString(layerName,"Manager"));                        
        }
        [TestCase("System")]
        [TestCase("Infra")]
        public void TestCreateScriptSystemManagerTest(string layerName)
        {            
            new SystemManagerTestScriptBuilder().GenerateScript("TestPath/Sample",layerName);
            _fileIoManager.Received().WriteFile("TestPath/Sample/Impl/SampleManagerTest.cs", GetFileString(layerName,"ManagerTest"));                        
        }
        

        private string GetFileString(string layerName,string kindName)
        {
            var stringBuilder = new StringBuilder();
            var file =string.Format(Application.dataPath+"/TastableFrameFrameWork/CodeGenerator/TestResult/{0}Sample{1}.txt",layerName,kindName);
            var sr = new StreamReader(file);
            while (sr.EndOfStream == false) {
                var line = sr.ReadLine();
                stringBuilder.Append( line);
                stringBuilder.AppendLine( );
            }

            stringBuilder.Length -= 1;
            sr.Close();
            return stringBuilder.ToString();
        }

    
    }
}
#endif
