using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverTime
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        public static ObservableCollection<TTimer> allTimers;
        [ObservableProperty]
        public static List<string> groups;


        public static bool isGroupsEmpty() => groups.Count == 1 ? true : false;

        static MainVM()
        {
            allTimers = new();
            groups = new() { TTimer.DEFAULT_GROUP };
        }

    }
}
