using NSubstitute;
using NUnit.Framework;

namespace MyModule.MVPSample
{
    public class MVPSampleModelTest
    {   
        private MVPSampleModel _mVPSampleModel;
        private MVPSamplePresenter _mVPSamplePresenter;

        [SetUp]
        public void SetUp()
        {
            _mVPSamplePresenter = Substitute.For<MVPSamplePresenter>();
            _mVPSampleModel = new MVPSampleModel();
            _mVPSampleModel.SetPresenter(_mVPSamplePresenter);
            throw new System.NotImplementedException();
        }

        [Test]
        public void TestOnShow()
        {
            _mVPSampleModel.OnShow();
            throw new System.NotImplementedException();
        }



        [Test]
        public void TestTest1 ()
        {
            // Set

            // Do
            _mVPSampleModel.Test1 ();

            // Check

            throw new System.NotImplementedException();
        }
        [Test]
        public void TestTest2 ()
        {
            // Set

            // Do
            _mVPSampleModel.Test2 ();

            // Check

            throw new System.NotImplementedException();
        }
        [TestCase(0)]
        public void TestTurnOnTest1Toggle (int index)
        {
            // Set

            // Do
            _mVPSampleModel.TurnOnTest1Toggle(index);

            // Check

            throw new System.NotImplementedException();
        }
        [TestCase(0)]
        public void TestTurnOnTest2Toggle (int index)
        {
            // Set

            // Do
            _mVPSampleModel.TurnOnTest2Toggle(index);

            // Check

            throw new System.NotImplementedException();
        }
    }
}