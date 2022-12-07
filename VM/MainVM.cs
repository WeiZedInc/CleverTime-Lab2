using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CleverTime.VM
{
    public partial class MainVM : ObservableObject
    {
        public ObservableCollection<TTimer> AllTimers { get; set; } = new();
        public ObservableCollection<string> Groups { get; set; } = new();


        public static bool isGroupsEmpty()
        {
            var sHelper = ServiceHelper.GetService<MainVM>(); // через хелпера юзать вызывать всё
            return sHelper.Groups.Count == 1 ? true : false;
        }

        public MainVM()
        {
            Groups.Add(TTimer.DEFAULT_GROUP);
            AllTimers.Add(new TTimer());
            AllTimers.Add(new TTimer(name: "test3"));
        }
    }
}
