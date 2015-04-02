using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using MediaMotion.Motion.LeapMotion.MovementsDetection;
using Moq;
using NUnit.Framework;

namespace MediaMotion.Motion.LeapMotionTests.Core {
	[TestFixture]
	public class DetectionContainerTests {
		private DetectionContainer _detectionContainer;
		private Mock<ICustomDetection> _customDetectionMock;
		private Mock<ILeapDetection> _iLeapDetectionMock;
		private ActionCollection _actionCollection;
		private Mock<Frame> _frameMock;
		private const ActionType AnUsedAction = ActionType.Down;
		private const ActionType AnUnsedAction = ActionType.Up;

		private class CustomDetectionImplementation : ICustomDetection {
			public void Detection(Frame frame, IActionCollection actionCollection) {
				actionCollection.Add(AnUsedAction);
			}
		}

		[SetUp]
		public void Setup() {
			this._customDetectionMock = new Mock<ICustomDetection>();
			this._detectionContainer = new DetectionContainer();
			this._iLeapDetectionMock = new Mock<ILeapDetection>();
			this._actionCollection = new ActionCollection();
			this._frameMock = new Mock<Frame>();
		}

		[Test]
		public void GivenNewClass_WhenCreated_ThenShouldBeEmpty() {
			Assert.IsTrue(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenNewClass_WhenRegisterAnInstance_ThenShouldBeNotEmpty() {
			this._detectionContainer.Register(this._customDetectionMock.Object);

			Assert.IsFalse(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenNewClass_WhenRegisterAType_ThenShouldBeEmpty() {
			this._detectionContainer.Register<CustomDetectionImplementation>(AnUsedAction);

			Assert.IsTrue(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterType_WhenEnableWithUsedAction_ThenShouldBeNotEmpty() {
			this._detectionContainer.Register<CustomDetectionImplementation>(AnUsedAction);
			
			this._detectionContainer.Enable(AnUsedAction);

			Assert.IsFalse(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterType_WhenEnableWithActionNeverUse_ThenShouldBeEmpty() {
			this._detectionContainer.Register<CustomDetectionImplementation>(AnUsedAction);

			this._detectionContainer.Enable(AnUnsedAction);

			Assert.IsTrue(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithMultipleRegisterInstance_WhenClear_ThenShouldBeEmpty() {
			this._detectionContainer.Register(this._customDetectionMock.Object);
			this._detectionContainer.Register(this._iLeapDetectionMock.Object);

			this._detectionContainer.Clear();

			Assert.IsTrue(this._detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterInstance_WhenDetectMouvement_ThenShouldCallMethodDetectionOfInstance() {
			this._detectionContainer.Register(this._customDetectionMock.Object);
			
			this._detectionContainer.DetectMouvement(this._frameMock.Object, this._actionCollection);

			this._customDetectionMock.Verify(cd => cd.Detection(this._frameMock.Object, this._actionCollection));
		}

		[Test]
		public void GivenRegisterTypeAndEnabledThis_WhenDetectMouvement_ThenShouldCallDetectionOfThisType() {
			this._detectionContainer.Register<CustomDetectionImplementation>(AnUsedAction);
			this._detectionContainer.Enable(AnUnsedAction);

			this._detectionContainer.DetectMouvement(this._frameMock.Object, this._actionCollection);


		}
	}
}