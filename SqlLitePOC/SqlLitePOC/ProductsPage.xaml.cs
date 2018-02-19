using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlLitePOC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductsPage : ContentPage
	{
		public ProductsPage (IProductsRepository productsRepository)
		{
			InitializeComponent ();
			BindingContext = new ProductsViewModel(productsRepository,this.Navigation);			
			MessagingCenter.Subscribe<ProductsViewModel>(this, "Deleted", (sender) => {
				DisplayAlert("Success", "Deleted successfully", "OK");
			});
		}		
	}
}