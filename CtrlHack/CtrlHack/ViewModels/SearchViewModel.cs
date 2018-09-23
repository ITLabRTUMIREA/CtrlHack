using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CtrlHack.ViewModels
{
	public class SearchViewModel : BaseViewModel
	{
		public SearchViewModel ()
		{
            Title = "Поиск проверок123";
            

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }
        //public ICommand OpenWebCommand { get; }
	}
}