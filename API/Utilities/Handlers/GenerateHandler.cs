using API.Contracts;
using API.Models;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {

        public static string LastNik(string nik)
        {
            string defaultNIK = "111111";
            if(nik == null)
            {
                return defaultNIK;
            }
            var newNIK = Convert.ToInt32(nik)+1;
            
            return newNIK.ToString();
        }
    }

}

