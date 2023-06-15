using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Input;
using TopoLocoApp.Models;
using TopoLocoApp.Services;
using TopoLocoApp.Views;

namespace TopoLocoApp.ViewModels
{
    public class TopoLocoViewModel : INotifyPropertyChanged
    {
        public ICommand JugarCommand { get; set; }
        public ICommand GolpearTopoCommand { get; set; }
        public ICommand VerPuntuacionesCommand { get; set; }

        public HubConnection connection = new HubConnectionBuilder().WithUrl("https://topoloco.sistemas19.com/topolocoHub").Build();

        public ObservableCollection<Usuario> Usuarios { get; set; } = new ();
        public ObservableCollection<Usuario> PuntuacionesAltas { get; set; } = new();

        TopoLocoService service { get; set; } = new();

        public Usuario jugador { get; set; } = new Usuario();

        System.Timers.Timer timer = new();
        public int tiempo { get; set; } = 10;

        public Usuario myUser { get; set; } = new();

        public int Puntos { get; set; } = 0;

        public TopoLocoViewModel()
        {
            connection.StartAsync();
            GolpearTopoCommand = new Command(GolpearTopo);
            JugarCommand = new Command(Jugar);
            VerPuntuacionesCommand = new Command(VerPuntuaciones);
            timer.Interval = 1000;
            timer.Elapsed += Contar;
            connection.On("NuevaPuntuacion", () =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await CargarPuntuacionesAltas();
                    await CargarUsuarios();
                });
            });
            CargarUsuarios();
        }

        private void Contar(object sender, ElapsedEventArgs e)
        {
            if (tiempo == 0)
                JuegoTerminado();

            tiempo--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(tiempo)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void GolpearTopo()
        {
            Puntos++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Puntos)));
        }

        private async void JuegoTerminado()
        {
            timer.Stop();
            jugador.Puntaje = Puntos;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Tiempo terminado", "El tiempo ha terminado, tu puntuación fue: " + Puntos.ToString(), "Aceptar");
                if (!await service.Update(jugador))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Error al guardar el puntaje", "Ok");
                }
                //await connection.InvokeAsync("NuevaPuntuacion");
                await service.NuevaPuntuacion();
                
                await App.Current.MainPage.Navigation.PopAsync();
                Puntos = 0;
                tiempo = 10;
            });
            
        }

        private async void Jugar()
        {
            Usuario j = Usuarios.Where(x => x.NombreUsuario == jugador.NombreUsuario).FirstOrDefault();
            await CargarPuntuacionesAltas();
            if (j != null)
            {
                jugador = j;
                await App.Current.MainPage.Navigation.PushAsync(new JuegoView());
                tiempo = 10;
                timer.Start();
            }
            else
            {
                if(!await service.Insert(jugador))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Error al generar usuario", "Ok");
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new JuegoView());
                    tiempo = 10;
                    timer.Start();
                }
            }
        }

        private async Task CargarUsuarios()
        {
            Usuarios.Clear();
            var datos = await service.Get();
            datos.ForEach(x=>Usuarios.Add(x));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private async Task CargarPuntuacionesAltas()
        {
            PuntuacionesAltas.Clear();
            var datos = await service.Get();
            
            datos.OrderByDescending(x=>x.Puntaje).Take(5).ToList().ForEach(PuntuacionesAltas.Add);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PuntuacionesAltas)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void VerPuntuaciones()
        {
            CargarUsuarios();
            App.Current.MainPage.Navigation.PushAsync(new PuntosView());
        }
    }
}
