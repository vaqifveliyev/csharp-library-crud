using KitabxanaAPP.Addons;
using KitabxanaAPP.DataModels;
using KitabxanaAPP.Helpers;
using KitabxanaAPP.Infastructure;
using KitabxanaAPP.StableModels;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace KitabxanaAPP
{
    internal class Program
    {
        const string dataFilePath = "database.dat";
        static GenericStore<Author> author = new GenericStore<Author>();
        static GenericStore<Book> book = new GenericStore<Book>();

        static void Main(string[] args)
        {
            try
            {
                using (var fs = File.OpenRead(dataFilePath))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    Database db = bf.Deserialize(fs) as Database;

                    if (db != null)
                    {
                        author = db.author;
                        book = db.book;
                    }
                }
            }
            catch (Exception)
            {
            }


            int selectedId;
            Author selectedAuthor;
            Book selectedBook;

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;


            

            //Qarşılama və Əsas Menyu hissəsi

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeWriter.TypeWriterEffect("Kitabxanaya xoş gəlmisiniz! \n");
        l1:
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeWriter.TypeWriterEffect(">> Əməliyyat növünü seç: \n\n");
            Console.ForegroundColor = color;

            Console.WriteLine("[1] Müəlliflər üzrə əməliyyat");
            Console.WriteLine("[2] Kitablar üzrə əməliyyat");
            Console.WriteLine("[3] Yadda saxla və Çıx \n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Əməliyyat [ID] : ");
            Console.ForegroundColor = color;

            int mainMenuChoice;
            int mainmenucounter = Enum.GetNames(typeof(MainMenu)).Length;
            int subauthormenucounter = Enum.GetNames(typeof(AuthorSubMenu)).Length;
            int subbookmenucounter = Enum.GetNames(typeof(BookSubMenu)).Length;

            if (!int.TryParse(Console.ReadLine(), out mainMenuChoice) || mainMenuChoice <= 0 || mainMenuChoice > mainmenucounter)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWriter.TypeWriterEffect("[!] ID boş, hərf və ya mətn formasında göndərilə bilməz\n");
                TypeWriter.TypeWriterEffect("[!] ID menyudakı saydan çox ola bilməz\n");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = color;
                Console.Clear();

                goto l1;
            }

            if (mainMenuChoice == (int)MainMenu.Author)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriter.TypeWriterEffect("[Müəlliflər üzrə əməliyyat] seçildi! \n");

            l2:
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriter.TypeWriterEffect(">> Əməliyyat növünü seçin: \n\n");
                Console.ForegroundColor = color;
                Console.WriteLine("[1] Müəllif əlavə et");
                Console.WriteLine("[2] Müəllif sil");
                Console.WriteLine("[3] Müəllifdə dəyişiklik et");
                Console.WriteLine("[4] Bütün müəllifləri göstər");
                Console.WriteLine("[5] ID ilə müəllifi göstər");
                Console.WriteLine("[6] Əsas Menyuya Qayıt\n\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Əməliyyat [ID] : ");
                Console.ForegroundColor = color;

                int authorSubMenuChoice;

                //Müəllif Submenu və əməliyyatlar hissəsi

                if (!int.TryParse(Console.ReadLine(), out authorSubMenuChoice) || authorSubMenuChoice <= 0 || authorSubMenuChoice > subauthormenucounter)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWriter.TypeWriterEffect("[!] ID boş, hərf və ya mətn formasında göndərilə bilməz\n");
                    TypeWriter.TypeWriterEffect("[!] ID menyudakı saydan çox ola bilməz\n");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = color;
                    Console.Clear();

                    goto l2;
                }

                if (authorSubMenuChoice == (int)AuthorSubMenu.CreateAuthor)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Müəllif əlavə et] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;

                    selectedAuthor = new Author();

                    selectedAuthor.Name = Helper.ReadString(">> Müəllifin adını daxil et: ");
                    selectedAuthor.Surname = Helper.ReadString(">> Müəllifin soyadını daxil et: ");

                    author.Add(selectedAuthor);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Müəllif uğurla əlavə olundu! Müəlliflərin siyahısını görmək üçün <ENTER> düyməsini sıx.\n");
                    TypeWriter.TypeWriterEffect(">> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                    Console.ForegroundColor = color;
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeWriter.TypeWriterEffect(">> Müəlliflərin siyahısı:\n");
                        Console.ForegroundColor = color;
                        GetAllAuthor();

                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeWriter.TypeWriterEffect("\n>> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                        Console.ForegroundColor = color;
                        var keyMenu = Console.ReadKey();
                        if (keyMenu.Key == ConsoleKey.M)
                        {
                            Console.Clear();
                            goto l2;
                        }
                    }
                    if (key.Key == ConsoleKey.M)
                    {
                        Console.Clear();
                        goto l2;
                    }
                }
                else if (authorSubMenuChoice == (int)AuthorSubMenu.DeleteAuthor)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Müəllif sil] əməliyyatı seçildi! \n\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (author.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Müəllif siyahısı boşdur.\n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l2;
                    }

                l3:
                    GetAllAuthor();

                    selectedId = Helper.ReadInt("\n>> Silmək istədiyin müəllifin ID nömrəsini qeyd et: ");

                    selectedAuthor = author.GetById(selectedId);

                    if (selectedAuthor == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l3;
                    }
                    author.Remove(selectedAuthor);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Müəllif uğurla silindi!\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto l2;

                }
                else if (authorSubMenuChoice == (int)AuthorSubMenu.EditAuthor)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Müəllifdə dəyişiklik et] əməliyyatı seçildi! \n\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (author.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Müəllif siyahısı boşdur.\n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l2;
                    }

                l6:
                    GetAllAuthor();

                    selectedId = Helper.ReadInt("\n>> Dəyişmək istədiyin müəllifin ID nömrəsini qeyd et: ");

                    selectedAuthor = author.GetById(selectedId);

                   

                    if (selectedAuthor == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l6;
                    }

                    selectedAuthor.Name = Helper.ReadString("\n>>Müəllifin yeni adını daxil et: ");
                    selectedAuthor.Surname = Helper.ReadString(">>Müəllifin yeni soyadını daxil et: ");

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Müəllif uğurla dəyişdirildi!\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto l2;
                }
                else if (authorSubMenuChoice == (int)AuthorSubMenu.GetAllAuthor)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Bütün müəllifləri göstər] əməliyyatı seçildi! \n\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (author.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Müəllif siyahısı boşdur.\n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l2;
                    }

                    GetAllAuthor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("\n>> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                    Console.ForegroundColor = color;
                    var keyMenu = Console.ReadKey();
                    if (keyMenu.Key == ConsoleKey.M)
                    {
                        Console.Clear();
                        goto l2;
                    }
                }
                else if (authorSubMenuChoice == (int)AuthorSubMenu.GetAuthorById)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[ID ilə müəllifi göstər] əməliyyatı seçildi! \n\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (author.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Müəllif siyahısı boşdur.\n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l2;
                    }

                l7:
                    GetAllAuthor();

                    selectedId = Helper.ReadInt("\n>> Müəlliflərin siyahısından ID daxil et: ");

                    selectedAuthor = author.GetById(selectedId);

                    if (selectedAuthor == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l7;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect($"\nDaxil etdiyin ID-yə görə tapılan müəllif: [{selectedAuthor}]\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto l2;

                }
                else if (authorSubMenuChoice == (int)AuthorSubMenu.GoHome)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeWriter.TypeWriterEffect("Əsas menyuya keçid olunur... \n");
                    Console.ForegroundColor = color;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    goto l1;
                }
            }

            //Kitab Submenu və əməliyyatlar hissəsi

            else if (mainMenuChoice == (int)MainMenu.Book)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriter.TypeWriterEffect("[Kitablar üzrə əməliyyat] seçildi! \n");
                Console.ForegroundColor = color;

            l3:
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriter.TypeWriterEffect(">> Əməliyyat növünü seçin: \n\n");
                Console.ForegroundColor = color;
                Console.WriteLine("[1] Kitab əlavə et");
                Console.WriteLine("[2] Kitab sil");
                Console.WriteLine("[3] Kitabda dəyişiklik et");
                Console.WriteLine("[4] Bütün kitabları göstər");
                Console.WriteLine("[5] ID ilə kitabı göstər");
                Console.WriteLine("[6] Əsas Menyuya Qayıt\n\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Əməliyyat [ID] : ");
                Console.ForegroundColor = color;

                int bookSubMenuChoise;


                if (!int.TryParse(Console.ReadLine(), out bookSubMenuChoise) || bookSubMenuChoise <= 0 || bookSubMenuChoise > subbookmenucounter)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWriter.TypeWriterEffect("[!] ID boş, hərf və ya mətn formasında göndərilə bilməz\n");
                    TypeWriter.TypeWriterEffect("[!] ID menyudakı saydan çox ola bilməz\n");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = color;
                    Console.Clear();

                    goto l3;
                }

                if (bookSubMenuChoise == (int)BookSubMenu.CreateBook)

                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Kitab əlavə et] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    selectedBook = new Book();

                    if (author.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Müəllif siyahısı boşdur. Kitab əlavə etmək üçün ilk növbədə müəllif əlavə et\n\n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l3;

                    }

                    GetAllAuthor();

                l8:
                    selectedId = Helper.ReadInt("\n>> Siyahıdan müəllif seç: ");




                    selectedAuthor = author.GetById(selectedId);
                    if (selectedAuthor == null)
                    {
                        goto l8;
                    }

                    selectedBook.AuthorId = selectedId;

                    selectedBook.Name = Helper.ReadString(">> Kitabın adını daxil et: ");
                    selectedBook.Genre = Helper.ReadEnum<Genre>("\n>> Siyahıdan kitabın janrını seç: ");
                    selectedBook.PageCount = Helper.ReadInt(">> Kitabın səhifə sayını daxil et: ");
                    selectedBook.Price = Helper.ReadDecimal(">> Kitabın qiymətini daxil et: ");
                    book.Add(selectedBook);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Kitab uğurla əlavə olundu! Kitabların siyahısını görmək üçün <ENTER> düyməsini sıx.\n");
                    TypeWriter.TypeWriterEffect(">> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                    Console.ForegroundColor = color;
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeWriter.TypeWriterEffect(">> Kitabların siyahısı:\n");
                        Console.ForegroundColor = color;
                        GetAllBooks();
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeWriter.TypeWriterEffect("\n>> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                        Console.ForegroundColor = color;
                        var keyMenu = Console.ReadKey();
                        if (keyMenu.Key == ConsoleKey.M)
                        {
                            Console.Clear();
                            goto l3;
                        }
                    }
                    if (key.Key == ConsoleKey.M)
                    {
                        Console.Clear();
                        goto l3;
                    }


                }
                else if (bookSubMenuChoise == (int)BookSubMenu.DeleteBook)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Kitab sil] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (book.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Kitab siyahısı boşdur! \n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l3;
                    }

                    l4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect(">> Kitabların siyahısı: \n\n");
                    Console.ForegroundColor = color;

                    GetAllBooks();

                    selectedId = Helper.ReadInt("\n>> Silmək istədiyin kitabın ID nömrəsini qeyd et: ");

                    selectedBook = book.GetById(selectedId);

                    if (selectedBook == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l4;
                    }
                    book.Remove(selectedBook);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Kitab uğurla silindi!\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto l3;



                }

                else if (bookSubMenuChoise == (int)BookSubMenu.EditBook)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Kitabda dəyişiklik et] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (book.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Kitab siyahısı boşdur! \n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l3;
                    }


                l9:
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect(">> Kitabların siyahısı: \n\n");
                    Console.ForegroundColor = color;

                    GetAllBooks();

                    selectedId = Helper.ReadInt("\n>> Dəyişiklik etmək istədiyin kitabın ID nömrəsini qeyd et: ");

                    selectedBook = book.GetById(selectedId);

                    if (selectedBook == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l9;
                    }

                    selectedBook.Name = Helper.ReadString("\n>> Kitabın yeni adını daxil et: ");
                    selectedBook.Genre = Helper.ReadEnum<Genre>(">> Kitabın yeni janrını seç: ");
                    selectedBook.PageCount = Helper.ReadInt(">> Kitabın yeni səhifə sayını daxil et: ");
                    selectedBook.Price = Helper.ReadDecimal(">> Kitabın yeni qiymətini daxil et: ");

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("Kitab uğurla dəyişdirildi!\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto l3;

                }
                else if (bookSubMenuChoise == (int)BookSubMenu.GetAllBook)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[Bütün kitabları göstər] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);


                    if (book.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Kitab siyahısı boşdur! \n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l3;
                    }


                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect(">> Kitabların siyahısı: \n");
                    Console.ForegroundColor = color;
                    GetAllBooks();

                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("\n>> Menyuya qayıtmaq üçün <M> düyməsini sıx.");
                    Console.ForegroundColor = color;
                    var keyMenu = Console.ReadKey();
                    if (keyMenu.Key == ConsoleKey.M)
                    {
                        Console.Clear();
                        goto l3;
                    }
                }
                else if (bookSubMenuChoise == (int)BookSubMenu.GetBookById)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect("[ID ilə kitabı göstər] əməliyyatı seçildi! \n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(1000);

                    if (book.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Kitab siyahısı boşdur! \n");
                        Console.ForegroundColor = color;
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto l3;
                    }

                    l10:
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect(">> Kitabların siyahısı: \n");
                    Console.ForegroundColor = color;
                    GetAllBooks();

                    selectedId = Helper.ReadInt(">> Seçmək istədiyin kitabın ID nömrəsini daxil et: ");


                    selectedBook = book.GetById(selectedId);

                    if (selectedBook == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeWriter.TypeWriterEffect("[!] Xəta baş verdi [Belə bir ID yoxdur].\n\n");
                        Console.ForegroundColor = color;
                        goto l10;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriter.TypeWriterEffect($"\nDaxil etdiyin ID-yə görə tapılan kitab: {selectedBook}\n");
                    Console.ForegroundColor = color;
                    Thread.Sleep(4000);
                    Console.Clear();
                    goto l3;


                }
                else if (bookSubMenuChoise == (int)BookSubMenu.GoHome)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeWriter.TypeWriterEffect("Əsas menyuya keçid olunur... \n");
                    Console.ForegroundColor = color;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    goto l1;
                }
            }
            else if (mainMenuChoice == (int)MainMenu.SaveAndExit)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriter.TypeWriterEffect(">> Daxil edilən məlumatlar yaddaşda saxlanılır, zəhmət olmasa gözlə...");
                Console.ForegroundColor = color;
                Thread.Sleep(4000);

                using (FileStream fs = File.OpenWrite(dataFilePath))
                {
                    Database db = new Database();

                    db.author = author;
                    db.book = book;

                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(fs, db);
                }
            }

        }

        static void GetAllAuthor()
        {
            foreach (var item in author)
            {
                Console.WriteLine(item);
            }
        }

        static void GetAllBooks()
        {
            foreach (var item in book)
            {
                Console.WriteLine(item);
            }
        }


    }
}


