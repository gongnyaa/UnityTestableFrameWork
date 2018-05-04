using My.System.SampleSystem.Interface;
using NUnit.Framework;

namespace My.System.SampleSystem.Impl
{
    public class SampleSystemManagerTest
    {
        private SampleSystemManager _sampleSystemManager;

        [SetUp]
        public void SetUp()
        {
            _sampleSystemManager = new SampleSystemManager();            
        }
    }
}