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
        public Command LoadItemsCommand { get; set; }
        public int Year;
        public string OrgName;
        public string Ogrn;
        public string Inn;
        public int? Subject;


        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Verify>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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