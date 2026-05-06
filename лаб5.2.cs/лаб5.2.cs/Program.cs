using System;

namespace LibraryHierarchy
{
    // 1. Базовий клас: Друковане видання
    class PrintedEdition
    {
        protected string title;

        // Конструктор 1: Без параметрів
        public PrintedEdition()
        {
            title = "Невідомо";
            Console.WriteLine("[PrintedEdition]: Конструктор без параметрів");
        }

        // Конструктор 2: З параметрами
        public PrintedEdition(string title)
        {
            this.title = title;
            Console.WriteLine($"[PrintedEdition]: Конструктор з назвою: {this.title}");
        }

        // Конструктор 3: Копіювання 
        public PrintedEdition(PrintedEdition other)
        {
            this.title = other.title;
            Console.WriteLine($"[PrintedEdition]: Конструктор копіювання для: {this.title}");
        }

        // Деструктор
        ~PrintedEdition()
        {
            Console.WriteLine($"[PrintedEdition]: Деструктор вивільняє ресурси для: {title}");
        }
    }

    // 2. Клас: Журнал
    class Magazine : PrintedEdition
    {
        private int issueNumber;

        public Magazine() : base()
        {
            issueNumber = 0;
            Console.WriteLine("[Magazine]: Конструктор без параметрів");
        }

        public Magazine(string title, int number) : base(title)
        {
            this.issueNumber = number;
            Console.WriteLine($"[Magazine]: Конструктор з номером випуску: {issueNumber}");
        }

        public Magazine(Magazine other) : base(other)
        {
            this.issueNumber = other.issueNumber;
            Console.WriteLine("[Magazine]: Конструктор копіювання");
        }

        ~Magazine()
        {
            Console.WriteLine("[Magazine]: Деструктор викликан");
        }
    }

    // 3. Клас: Книга
    class Book : PrintedEdition
    {
        protected string author;

        public Book() : base()
        {
            author = "Невідомий";
            Console.WriteLine("[Book]: Конструктор без параметрів");
        }

        public Book(string title, string author) : base(title)
        {
            this.author = author;
            Console.WriteLine($"[Book]: Конструктор з автором: {this.author}");
        }

        public Book(Book other) : base(other)
        {
            this.author = other.author;
            Console.WriteLine("[Book]: Конструктор копіювання");
        }

        ~Book()
        {
            Console.WriteLine("[Book]: Деструктор викликан");
        }
    }

    // 4. Клас: Підручник (Нащадок книги)
    class Textbook : Book
    {
        private string subject;

        public Textbook() : base()
        {
            subject = "Загальний";
            Console.WriteLine("[Textbook]: Конструктор без параметрів");
        }

        public Textbook(string title, string author, string subject) : base(title, author)
        {
            this.subject = subject;
            Console.WriteLine($"[Textbook]: Конструктор з предметом: {this.subject}");
        }

        public Textbook(Textbook other) : base(other)
        {
            this.subject = other.subject;
            Console.WriteLine("[Textbook]: Конструктор копіювання");
        }

        ~Textbook()
        {
            Console.WriteLine("[Textbook]: Деструктор викликан");
        }
    }

    class Program
    {
        static void CreateObjects()
        {
            Console.WriteLine(" Початок створення об'єктів \n");

            // Масив об'єктів базового типу
            PrintedEdition[] library = new PrintedEdition[3];

            Console.WriteLine("1. Створення Журналу (конструктор з параметрами):");
            library[0] = new Magazine("Vogue", 5);

            Console.WriteLine("\n2. Створення Книги (конструктор копіювання):");
            Book originalBook = new Book("Кобзар", "Т. Шевченко");
            library[1] = new Book(originalBook);

            Console.WriteLine("\n3. Створення Підручника (конструктор без параметрів):");
            library[2] = new Textbook();

            Console.WriteLine("\n Об'єкти створені. Вихід з методу... ");
        }

        static void Main(string[] args)
        {
            CreateObjects();

            // Примусовий виклик збирача сміття для демонстрації деструкторів
            Console.WriteLine("\nЧекаємо на збирач сміття (GC)...");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nПрограма завершена.");
        }
    }
}