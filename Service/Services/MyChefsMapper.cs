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
    public class MyChefsMapper : Profile
    {
        string path = Directory.GetCurrentDirectory() + "\\images_chefs\\";
        public MyChefsMapper()
        {
            CreateMap<Chef, ChefDto>().ForMember("Image", x => x.MapFrom(y => fromStringToByte(y.ImageUrl)));
            CreateMap<ChefDto, Chef>().ForMember("ImageUrl", x => x.MapFrom(y => y.FileImage.FileName));
        }
        public byte[] fromStringToByte(string mypath)
        {
            string filePath;

            if (string.IsNullOrEmpty(mypath))
            {
                // אם ה-ImageUrl ריק, השתמש בתמונה ברירת מחדל
                filePath = Path.Combine(path, "default.jpg"); // הנח שהקובץ הוא default.jpg
            }
            else
            {
                filePath = Path.Combine(path, mypath);
            }

            // בדוק אם הקובץ קיים לפני קריאת התוכן
            if (!File.Exists(filePath))
            {
                // אם הקובץ לא קיים, ניתן להחזיר תמונה ברירת מחדל או לזרוק שגיאה
                throw new FileNotFoundException("The image file was not found.", filePath);
            }

            return File.ReadAllBytes(filePath);
        }

    }
}
