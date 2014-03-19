using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace ImageWrapView.DataModels
{
	public class WorkspaceViewModel : NotificationObject
	{
		public WorkspaceViewModel(){
			this._Items = new DispatcherCollection<MyItem>(DispatcherHelper.UIDispatcher);
		}

		public void InitializeItems()
		{
			this._Items.Clear();

			for (int i = 1; i < 100; i++)
			{
				this._Items.Add(new MyItem { Label = "MyLabel =" + i });
			}
		}

		DispatcherCollection<MyItem> _Items;
		public DispatcherCollection<MyItem> Items { get { return _Items; } }


		#region ItemWidth変更通知プロパティ
		private int _ItemWidth = 150;

		public int ItemWidth
		{
			get
			{ return _ItemWidth; }
			set
			{ 
				if (_ItemWidth == value)
					return;
				_ItemWidth = value;
				RaisePropertyChanged();
			}
		}
		#endregion

	}
}
