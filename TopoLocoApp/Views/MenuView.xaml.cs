namespace TopoLocoApp.Views;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
		InitializeComponent();
		BindingContext = App.ViewModel;
	}
}