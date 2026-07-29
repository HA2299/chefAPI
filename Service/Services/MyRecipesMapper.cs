using AutoMapper;
using Repository.Entities;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyRecipesMapper : Profile
    {
        string path = Directory.GetCurrentDirectory() + "\\images_recipes\\";
        //public MyRecipesMapper()
        //{
        //    CreateMap<Recipe, RecipeDto>().ForMember("Image", x => x.MapFrom(y => fromStringToByte(y.ImageUrl)));
        //    CreateMap<RecipeDto, Recipe>().ForMember("ImageUrl", x => x.MapFrom(y => y.FileImage.FileName));
        //}
        public MyRecipesMapper()
        {
            // משנים את המיפוי כדי להתעלם מהשדה Image בזמן המיפוי האוטומטי
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            // המיפוי ההפוך נשאר כפי שהוא (או לפי הצורך שלך)
            CreateMap<RecipeDto, Recipe>()
                .ForMember(dest => dest.ImageUrl, x => x.MapFrom(y => y.FileImage.FileName));
        }
        //public byte[] fromStringToByte(string mypath)
        //{
        //    string filePath;

        //    if (string.IsNullOrEmpty(mypath))
        //    {
        //        filePath = Path.Combine(path, "default.jpg");
        //    }
        //    else
        //    {
        //        filePath = Path.Combine(path, mypath);
        //    }

        //    if (!File.Exists(filePath))
        //    {
        //        throw new FileNotFoundException("The image file was not found.", filePath);
        //    }

        //    return File.ReadAllBytes(filePath);
        //}


        public async Task<byte[]> fromStringToByteAsync(string mypath)
        {
            if (string.IsNullOrEmpty(mypath))
            {
                return await File.ReadAllBytesAsync(Path.Combine(path, "default.jpg"));
            }

            if (mypath.StartsWith("http"))
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        // כאן אנחנו עושים await ישירות על הבקשה
                        return await client.GetByteArrayAsync(mypath);
                    }
                }
                catch
                {
                    return await File.ReadAllBytesAsync(Path.Combine(path, "default.jpg"));
                }
            }

            string filePath = Path.Combine(path, mypath);
            return File.Exists(filePath) ? await File.ReadAllBytesAsync(filePath) : await File.ReadAllBytesAsync(Path.Combine(path, "default.jpg"));
        }
    }
}
