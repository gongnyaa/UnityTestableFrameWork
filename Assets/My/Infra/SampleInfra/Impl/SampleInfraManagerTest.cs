using My.Infra.SampleInfra.Interface;
using NUnit.Framework;

namespace My.Infra.SampleInfra.Impl
{
    public class SampleInfraManagerTest
    {
        private SampleInfraManager _sampleInfraManager;

        [SetUp]
        public void SetUp()
        {
            _sampleInfraManager = new SampleInfraManager();            
        }
    }
}