using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlHack.Models
{
    public enum MenuItemType
    {
        Inspections,
        Search
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
