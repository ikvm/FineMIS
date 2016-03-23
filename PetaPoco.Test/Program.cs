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
            var sql = Sql.Builder.Append("SET a = 1, b = 2");
            var fsql = Sql.Builder.Append("SET c = 3, d = 4").Where("e = 5").Where("f = 6");
            Console.WriteLine(sql.Append(fsql.SQL));

        }
    }
}
