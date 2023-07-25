namespace KitabxanaAPP.Addons
{
    public class TypeWriter
    {
        public static void TypeWriterEffect(string message)
        {
            for (int i = 0; i < message.Length; i++)

            {

                Console.Write(message[i]);

                System.Threading.Thread.Sleep(15);

            }
        }
    }
}
