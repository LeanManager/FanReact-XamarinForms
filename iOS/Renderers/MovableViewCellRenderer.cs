using System;
using System.Linq;
using FanReact;
using FanReact.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MovableViewCell), typeof(MovableViewCellRenderer))]
namespace FanReact.iOS
{
	public class MovableViewCellRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var parent = (ListView)item.Parent;
			var movableViewCell = (MovableViewCell)item;
			if (movableViewCell.CustomReorderCommand == null && !(parent.ItemsSource is IObservableCollectionEx))
				throw new InvalidOperationException("ItemsSource in ListView which contains MovableViewCell must implement IObservableCollectionEx or MovableViewCell.CustomReorderCommand must be set.");

			var newCell = base.GetCell(item, reusableCell, tv);

			if (newCell.GestureRecognizers != null && newCell.GestureRecognizers.OfType<MovableCellGestureRecognizer>().Any())
				newCell.RemoveGestureRecognizer(newCell.GestureRecognizers.OfType<MovableCellGestureRecognizer>().Single());

			newCell.AddGestureRecognizer(MovableCellGestureRecognizer.CreateGesture(parent, tv, movableViewCell, newCell));
			return newCell;
		}
	}
}
