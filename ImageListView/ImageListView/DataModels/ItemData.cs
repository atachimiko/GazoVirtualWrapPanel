using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using System.Windows.Media.Imaging;

namespace ImageListView.DataModels
{
	public class ItemData : NotificationObject
	{

		public ItemData()
		{
			var path = @"D:\Desktop\SpringFsデータサンプル\[緊急避難] 妹との日常\00.jpg";
			this.Icon = BitmapFrame.Create(new Uri(path, UriKind.Relative));
		}

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
	}
}
