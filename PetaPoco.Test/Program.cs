using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace PetaPoco.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var sql = Sql.Builder.Where("1=1").Where("2=2").OrderBy("3").Where("4=4");
            Console.WriteLine(Sql.Builder.SQL);

        }
    }
}
