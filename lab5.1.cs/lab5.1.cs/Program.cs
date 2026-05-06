using System;
using System.Collections.Generic;
using System.Linq;
namespace PrintedEditionsApp
{
    abstract class PrintedEdition
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        protected PrintedEdition(string title, int pageCount)
        {
            Title = title;
            PageCount = pageCount;
        }
        public abstract void Show();
    }
    class Book : PrintedEdition
    {
        public string Author { get; set; }
        public Book(string title, int pageCount, string author)
            : base(title, pageCount)
        {
            Author = author;
        }
        public override void Show()
        {
            Console.WriteLine($"[КНИГА]     | Назва: {Title,-20} | Автор: {Author,-15} | Сторінок: {PageCount}");
        }
    }
    class Magazine : PrintedEdition
    {
        public int IssueNumber { get; set; }
        public Magazine(string title, int pageCount, int issueNumber)
            : base(title, pageCount)
        {
            IssueNumber = issueNumber;
        }
        public override void Show()
        {
            Console.WriteLine($"[ЖУРНАЛ]    | Назва: {Title,-20} | Номер: {IssueNumber,-15} | Сторінок: {PageCount}");
        }
    }
    class Textbook : PrintedEdition
    {
        public string Subject { get; set; }

        public Textbook(string title, int pageCount, string subject)
            : base(title, pageCount)
        {
            Subject = subject;
        }
        public override void Show()
        {
            Console.WriteLine($"[ПІДРУЧНИК] | Назва: {Title,-20} | Предмет: {Subject,-13} | Сторінок: {PageCount}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<PrintedEdition> editions = new List<PrintedEdition>
            {
                new Book("Маруся Чурай", 190, "Ліна Костенко"),
                new Magazine("Discovery", 64, 8),
                new Textbook("Алгебра", 250, "Математика"),
                new Book("Тигролови", 320, "Іван Багряний"),
                new Magazine("Vogue", 110, 3),
                new Textbook("Географія", 180, "Природознавство")
            };
            var sortedEditions = editions.OrderBy(e => e.PageCount).ToList();

            Console.WriteLine("Список друкованих видань (відсортовано за кількістю сторінок):");
            Console.WriteLine(new string('-', 85));
            foreach (var edition in sortedEditions)
            {
                edition.Show();
            }
            Console.WriteLine(new string('-', 85));
            Console.ReadKey();
        }
    }
}
