using System;
using System.Collections.Generic;
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
            var books = HPLibrary.GetAllBooks().ToList();
            Assert.That(books.Select(book => book.Price), Is.All.EqualTo(8m));
        }

        [Test]
        public void GivesRightPriceForTwoCopiesOfFirstBookAndOneOfTheSecond()
        {
            var books = new[]
            {
                HPLibrary.GetBookById(1), HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(2)
            };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(23.2m));
        }

        [Test]
        public void GivesRightPriceForTwoDifferentBooks()
        {
            var books = new[] { HPLibrary.GetBookById(1), HPLibrary.GetBookById(2) };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(15.2m));
        }

        [Test]
        public void GivesRightPriceForThreeDifferentBooks()
        {
            var books = new[]
            {
                HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(2),
                HPLibrary.GetBookById(3)
            };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(21.6m));
        }

        [Test]
        public void GivesRightPriceForFourDifferentBooks()
        {
            var books = new[]
            {
                HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(2),
                HPLibrary.GetBookById(3),
                HPLibrary.GetBookById(4)
            };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(25.6m));
        }

        [Test]
        public void GivesRightPriceForFiveDifferentBooks()
        {
            var books = new[]
            {
                HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(2),
                HPLibrary.GetBookById(3),
                HPLibrary.GetBookById(4),
                HPLibrary.GetBookById(5)
            };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(30m));
        }

        [Test]
        public void GivesTheRightPriceForMultipleSets()
        {
            var books = new[]
            {
                HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(1),
                HPLibrary.GetBookById(2),
                HPLibrary.GetBookById(2),
                HPLibrary.GetBookById(3),
                HPLibrary.GetBookById(3),
                HPLibrary.GetBookById(4),
                HPLibrary.GetBookById(5)
            };

            var price = new PriceCalculator().Calculate(books.ToList());

            Assert.That(price, Is.EqualTo(51.6));
        }
    }

    public class PriceCalculator
    {
        public decimal Calculate(List<Book> books)
        {
            var price = 0m;
            var distinctBooks = books.GroupBy(b => b.Id).Select(b => b.First());

            while (distinctBooks.Count() > 1)
            {
                var discount = 0m;
                switch (distinctBooks.Count())
                {
                    case 2:
                        discount = 0.05m;
                        break;
                    case 3:
                        discount = 0.10m;
                        break;
                    case 4:
                        discount = 0.20m;
                        break;
                    case 5:
                        discount = 0.25m;
                        break;
                }

                foreach (var book in distinctBooks)
                {
                    price += book.Price - (book.Price * discount);
                    books.Remove(book);
                }
            }

            price += books.Sum(b => b.Price);

            return price;
        }
    }

    public class HPLibrary
    {
        public static Book[] GetAllBooks()
        {
            return new Book[] { new Book(1), new Book(2),
                                new Book(3), new Book(4),
                                new Book(5) };
        }

        public static Book GetBookById(int id)
        {
            return new Book(id);
        }
    }

    public class Book
    {
        public int Id { get; }

        public Book(int id)
        {
            Id = id;
        }

        public decimal Price { get; } = 8;
    }
}
