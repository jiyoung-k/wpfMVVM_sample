using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFMVVM
{
    public class Currency
    {
        public string Title { get; set; }
        public decimal Rate { get; set; }

        public Currency(string title, decimal rate)
        {
            Title = title;
            Rate = rate;
        }
    }

    public class CurrencyConverterViewModel:Notifier
    {
        private decimal euros;
        public decimal Euros
        {
            get { return euros; }
            set
            {
                euros = value;
                OnPropertyChanged("Euros");
                OnEurosChanged();
            }
        }

        private decimal converted;
        public decimal Converted
        {
            get { return converted; }
            set
            {
                converted = value;
                OnPropertyChanged("Converted");
            }
        }

        private Currency selectedCurrency;
        public Currency SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                selectedCurrency = value;
                OnPropertyChanged("SelectedCurrency");
                OnSelectedCurrencyChanged();
            }
        }

        private IEnumerable<Currency> currencies;
        public IEnumerable<Currency> Currencies
        {
            get { return currencies; }
            set
            {
                currencies = value;
                OnPropertyChanged("Currencies");
            }
        }

        private string resultText;
        public string ResultText
        {
            get { return resultText; }
            set
            {
                resultText = value;
                OnPropertyChanged("ResultText");
            }
        }

        private string resultRate;
        public string ResultRate
        {
            get { return resultRate; }
            set
            {
                resultRate = value;
                OnPropertyChanged("ResultRate");
            }
        }

        public ICommand MyCommand { get; set; }

        private bool CanExecute(object arg)
        {
            return true;
        }

        private void Execute(object obj)
        {
            string clickMSG = "use ICommand button ...... ";
            if (selectedCurrency != null) clickMSG += selectedCurrency.Title;

            MessageBox.Show(clickMSG);
        }

        public ICommand TestCommand { get; set; }

        private bool TestCanExecute(object arg)
        {
            return true;
        }

        private void TestExecute(object arg)
        {
            string test = "testCommand!!!!!!!";
            MessageBox.Show(test);
        }

        public CurrencyConverterViewModel()
        {
            MyCommand = new Command(Execute, CanExecute);
            TestCommand = new Command(TestExecute, TestCanExecute);

            Currencies = new Currency[]
            {
                new Currency("US Dollar", 1.1M),
                new Currency("British Pound", 0.9M)
            };
        }

        private void OnEurosChanged()
        {
            ComputeConverted();
        }

        private void OnSelectedCurrencyChanged()
        {
            ComputeConverted();
        }

        private void ComputeConverted()
        {
            if (SelectedCurrency == null)
            {
                return;
            }

            Converted = Euros * SelectedCurrency.Rate;
            ResultText = string.Format("Amount in {0}", selectedCurrency.Title);
            ResultRate = string.Format("Rate is {0}", selectedCurrency.Rate);
        }
    }
}
