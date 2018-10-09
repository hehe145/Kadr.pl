using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Figura
    {
        public virtual void  pole()
        {
            Console.WriteLine("figura pole");
        }
        public void a()
        {
            Console.WriteLine("figura a");
        }

        public sealed void b()
        {
            Console.WriteLine("figura b");
        }
        public abstract void c();
    }
}
