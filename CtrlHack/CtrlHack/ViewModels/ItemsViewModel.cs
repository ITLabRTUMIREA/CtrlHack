using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CtrlHack.Models;
using CtrlHack.Views;

namespace CtrlHack.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Verify> Items { get; set; }

        bool searchOpened = false;
        public bool SearchOpened 
        {
            get { return searchOpened; }
            set { SetProperty(ref searchOpened, value); }
        }

        public Command LoadItemsCommand { get; set; }
        public int Year = 2018;
        public string OrgName;
        public string Ogrn;
        public string Inn;
        public int? Subject = 77;

        public ItemsViewModel()
        {
            Title = "Список проверок";
            Items = new ObservableCollection<Verify>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public void UpdateParameters(int year, string orgName, string ogrn, string inn, int? subject)
        {
            Year = year;
            OrgName = orgName;
            Ogrn = ogrn;
            Inn = inn;
            Subject = subject;
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetVerifyesAsync(Year, OrgName, Ogrn, Inn, Subject);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}