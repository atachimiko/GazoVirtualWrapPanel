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
using ImageWrapView.DataModels;
using Livet;
using ImageWrapView.Utils;

namespace ImageWrapView
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

			viewModel = new WorkspaceViewModel();
			this.DataContext = viewModel;

			viewModel.InitializeItems();
		}

		WorkspaceViewModel viewModel;

		private void VisibleButton_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Items[0].IsVisible = true;
		}

		private void ShowItemsControlMessageButton_Click(object sender, RoutedEventArgs e)
		{
			this.MessageTextBox.Text = thumbnailListBox.GetMessage("Show GeneratorPosition");
		}
	}
}
