using System;

using CtrlHack.Models;

namespace CtrlHack.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Verify Item { get; set; }
        public ItemDetailViewModel(Verify item = null)
        {
            Title = item?.OrgName;
            Item = item;
        }
    }
}
