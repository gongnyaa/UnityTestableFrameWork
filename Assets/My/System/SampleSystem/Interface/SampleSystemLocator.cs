namespace My.System.SampleSystem.Interface
{
    public class SampleSystemLocator : ISampleSystemManager
    {
        private static ISampleSystemManager _sampleSystemManager;

        public static readonly SampleSystemLocator Instance = new SampleSystemLocator();
       
        private SampleSystemLocator()
        {
        }
        
        public static void Set(ISampleSystemManager sampleSystemManager)
        {
            _sampleSystemManager = sampleSystemManager;
        }
    }
}