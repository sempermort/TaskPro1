using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro1.Models;

namespace TaskPro1.Helpers
{
        public static class RecentPlacesStore
        {
            public static ObservableCollection<PlaceAutoCompletePrediction> RecentPlaces { get; } =
                new ObservableCollection<PlaceAutoCompletePrediction>
                {
                new PlaceAutoCompletePrediction
                {
                    PlaceId = "ChIJq0wAE_CJr44RtWSsTkp4ZEM",
                    StructuredFormatting = new StructuredFormatting
                    {
                        MainText = "Random Place",
                        SecondaryText = "Paseo de los locutores #32"
                    }
                },
                new PlaceAutoCompletePrediction
                {
                    PlaceId = "ChIJq0wAE_CJr44RtWSsTkp4ZEM",
                    StructuredFormatting = new StructuredFormatting
                    {
                        MainText = "Green Tower",
                        SecondaryText = "Ensanche Naco #4343, Green 232"
                    }
                },
                new PlaceAutoCompletePrediction
                {
                    PlaceId = "ChIJm02ImNyJr44RNs73uor8pFU",
                    StructuredFormatting = new StructuredFormatting
                    {
                        MainText = "Tienda Aurora",
                        SecondaryText = "Rafael Augusto Sanchez"
                    }
                }
                };
        }
    }


