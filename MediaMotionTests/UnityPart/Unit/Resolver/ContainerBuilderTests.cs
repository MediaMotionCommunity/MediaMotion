using MediaMotion.Core.Resolver;
using MediaMotion.Core.Resolver.Containers;
using MediaMotion.Core.Resolver.Registrations.Interfaces;
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
			var register = this.containerBuilder.Register(new ObjectWithoutDependency());

			Assert.IsTrue(register is IRegistration);
		}

		[Test]
		public void RegisterInstanceWhenResolveShouldReturnTheSameInstance() {
			var instance = new ObjectWithoutDependency();
			this.containerBuilder.Register(instance);
			var container = this.containerBuilder.Build();

			var @object = container.Get<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object == instance);
		}

		[Test]
		public void RegisterInstanceWithInterfaceWhenResolveInterfaceShouldReturnTheInstance() {
			var instance = new ObjectWithoutDependency();
			this.containerBuilder.Register(instance).As<IObjectWithoutDependency>();
			var container = this.containerBuilder.Build();

			var @object = container.Get<IObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object == instance);
		}

		[Test]
		public void RegisterTypeWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.Register<ObjectWithoutDependency>();
			var container = this.containerBuilder.Build();

			var @object = container.Get<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithoutDependency);
		}

		[Test]
		public void RegisterTypeWithInterfaceWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.Register<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			var container = this.containerBuilder.Build();

			var @object = container.Get<ObjectWithoutDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithoutDependency);			
		}

		[Test]
		public void RegisterTypeWithADependencyWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.Register<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			this.containerBuilder.Register<ObjectWithDependency>().As<IObjectWithDependency>();
			var container = this.containerBuilder.Build();

			var @object = container.Get<ObjectWithDependency>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithDependency);
		}

		[Test]
		public void RegisterTypeWithTwoDependenciesWhenResolveShouldReturnAnInstanceOfType() {
			this.containerBuilder.Register<ObjectWithDependencies>().As<IObjectWithDependencies>();
			this.containerBuilder.Register<ObjectWithoutDependency>().As<IObjectWithoutDependency>();
			this.containerBuilder.Register<ObjectWithDependency>().As<IObjectWithDependency>();
			var container = this.containerBuilder.Build();

			var @object = container.Get<ObjectWithDependencies>();

			Assert.IsNotNull(@object);
			Assert.IsTrue(@object is ObjectWithDependencies);
		}

		[Test]
		public void RegisterTypeWithSingleInstanceScopeWhenResolveManyTimeShouldBeTheSameInstance() {
			this.containerBuilder.Register<ObjectWithoutDependency>().As<IObjectWithoutDependency>().SingleInstance();
			var container = this.containerBuilder.Build();

			var @object = container.Get<IObjectWithoutDependency>();

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
