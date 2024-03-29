﻿using AppMinhasCompras.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMinhasCompras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        // Coleção Observável -> Array de objeto que atualiza a interface 
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();
        public Listagem()
        {
            InitializeComponent();

            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.NovoProduto());
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Preco * i.Quantidade);

            string msg = "O total da compra é: " + soma;


            // 1:24:29
            DisplayAlert("Total", msg, "OK");
        }

        protected override void OnAppearing()
        {
            if(lista_produtos.Count == 0)
            {
                // Na hora da tela da listagem aparecer, começa uma task paralela
                System.Threading.Tasks.Task.Run(async () =>
                {
                    List<Produto> temp = await App.Database.GetAll();

                    // Preenchendo a ObservableCollection
                    foreach (Produto item in temp)
                    {
                        lista_produtos.Add(item);
                    }

                    ref_carregando.IsRefreshing = false;
                });
            }

                    

            
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;
            Produto produto_selecionado = (Produto)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.Delete(produto_selecionado.Id);

                lista_produtos.Remove(produto_selecionado);
            }
        }

        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;
          
            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produto> temp = await App.Database.Search(buscou);

                lista_produtos.Clear();

                // Preenchendo a ObservableCollection
                foreach (Produto item in temp)
                {
                    lista_produtos.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = (Produto)e.SelectedItem
            });
        }
    }
}