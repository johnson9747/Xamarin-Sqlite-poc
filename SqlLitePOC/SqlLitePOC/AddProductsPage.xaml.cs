using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SqlLitePOC
{
	public partial class AddProductsPage : ContentPage
	{		
		public AddProductsPage(IProductsRepository productsRepository)
		{
			InitializeComponent();
			BindingContext = new AddProductsViewModel(productsRepository, this.Navigation);
			MessagingCenter.Subscribe<AddProductsViewModel>(this, "Created", (sender) => {
				DisplayAlert("Success", "Created successfully", "OK");
			});

		}
	}
}
