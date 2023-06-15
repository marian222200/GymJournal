using CommunityToolkit.Mvvm.ComponentModel;

namespace GymJournal.App.ViewModel
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsNotBusy))]
		bool isBusy;
		[ObservableProperty]
		string title;

		public bool IsNotBusy => !IsBusy;

		public BaseViewModel() { }
	}
}
