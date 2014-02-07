using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageListView.Supports
{
	public static class UIElementExtensions
	{
		public static ScrollViewer GetScrollViewer(this UIElement uiParent)
		{
			int nCount = VisualTreeHelper.GetChildrenCount(uiParent);

			try
			{
				for (int i = 0; i < nCount; ++i)
				{
					UIElement uielement = VisualTreeHelper.GetChild(uiParent, i) as UIElement;
					if (uielement.GetType() == typeof(System.Windows.Controls.ScrollViewer))
					{
						return (ScrollViewer)uielement;
					}
					ScrollViewer scrollviewer = GetScrollViewer(uielement);
					if (scrollviewer != null) return scrollviewer;
				}
			}

			catch (Exception ex)
			{
			}

			return null;
		}
	}
}
