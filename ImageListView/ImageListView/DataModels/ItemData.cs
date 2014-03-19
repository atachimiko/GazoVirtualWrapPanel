using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections;
using Livet.EventListeners;
using Livet.EventListeners.WeakEvents;

namespace ImageListView.DataModels
{
	public class ItemData : NotificationObject, ILazyLoadingItem
	{

		public ItemData()
		{
			this.Label = "Loading...";
			LoadDefaultIcon();
		}

		/// <summary>
		/// 
		/// </summary>
		public void Unload()
		{
			if (this._IsLoaded)
			{
				Console.WriteLine("Unload Id:{0}", this.IdText);
				LoadDefaultIcon();

				this._IsLoaded = false;
			}
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


		#region IdText変更通知プロパティ
		private string _IdText;

		public string IdText
		{
			get
			{ return _IdText; }
			set
			{ 
				if (_IdText == value)
					return;
				_IdText = value;
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
				if (!_IsLoaded)
					OnLoaded();
				_IsLoaded = true;
				return true;
			}
		}
		private bool _IsLoaded = false;
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

		// IsLoadedプロパティとバインドするための添付プロパティ
		public static readonly DependencyProperty LoadedProperty =
			DependencyProperty.RegisterAttached(
				"Loaded",
				typeof(bool),
				typeof(ItemData),
				new PropertyMetadata(false));

		public static bool GetLoaded(DependencyObject obj)
		{
			return (bool)obj.GetValue(LoadedProperty);
		}

		public static void SetLoaded(DependencyObject obj, bool value)
		{
			obj.SetValue(LoadedProperty, value);
		}

		private void LoadDefaultIcon()
		{
			var path = @"Assets/Desert.jpg";
			this.Icon = BitmapFrame.Create(new Uri(path, UriKind.Relative));
		}
	}

	/// <summary>
	/// 遅延読み込みをサポートするアイテム
	/// </summary>
	interface ILazyLoadingItem
	{
		bool IsLoaded { get; }
		event EventHandler Loaded;
	}

	/// <summary>
	/// データソース
	/// </summary>
	/// <typeparam name="T"></typeparam>
	interface IDataSource<T>
		where T : ICollection
	{
		T Items { get; }
	}

	/// <summary>
	/// ItemDataモデルのデータソース
	/// </summary>
	public class ItemDataDataSource : IDataSource<DispatcherCollection<ItemData>>, IDisposable
	{
		public ItemDataDataSource()
		{
			this._Items = new DispatcherCollection<ItemData>(DispatcherHelper.UIDispatcher);
		}

		/// <summary>
		/// 新しいItemDataを追加する
		/// </summary>
		/// <param name="item"></param>
		public void AddItem(ItemData item)
		{
			var listener = new EventListener<EventHandler>(
				h => item.Loaded += h,
				h => item.Loaded -= h,
				(sender, e) => { OnLazyDataLoad((ItemData)sender); }
			);
			_Listeners.Add(listener);
				
			this.Items.Insert(this.Items.Count, item);
		}

		public void Dispose()
		{
			foreach (var @d in _Listeners)
			{
				@d.Dispose();
			}
		}

		DispatcherCollection<ItemData> _Items;
		public DispatcherCollection<ItemData> Items { get { return _Items; } }

		ObservableSynchronizedCollection<EventListener<EventHandler>> _Listeners = new ObservableSynchronizedCollection<EventListener<EventHandler>>();

		async void OnLazyDataLoad(ItemData sender)
		{
			//Console.WriteLine("OnPropertyChangedIsLoaded " + sender.IdText);

			await DispatcherHelper.UIDispatcher.InvokeAsync(async () =>
			{
				var results = await this.GetData();

				sender.Label = results[0].Label;
				sender.Icon = results[0].Icon;
			});
		}

		// ネットワークの向こうのデータをとってくるイメージ
		private async Task<List<ItemData>> GetData()
		{
			var @rnd = new Random();
			return await Task.Delay(@rnd.Next(1,5) * 1000)
				.ContinueWith(_ =>
				{
					List<ItemData> l = new List<ItemData>();

					var r = new ItemData { Label = DateTime.Now.ToString() };
					var path = @"Assets/Penguins.jpg";
					r.Icon = BitmapFrame.Create(new Uri(path, UriKind.Relative));

					l.Add(r);

					return l;
				});
		}
	}
}
