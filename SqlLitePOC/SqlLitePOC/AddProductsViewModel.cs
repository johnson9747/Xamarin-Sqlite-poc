using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SqlLitePOC
{
    public class AddProductsViewModel: INotifyPropertyChanged
	{
		private IEnumerable<Product> _products;
		private readonly IProductsRepository _productsRepository;
		private INavigation _navigation;
		public string ProductPrice { get; set; }		
		public string ProductTitle { get; set; }
		public IEnumerable<Product> Products
		{
			get
			{
				return _products;
			}
			set
			{
				_products = value;
				OnPropertyChanged();
			}
		}

		public AddProductsViewModel(IProductsRepository productsRepository, INavigation navigation)
		{
			_navigation = navigation;			
			_productsRepository = productsRepository;			

		}
		public ICommand AddCommand
		{
			get
			{
				return new Command(async () =>
				{
					var product = new Product
					{
						Title = ProductTitle,
						Price = Convert.ToDouble(ProductPrice),
					};
					bool isAdded = await _productsRepository.AddProductsAsync(product);
					if (isAdded)
					{						
						MessagingCenter.Send(this, "Created");
						await _navigation.PopModalAsync(true);
					}
				});
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}		
	}
}
