using AppMinhasCompras.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMinhasCompras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {

                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = double.Parse(txt_quantidade.Text),
                    Preco = double.Parse(txt_preco.Text),
                };

                await App.Database.Insert(p);
                await DisplayAlert("Sucesso!", "Produto cadastrado.", "OK");
                await Navigation.PushAsync(new Listagem());

            }catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}