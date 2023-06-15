namespace TopoLocoApp.Views;

public partial class JuegoView : ContentPage
{
    public JuegoView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }



    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        btnimgtopo.Source = "/Resources/Images/topogolpeado.png";         
    }
}