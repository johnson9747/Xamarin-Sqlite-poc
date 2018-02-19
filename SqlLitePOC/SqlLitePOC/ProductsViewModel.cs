using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SqlLitePOC
{
	public class ProductsViewModel : INotifyPropertyChanged
	{
		private readonly IProductsRepository _productsRepository;
		private INavigation _navigation;
		private IEnumerable<Product> _products;
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
		public bool IsRefreshing { get; set; }				
		public ProductsViewModel(IProductsRepository productsRepository, INavigation navigation)
		{
			_navigation = navigation;
			IsRefreshing = false;
			_productsRepository = productsRepository;
			Products =  _productsRepository.GetProductsAsync().Result;
			MessagingCenter.Subscribe<AddProductsViewModel>(this, "Created", (sender) =>
			{
				Products =  _productsRepository.GetProductsAsync().Result;
			});

		}	
		public ICommand AddNewProductCommand
		{
			get
			{
				return new Command(async () =>
				{
					await _navigation.PushModalAsync(new AddProductsPage(_productsRepository)); // HERE
				});
			}
		}
		public ICommand DeleteCommand {
			get
			{
				return new Command(async (object obj) =>
				{
					var item = obj as Product;
					bool isDeleted = await _productsRepository.DeleteProductAsync(item.Id);
					if (isDeleted)
					{						
						Products = await _productsRepository.GetProductsAsync();
						MessagingCenter.Send(this, "Deleted");						
					}
				});
			}
		}
		public ICommand RefreshCommand
		{
			get
			{
				return new Command(async () =>
				{
					IsRefreshing = true;
					Products = await _productsRepository.GetProductsAsync();
					IsRefreshing = false;
					OnPropertyChanged("IsRefreshing");
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
