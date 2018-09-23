using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CtrlHack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public SearchPage ()
		{
			InitializeComponent ();
            SearchButton.Clicked += async (sender, e) =>
            {
                if (e == null)
                    return;

                var id = 1;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}