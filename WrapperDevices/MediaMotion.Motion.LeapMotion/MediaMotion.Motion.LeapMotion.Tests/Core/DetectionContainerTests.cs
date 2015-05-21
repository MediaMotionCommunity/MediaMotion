using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using MediaMotion.Motion.LeapMotion.Core.Exceptions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;
using Moq;
using NUnit.Framework;

namespace MediaMotion.Motion.LeapMotion.Tests.Core {
	[TestFixture]
	public class DetectionContainerTests {
		private DetectionContainer detectionContainer;
		private Mock<ICustomDetection> customDetectionMock;
		private Mock<ILeapDetection> iLeapDetectionMock;
		private ActionCollection actionCollection;
		private Mock<Frame> frameMock;
		private const ActionType AUsedAction = ActionType.Down;
		private const ActionType AnOtherUsedAction = ActionType.Right;
		private const ActionType AnUnsedAction = ActionType.Up;

		private class CustomDetectionImplementation : ICustomDetection {
			public void Detection(Frame frame, IActionCollection actionCollection) {
				actionCollection.Add(AUsedAction);
			}
		}

		private class DetectionWithParameterConstructor : ICustomDetection {
			private readonly CustomDetectionImplementation implementation;

			public DetectionWithParameterConstructor(CustomDetectionImplementation implementation) {
				this.implementation = implementation;
			}

			public void Detection(Frame frame, IActionCollection actionCollection) {
				if (this.implementation != null)
					actionCollection.Add(AnOtherUsedAction);
			}
		}

		[SetUp]
		public void Setup() {
			this.customDetectionMock = new Mock<ICustomDetection>();
			this.detectionContainer = new DetectionContainer();
			this.iLeapDetectionMock = new Mock<ILeapDetection>();
			this.actionCollection = new ActionCollection();
			this.frameMock = new Mock<Frame>();
		}

		[Test]
		public void GivenNewClass_WhenCreated_ThenShouldBeEmpty() {
			Assert.IsTrue(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenNewClass_WhenRegisterAnInstance_ThenShouldBeNotEmpty() {
			this.detectionContainer.Register(this.customDetectionMock.Object);

			Assert.IsFalse(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenNewClass_WhenRegisterAType_ThenShouldBeEmpty() {
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);

			Assert.IsTrue(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterType_WhenEnableWithUsedAction_ThenShouldBeNotEmpty() {
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);
			
			this.detectionContainer.Enable(AUsedAction);

			Assert.IsFalse(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterType_WhenEnableWithActionNeverUse_ThenShouldBeEmpty() {
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);

			this.detectionContainer.Enable(AnUnsedAction);

			Assert.IsTrue(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithMultipleRegisterInstance_WhenClear_ThenShouldBeEmpty() {
			this.detectionContainer.Register(this.customDetectionMock.Object);
			this.detectionContainer.Register(this.iLeapDetectionMock.Object);

			this.detectionContainer.Clear();

			Assert.IsTrue(this.detectionContainer.IsEmpty());
		}

		[Test]
		public void GivenInstanceWithRegisterInstance_WhenDetectMouvement_ThenShouldCallMethodDetectionOfInstance() {
			this.detectionContainer.Register(this.customDetectionMock.Object);
			
			this.detectionContainer.DetectMouvement(this.frameMock.Object, this.actionCollection);

			this.customDetectionMock.Verify(cd => cd.Detection(this.frameMock.Object, this.actionCollection));
		}

		[Test]
		public void GivenRegisterTypeAndEnabledThis_WhenDetectMouvement_ThenShouldUsedDetectionMethod() {
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);
			this.detectionContainer.Enable(AUsedAction);

			this.detectionContainer.DetectMouvement(this.frameMock.Object, this.actionCollection);

			Assert.IsNotNull(this.actionCollection.FirstOrDefault(ac => ac.Type.Equals(AUsedAction)));
		}

		[Test]
		public void GivenRegisterAndEnabledATypeWithNoParameterLessConstructorButParameterTypeNotEnabled_WhenDetectMouvement_ThenShouldReceiveANullArgument() {
			this.detectionContainer.Register<DetectionWithParameterConstructor>(AnOtherUsedAction);
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);
			this.detectionContainer.Build();
			this.detectionContainer.Enable(AnOtherUsedAction);

			this.detectionContainer.DetectMouvement(this.frameMock.Object, this.actionCollection);

			Assert.IsNull(this.actionCollection.FirstOrDefault(ac => ac.Type.Equals(AnOtherUsedAction)));
		}

		[Test]
		public void GivenRegisterAndEnabledATypeWithNoParameterLessConstructorAndParameterTypeEnabled_WhenDetectMouvement_ThenShouldReceiveOtherDetectionInstance() {
			this.detectionContainer.Register<DetectionWithParameterConstructor>(AnOtherUsedAction);
			this.detectionContainer.Register<CustomDetectionImplementation>(AUsedAction);
			this.detectionContainer.Build();
			this.detectionContainer.Enable(AUsedAction);
			this.detectionContainer.Enable(AnOtherUsedAction);

			this.detectionContainer.DetectMouvement(this.frameMock.Object, this.actionCollection);

			Assert.IsNotNull(this.actionCollection.FirstOrDefault(ac => ac.Type.Equals(AnOtherUsedAction)));
		}

		[Test]
		[ExpectedException(typeof(DetectionResolveException))]
		public void GivenRegisterAndEnabledATypeWithNoParameterLessConstructorButParameterTypeNotRegister_WhenBuild_ThenShouldThrowAnException() {
			this.detectionContainer.Register<DetectionWithParameterConstructor>(AnOtherUsedAction);
			this.detectionContainer.Build();
		}
	}
}