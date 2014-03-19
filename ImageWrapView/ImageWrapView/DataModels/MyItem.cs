using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace ImageWrapView.DataModels
{
	public class MyItem : NotificationObject
	{

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


		#region IsVisible変更通知プロパティ
		private bool _IsVisible = false;

		public bool IsVisible
		{
			get
			{ return _IsVisible; }
			set
			{ 
				if (_IsVisible == value)
					return;
				_IsVisible = value;
				RaisePropertyChanged();
			}
		}
		#endregion

	}
}
