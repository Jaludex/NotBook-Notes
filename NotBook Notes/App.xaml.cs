namespace NotBook_Notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Posible inicio de sesion en la ventana que dejaste la ultima vez
            //MainPage = new AppShell();

            Current!.MainPage = new Views.AppShell();
        }
    }
}
