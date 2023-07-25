using KitabxanaAPP.Addons;
using KitabxanaAPP.DataModels;

namespace KitabxanaAPP.Helpers
{
    public static partial class Helper
    {
        public static int ReadInt(string caption)
        {
            int value;
            var color = Console.ForegroundColor;

        l1:


            Console.Write(caption);





            if (!int.TryParse(Console.ReadLine(), out value))
            {

                Console.ForegroundColor = ConsoleColor.Red;
                TypeWriter.TypeWriterEffect("\n[!] Xəta baş verdi [Hərf və ya mətn daxil etmək qadağandır].\n\n");
                Console.ForegroundColor = color;
                goto l1;
            }

            return value;

        }

        public static decimal ReadDecimal(string caption)
        {
            decimal value;
            var color = Console.ForegroundColor;

        l1:

            
            Console.Write(caption);
            



            if (!decimal.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }

            return value;

        }

        public static ushort ReadShort(string caption)
        {
            ushort value;
            var color = Console.ForegroundColor;

        l1:

            
            Console.Write(caption);
            



            if (!ushort.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }

            return value;

        }

        public static string ReadString(string caption)
        {
            string value;
            var color = Console.ForegroundColor;

        l1:

            Console.Write(caption);


            value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                goto l1;
            }

            return value;
        }



    }
}
