using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanReact
{
	public interface IAppNavigation
	{
		void PushNNM(NativeNavigationModel nnm);
		void Push(Page page);
		void Pop();
	}
}

