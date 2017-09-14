#region

using System;
using Autofac;
using FanReact;

#endregion

namespace FanReact.Droid {
	public class DroidDependencySetup : AppSetup {
		protected override void RegisterDependencies(ContainerBuilder cb) {
			base.RegisterDependencies(cb);
		}
	}
}