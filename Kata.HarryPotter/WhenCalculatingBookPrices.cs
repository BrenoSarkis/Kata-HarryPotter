using System.Linq;
using NUnit.Framework;

namespace Kata.HarryPotter
{
    [TestFixture]
    public class WhenCalculatingBookPrices
    {
        [Test]
        public void EveryIndividualBookCostsEightEuros()
        {
            var books = new SomeBookRepository().GetAllBooks();
            Assert.That(books.Select(book => book.Price), Is.All.EqualTo(8m));
        }
    }

    public class SomeBookRepository
    {
        public Book[] GetAllBooks()
        {
            return new Book[] { new Book(), new Book(), new Book(), new Book(), new Book() };
        }
    }

    public class Book
    {
        public decimal Price { get; } = 8;
    }
}
