namespace TopoLocoApp.Views;

public partial class PuntosView : ContentPage
{
	public PuntosView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }
}