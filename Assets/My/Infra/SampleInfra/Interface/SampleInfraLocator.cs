namespace My.Infra.SampleInfra.Interface
{
    public class SampleInfraLocator : ISampleInfraManager
    {
        private static ISampleInfraManager _sampleInfraManager;

        public static readonly SampleInfraLocator Instance = new SampleInfraLocator();
       
        private SampleInfraLocator()
        {
        }
        
        public static void Set(ISampleInfraManager sampleInfraManager)
        {
            _sampleInfraManager = sampleInfraManager;
        }
    }
}