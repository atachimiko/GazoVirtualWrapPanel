using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Livet;
using ImageListView.DataModels;

namespace ImageListView
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DispatcherHelper.UIDispatcher = this.Dispatcher;
		
			this.Data = new MyData(this);
			this.DataContext = this.Data;

		}

		MyData Data;

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;
			textBox.ScrollToLine(textBox.LineCount - 1);
		}

		private void ScrollOffsetButton_Click(object sender, RoutedEventArgs e)
		{
			this.Data.ListViewSampleContainerVertialOffset += 10.0;
		}
	}


}
