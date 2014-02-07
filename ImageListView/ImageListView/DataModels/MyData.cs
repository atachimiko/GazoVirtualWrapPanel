using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageListView.Utils;
using ImageListView.Supports;

namespace ImageListView.DataModels
{

	public class MyData : NotificationObject
	{
		public MyData(MainWindow mainWindow)
		{
			this._MainWindow = mainWindow;

			// 初期データ
			for (int i = 0; i < 30; i++)
			{
				this.ListItems.Add(new ItemData { Label = "初期値" + i });
			}

			_MainWindow.ListViewSampleContainer.ItemsSource = ListItems;
		}

		/// <summary>
		/// 
		/// </summary>
		public void AddItem()
		{
			int pos = this.ListItems.Count + 1;
			this.ListItems.Insert(this.ListItems.Count, new ItemData { Label = "追加 " + pos });

			if (ListViewSampleContainerVertialOffset == lastScrollVerticalOffset)
				ListViewSampleContainerVertialOffset = lastScrollVerticalOffset + 0.1;
			else
				ListViewSampleContainerVertialOffset = lastScrollVerticalOffset;
		}

		/// <summary>
		/// GeneratorPositionの状態を出力します
		/// </summary>
		public void ShowGeneratorPosition()
		{
			this.Message += _MainWindow.ListViewSampleContainer.GetMessage("Show GeneratorPosition");
		}

		/// <summary>
		/// 表示中のメッセージをクリアします
		/// </summary>
		public void MessageClear()
		{
			this.Message = string.Empty;
		}

		public void Scrolling()
		{
			Console.WriteLine("Scrolling");
			var @scroll = this._MainWindow.ListViewSampleContainer.GetScrollViewer();
			Console.WriteLine("スクロール位置 " + @scroll.VerticalOffset);

			lastScrollVerticalOffset = @scroll.VerticalOffset;
		}
		double lastScrollVerticalOffset = 0.0;

		#region プロパティ
		#region ListViewSampleContainerVertialOffset変更通知プロパティ
		private double _ListViewSampleContainerVertialOffset;

		public double ListViewSampleContainerVertialOffset
		{
			get
			{ return _ListViewSampleContainerVertialOffset; }
			set
			{
				Console.WriteLine("ListViewSampleContainerVertialOffset.Value = {0}", value);
				if (_ListViewSampleContainerVertialOffset == value)
					return;
				_ListViewSampleContainerVertialOffset = value;
				RaisePropertyChanged();
			}
		}
		#endregion


		#region Message変更通知プロパティ
		private string _Message;

		public string Message
		{
			get
			{ return _Message; }
			set
			{
				if (_Message == value)
					return;
				_Message = value;
				RaisePropertyChanged();
			}
		}
		#endregion
		#endregion
		ObservableSynchronizedCollection<ItemData> ListItems = new ObservableSynchronizedCollection<ItemData>();
		MainWindow _MainWindow;
	}
}
