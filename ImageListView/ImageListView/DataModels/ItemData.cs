using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using System.Windows.Media.Imaging;

namespace ImageListView.DataModels
{
	public class ItemData : NotificationObject, ILazyLoadingItem
	{

		public ItemData()
		{
			var path = @"Assets/Penguins.jpg";
			this.Icon = BitmapFrame.Create(new Uri(path, UriKind.Relative));
		}

		#region [プロパティ]
		#region Label変更通知プロパティ
		private string _Label;

		public string Label
		{
			get
			{ return _Label; }
			set
			{ 
				if (_Label == value)
					return;
				_Label = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Icon変更通知プロパティ
		private BitmapSource _Icon;

		public BitmapSource Icon
		{
			get
			{ return _Icon; }
			set
			{
				if (_Icon == value)
					return;
				_Icon = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		/// <summary>
		/// 画面に表示されたことを検知するためのプロパティ
		/// </summary>
		/// <remarks>
		/// XAMLでバインディングが呼び出された場合にgetプロパティが呼び出されます。
		/// </remarks>
		public bool IsLoaded
		{
			get
			{
				OnLoaded();
				return true;
			}
		}
		#endregion


		/// <summary>
		/// IsLoadedプロパティが読み込まれたときに発生するイベント
		/// </summary>
		public event EventHandler Loaded;

		private void OnLoaded()
		{
			var h = this.Loaded;
			if (h != null)
			{
				h(this, EventArgs.Empty);
			}
		}
	}

	interface ILazyLoadingItem
	{
		bool IsLoaded { get; }
		event EventHandler Loaded;
	}
}
