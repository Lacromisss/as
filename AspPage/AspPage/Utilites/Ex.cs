using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspPage.Utilites
{
    public static class Ex
    {
        public static bool ChechSize( this IFormFile formFile,int kb)
        {
            if (formFile.Length/1024*1024>kb)
            {
                return true;


            }
            return false;

        }
        public static bool CheckType( this IFormFile formFile,string typee)
        {
            if (formFile.ContentType.Contains("image/"))
            {
                return true;

            }
            return false;



        }
        public async static Task<string> SavaChacheAsync( this IFormFile formfile,string pathh)
        {
            string Musi= Guid.NewGuid().ToString()+formfile.FileName;
            string path = Path.Combine(pathh,Musi);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                await formfile.CopyToAsync(file);
            }
            return Musi;    


        }
    }
}
