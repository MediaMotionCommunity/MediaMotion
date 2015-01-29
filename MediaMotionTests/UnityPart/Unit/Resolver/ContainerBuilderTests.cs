using MediaMotion.Resolver;
using NUnit.Framework;

namespace MediaMotionTests.UnityPart.Unit.Resolver {
	/// <summary>
	/// The container builder tests.
	/// </summary>
	[TestFixture]
	public class ContainerBuilderTests {
		private ContainerBuilder containerBuilder;

		[SetUp]
		public void Setup() {
			this.containerBuilder = new ContainerBuilder();
		}

		[Test]
		public void WhenRegisteringInstanceShouldReturnTheRegistrationObject() {
			var register = this.containerBuilder.RegisterInstance(new ObjectWithoutDependency());

			Assert.IsTrue(register is IRegistration);
		}

		[Test]
		public void RegisterInstanceWhenResolveShouldReturnTheSameInstance() {
			var instance = new ObjectWithoutDependency();
			this.containerBuilder.RegisterInstance(instance);
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object == instance);
		}

		[Test]
		public void RegisterInstanceWithInterfaceWhenResolveInterfaceShouldReturnTheInstance() {
			var instance = new ObjectWithoutDependency();
			this.containerBuilder.RegisterInstance(instance).As<IObjectWithoutDependency>();
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<IObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object == instance);
		}

		[Test]
		public void RegisterTypeWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.RegisterType<ObjectWithoutDependency>();
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithoutDependency);
		}

		[Test]
		public void RegisterTypeWithInterfaceWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.RegisterType<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			this.containerBuilder.Build();


			var @object = this.containerBuilder.Resolve<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithoutDependency);			
		}

		[Test]
		public void RegisterTypeWithADependencyWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.RegisterType<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			this.containerBuilder.RegisterType<ObjectWithDependency>().As<IObjectWithDependency>();
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<ObjectWithDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithDependency);						
		}

		[Test]
		public void RegisterTypeWithTwoDependenciesWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.RegisterType<ObjectWithDependencies>().As<IObjectWithDependencies>();
			this.containerBuilder.RegisterType<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			this.containerBuilder.RegisterType<ObjectWithDependency>().As<IObjectWithDependency>();
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<ObjectWithDependencies>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithDependencies);						
		}

		[Test]
		public void RegisterTypeWithSingleInstanceScopeWhenResolveManyTimeShouldBeTheSameInstance() {
			this.containerBuilder.RegisterType<ObjectWithoutDependency>().As<IObjectWithoutDependency>().SingleInstance();
			this.containerBuilder.Build();

			var @object = this.containerBuilder.Resolve<IObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithoutDependency);						
		}

		#region Internal Class
		internal class ObjectWithDependencies : IObjectWithDependencies {
			private readonly IObjectWithDependency objectWithDependency;

			private readonly IObjectWithoutDependency objectWithoutDependency;

			public ObjectWithDependencies(IObjectWithDependency objectWithDependency, IObjectWithoutDependency objectWithoutDependency) {
				this.objectWithDependency = objectWithDependency;
				this.objectWithoutDependency = objectWithoutDependency;
			}
		}

		internal interface IObjectWithDependencies {
		}

		internal class ObjectWithDependency : IObjectWithDependency {
			private readonly IObjectWithoutDependency dependency;

			public ObjectWithDependency(IObjectWithoutDependency dependency) {
				this.dependency = dependency;
			}
		}

		internal interface IObjectWithDependency {
		}

		internal class ObjectWithoutDependency : IObjectWithoutDependency {
		}

		internal interface IObjectWithoutDependency {
		}
		#endregion
	}
}
