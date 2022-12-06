using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CleverTime
{
    public partial class MainVM : ObservableObject
    {
        public ObservableCollection<TTimer> AllTimers { get; set; } = new();
        public ObservableCollection<string> Groups { get; set; } = new();


        public static bool isGroupsEmpty()
        {
            var sHelper = ServiceHelper.GetService<MainVM>();
            return sHelper.Groups.Count == 1 ? true : false;
        }

        public MainVM()
        {
            Groups.Add("123");
            Groups.Add("3232");
        }
    }
}
