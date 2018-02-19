using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SqlLitePOC
{
	public partial class App : Application
	{
		public App (IProductsRepository productsRepository)
		{
			InitializeComponent();

			MainPage = new NavigationPage(new ProductsPage(productsRepository));
			//{
			//	BindingContext = new ProductsViewModel(productsRepository),
			//};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
