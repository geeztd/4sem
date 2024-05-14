using servic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Data;
using System.IO;
using System.Text.Json;

namespace servic.Products
{
    public class ProductStoreViewModel
    {

        public BindingList<Product> Products { get; set; }
        public ICommand ShowDetailsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand SaveInFileCommand { get; }
        public ICommand ForwordCommand { get; }
        public ICommand BackCommand { get; }


        public ObservableCollection<Product> filtered { get; set; }

        public ICommand SetLanguageCommand { get; }

        public void updateFilter()
        {
            filtered.Clear();
            foreach (var item in Products)
            {
                filtered.Add(item);
            }
        }

        public void SetLanguage(object languageCode)
        {
            var dict = new ResourceDictionary();
            switch (languageCode.ToString())
            {
                case "ru":
                    dict.Source = new Uri("/Languages/Russian.xaml", UriKind.Relative);
                    break;
                case "en":
                    dict.Source = new Uri("/Languages/English.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries[0] = dict;
        }

        public void SaveInFile(object item)
        {
            using (StreamWriter sw = new("file.json"))
            {

                foreach (var pr in Products)
                {
                    string str = pr.Name + " ";
                    str += pr.ShortName + " ";
                    str += pr.Description + " ";
                    str += pr.category.ToString() + " ";
                    str += pr.imgPath + " ";
                    str += pr.Cost.ToString() + " ";
                    str += pr.Creater;
                    sw.WriteLine(str);
                }

            }
            ResourceDictionary dict = Application.Current.Resources;


            MessageBox.Show(dict["Save"].ToString());
        }

        public void ShowDetails(object item)
        {
            if (item is Product product)
            {
                ProductWindow window = new ProductWindow(product, this);
                window.Show();
            }
        }
        private Stack<BindingList<Product>> backStack = new Stack<BindingList<Product>>();
        private Stack<BindingList<Product>> forwardStack = new Stack<BindingList<Product>>();

        public void AddProduct(Product product)
        {
            var arr = new BindingList<Product>();
            foreach (var item in Products)
            {
                arr.Add(item);
            }
            backStack.Push(arr);
            forwardStack.Clear();
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            var arr = new BindingList<Product>();
            foreach (var item in Products)
            {
                arr.Add(item);
            }
            backStack.Push(arr);
            forwardStack.Clear();
            Products.Remove(product);
        }

        //public void UpdateProduct(Product oldProduct, Product newProduct)
        //{
        //    backStack.Push(new BindingList<Product>(Products));
        //    forwardStack.Clear();
        //    int index = Products.IndexOf(oldProduct);
        //    if (index != -1)
        //    {
        //        Products[index] = newProduct;
        //    }
        //}

        public void GoBack(object item)
        {
            if (backStack.Count > 0)
            {
                var arr = new BindingList<Product>();
                foreach (var pr in Products)
                {
                    arr.Add(pr);
                }
                forwardStack.Push(arr);
                Products = backStack.Pop();
                updateFilter();
            }
        }

        public void GoForward(object item)
        {
            if (forwardStack.Count > 0)
            {
                var arr = new BindingList<Product>();
                foreach (var pr in Products)
                {
                    arr.Add(pr);
                }
                backStack.Push(arr);
                Products = forwardStack.Pop();
                updateFilter();
            }

        }

        public void AddProduct(object item)
        {
            AddWindow window = new AddWindow(this);
            window.Show();
        }
        public ProductStoreViewModel()
        {
            SetLanguageCommand = new RelayCommand(SetLanguage);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            AddProductCommand = new RelayCommand(AddProduct);
            SaveInFileCommand = new RelayCommand(SaveInFile);
            ForwordCommand = new RelayCommand(GoForward);
            BackCommand = new RelayCommand(GoBack);

            filtered = new ObservableCollection<Product>();


            Products = new BindingList<Product> {
                new Product()
            {
                Name = "Кроссовки баскетбольные NIKE КD TREY 5",
                ShortName = "КD TREY 5",
                Description = "Кроссовки баскетбольные NIKE КD TREY 5 IX CW3400-400",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/trey.jpg")),
                category = Category.basketboll,
                Cost = 49.99f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/trey.jpg",
            },
            new Product()
            {
                Name = "Кроссовки баскетбольные NIKE KD14 D06903-400",
                ShortName = "KD14",
                Description = "Кроссовки KD14 созданы для таких универсальных игроков, как Кевин Дюрант, и помогут сохранить точность движений на протяжении всей игры.\r\n\r\nМногослойная сетка верха и ремешок в средней части стопы позволяют снизить нагрузку на стопу, а также создает ощущение комфорта с первых секунд игры. Дополнительные уплотнения в ключевых зонах надежно фиксируют стопу.\r\n\r\nАмортизирующая подушка Zoom Air и пеноматериал Cushlon усиливают амортизацию, создают ощущение мягкости и комфорта.\r\n\r\nНеобычный рисунок подошвы усиливает сцепление с поверхностью во время движения. Под сводом стопы расположена жесткая вставка для дополнительной стабилизации.",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/KD14.jpg")),
                category = Category.basketboll,
                Cost = 59.99f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/KD14.jpg"
            },
            new Product()
            {   Name = "Кроссовки баскетбольные NIKE JORDAN “WHY NOT?” ZER0.3 SE CK6611-101",
                ShortName = "“WHY NOT?” ZER0.3",
                Description = "Расселлу Уэстбруку нужны были легкие кроссовки с надежной фиксацией, которые бы выглядели невероятно быстрыми. Кроссовки Jordan “Why Not?” Zer0.3 SE легче и ниже, чем модель Zer0.3, а их мягкий бортик обеспечивает комфортную поддержку голеностопа. Благодаря мгновенной амортизации в передней части стопы Расс превращает свое бесстрашие в непобедимую силу.\r\n\r\nЛегкость и низкий профиль\r\n\r\nВ модели нет ремешка, бортик стал ниже: мы убрали все лишние детали, чтобы сделать кроссовки максимально легкими.\r\n\r\nУпругость для Расса\r\n\r\nБольшая вставка Air Zoom Turbo под подушечками стопы проходит вдоль параллельных линий и была разработана специально для Расса. Она обеспечивает посегментную амортизацию в разные моменты движения стопы.",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/why.jpg")),
                category = Category.basketboll,
                Cost = 53.89f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/why.jpg"
            },
            new Product()
            {
                Name = "Кроссовки волейбольные NIKE AIR ZOOM HYPERACE 2 SE DM8199-170",
                ShortName = "AIR ZOOM HYPERACE 2",
                Description = "Кроссовки Nike Air Zoom Hyperace 2 выполнены с прочной конструкцией и отлично подойдут для игры на твердых покрытиях. Специально разработанный легкий текстильный верх обеспечивает вентиляцию внутри обуви.",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/race.jpg")),
                category = Category.volleyboll,
                Cost = 35.10f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/race.jpg"
            },
            new Product()
            {
                Name = "Кроссовки волейбольные NIKE ZOOM HYPERSPEED COURT SE DJ4476-064",
                ShortName = "ZOOM HYPERSPEED COURT",
                Description = "Волейбольные кроссовки Nike Hyperspeed Court дают вам все, что может предложить хорошая спортивная обувь. Независимо от того, делаете ли вы много быстрых коротких движений или постоянно прыгаете в высоту, эта обувь дает вашим ногам поддержку и еще больше энергии. Благодаря особому материалу верха обувь, они отличается особой прочностью, поэтому не только вы, но и ваша обувь прослужат дольше",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/speed.jpg")),
                category = Category.volleyboll,
                Cost = 69.99f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/speed.jpg"
            },
            new Product()
            {
                Name = "Кроссовки волейбольные NIKE REACT HYPERSET DJ4473 121",
                ShortName = "REACT HYPERSET",
                Description = "Кроссовки для игры в волейбол Nike React Hyperset обеспечивают отличное сцепление, что позволяет развивать максимальную скорость и резко останавливаться, уверенно прыгать и приземляться. Данная обувь отлично подойдет для различных видов спорта в закрытом помещении.",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/set.jpg")),
                category = Category.volleyboll,
                Cost = 99.99f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/set.jpg"
            },
            new Product()
            {
                Name = "Штангетки NIKE NIKE ROMALEOS 4",
                ShortName = "ROMALEOS 4",
                Description = "Штангетки NIKE NIKE ROMALEOS 4 AMP CD6452-060",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/romaleos.jpg")),
                category = Category.weightlifting,
                Cost = 45.59f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/romaleos.jpg"
            },
            new Product()
            {
                Name = "Штангетки NIKE SAVALEOS",
                ShortName = "SAVALEOS",
                Description = "Штангетки NIKE SAVALEOS CV5708-010",
                bitmapImage = new BitmapImage(new Uri("D:/stady/labs 2/C#/servic/sours/savaleos.jpg")),
                category = Category.weightlifting,
                Cost = 51.29f,
                Creater = "Nike",
                imgPath = "D:/stady/labs 2/C#/servic/sours/savaleos.jpg"
            },
            };
            updateFilter();
        }
    }
}
