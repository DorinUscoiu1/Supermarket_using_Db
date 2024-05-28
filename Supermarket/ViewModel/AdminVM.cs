using System.Windows;
using System.Windows.Input;
using Supermarket.View;

namespace Supermarket.ViewModel
{
    public class AdminVM
    {
        public ICommand OpenUsersCommand { get; }
        public ICommand OpenCategoriesCommand { get; }
        public ICommand OpenManufacturersCommand { get; }
        public ICommand OpenProductsCommand { get; }
        public ICommand OpenStocksCommand { get; }

        public AdminVM()
        {
            OpenUsersCommand = new RelayCommand(OpenUsers);
            OpenCategoriesCommand = new RelayCommand(OpenCategories);
            OpenManufacturersCommand = new RelayCommand(OpenManufacturers);
            OpenProductsCommand = new RelayCommand(OpenProducts);
            OpenStocksCommand = new RelayCommand(OpenStocks);
        }

        private void OpenUsers(object parameter)
        {
            var usersView = new UserView();
            usersView.DataContext = new UserVM();
            usersView.Show();
        }

        private void OpenCategories(object parameter)
        {
            var categoriesView = new CategoriiView();
            categoriesView.DataContext = new CategoriiVM();
            categoriesView.Show();
        }

        private void OpenManufacturers(object parameter)
        {
            var manufacturersView = new ProducatoriView();
            manufacturersView.DataContext = new ProducatoriVM();
            manufacturersView.Show();
        }

        private void OpenProducts(object parameter)
        {
            var productsView = new ProduseView();
            productsView.DataContext = new ProduseVM();
            productsView.Show();
        }

        private void OpenStocks(object parameter)
        {
            var stocksView = new StocuriView();
            stocksView.DataContext = new StocuriVM();
            stocksView.Show();
        }
    }
}
