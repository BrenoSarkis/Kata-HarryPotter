using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kata.HarryPotter
{
    [TestFixture]
    public class WhenCalculatingBookPrices
    {
        private Book[] books;

        [SetUp]
        public void SetUp()
        {
            var books = new SomeBookRepository().GetAllBooks();
        }

        [Test]
        public void EveryIndividualBookCostsEightEuros()
        {
            Assert.That(books.Select(book => book.Price), Is.All.EqualTo(8m));
        }

        [Test]
        public void WithTwoDifferentBooks_FivePercentDiscountApplies()
        {
            var price = new PriceCalculator().Calculate(books.Take(2));

            Assert.That(price, Is.EqualTo(15.2m));
        }
    }

    public class PriceCalculator
    {
        public decimal Calculate(IEnumerable<Book> books)
        {
            var sumOfBooks = books.Sum(book => book.Price);
            return sumOfBooks - (sumOfBooks * 0.05m);
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
