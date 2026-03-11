using AutoMapper;
using Repository.Entities;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyRecipesMapper : Profile
    {
        string path = Directory.GetCurrentDirectory() + "\\images_recipes\\";
        public MyRecipesMapper()
        {
            CreateMap<Recipe, RecipeDto>().ForMember("Image", x => x.MapFrom(y => fromStringToByte(y.ImageUrl)));
            CreateMap<RecipeDto, Recipe>().ForMember("ImageUrl", x => x.MapFrom(y => y.FileImage.FileName));
        }
        public byte[] fromStringToByte(string mypath)
        {
            string filePath;

            if (string.IsNullOrEmpty(mypath))
            {
                filePath = Path.Combine(path, "default.jpg");
            }
            else
            {
                filePath = Path.Combine(path, mypath);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The image file was not found.", filePath);
            }

            return File.ReadAllBytes(filePath);
        }

    }
}
