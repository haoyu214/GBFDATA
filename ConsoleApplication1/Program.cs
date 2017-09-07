using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Invoked iA = new AChildInvoked();
            Invoked iAA = new AAChildInvoked();
            AInvoked iAAA = new AChildInvoked();
            AInvoked iAAAA = new AAChildInvoked();

            iA.GetMessage();
            iAA.GetMessage();
            iAAA.GetMessage();
            iAAAA.GetMessage();
            Console.ReadLine();



        }
    }


    public interface Invoked
    {
         void  GetMessage();
             
    }


    public class AInvoked:Invoked
    {
         public virtual void GetMessage()

        {
            Console.WriteLine("AInvoked is  write");
        }

    }

    public class BInvoked:Invoked
    {
        public  void GetMessage()

        {
            Console.WriteLine("BInvoked is  write");
        }

    }
    public class AChildInvoked:AInvoked
    {
        public new void GetMessage()

        {
            Console.WriteLine("AChildInvoked is  write");
        }

    }
    public class AAChildInvoked : AInvoked
    {
        public override void GetMessage()

        {
            Console.WriteLine("AAChildInvoked is  write");
        }

    }

    public class BChildInvoked:BInvoked
    {
        public void GetMessage()

        {
            Console.WriteLine("BChildInvoked is  write");
        }

    }

    public class BBChildInvoked : BInvoked
    {
        public new void GetMessage()

        {
            Console.WriteLine("BBChildInvoked is  write");
        }

    }
}
