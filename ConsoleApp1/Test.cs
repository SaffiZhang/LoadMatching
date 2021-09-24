using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class Test
    {
        public async Task<int> Run()
        {
            var i = 0;
            var j = 0;
            for ( i =0; i<900000000;i++)
            {
                j++;
                for (i = 0; i < 900000000; i++)
                {
                    j++;
                    for (i = 0; i < 900000000; i++)
                    {
                        j++;
                        for (i = 0; i < 900000000; i++)
                        {
                            j++;
                            for (i = 0; i < 900000000; i++)
                            {
                                j++;
                                for (i = 0; i < 900000000; i++)
                                {
                                    j++;
                                }
                            }

                        }
                    }
                }
            }
            Console.WriteLine("2---" + i.ToString() + "---" + DateTime.Now.ToString());
   
            return i;
        }
    }
}
