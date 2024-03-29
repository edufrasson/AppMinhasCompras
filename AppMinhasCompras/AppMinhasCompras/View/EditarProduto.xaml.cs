﻿using AppMinhasCompras.Model;
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
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto produto = BindingContext as Produto;
                Produto p = new Produto
                {
                    Id = produto.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = double.Parse(txt_quantidade.Text),
                    Preco = double.Parse(txt_preco.Text),
                };

                await App.Database.Update(p);
                await DisplayAlert("Sucesso!", "Produto atualizado.", "OK");
                await Navigation.PushAsync(new Listagem());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}