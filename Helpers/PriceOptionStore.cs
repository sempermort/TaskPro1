using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro1.Models;

namespace TaskPro1.Helpers
{
    public static class PriceOptionsStore
    {
        public static ObservableCollection<PriceOption> PriceOptions { get; } =
            new ObservableCollection<PriceOption>
            {
                new PriceOption
                {
                    Tag = "xUBERX",
                    Category = "Economy",
                    CategoryDescription = "Affordable, everyday rides",
                    PriceDetails = new List<Models.PriceDetail>
                    {
                        new PriceDetail
                        {
                            Type = "xUber X",
                            Price = 332,
                            ArrivalETA = "12:00pm",
                            Icon = "https://carcody.com/wp-content/uploads/2019/11/Webp.net-resizeimage.jpg"
                        },
                        new PriceDetail
                        {
                            Type = "xUber Black",
                            Price = 150,
                            ArrivalETA = "12:40pm",
                            Icon = "https://i0.wp.com/uponarriving.com/wp-content/uploads/2019/08/uberxl.jpg"
                        }
                    }
                },
                new PriceOption
                {
                    Tag = "xUBERXL",
                    Category = "Extra Seats",
                    CategoryDescription = "Affordable rides for groups up to 6",
                    PriceDetails = new List<PriceDetail>
                    {
                        new PriceDetail
                        {
                            Type = "xUber XL",
                            Price = 332,
                            ArrivalETA = "12:00pm",
                            Icon = "https://i0.wp.com/uponarriving.com/wp-content/uploads/2019/08/uberxl.jpg"
                        }
                    }
                }
            };
    }
}


