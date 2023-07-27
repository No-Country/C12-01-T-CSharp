using API.Dtos;
using API.Helpers;
using API.Interfaces;
using API.models;
using API.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.DataAccess
{

    public class BookService : IBookService
    {
        private readonly BookCartContext _dbContext;
        private readonly IConfiguration _configuration;
        public BookService(BookCartContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _configuration = config;
        }
        public async Task<List<Book>> GetAllBooks(BooksResourceParameters parameters)
        {
            var collection = _dbContext.Books  as IQueryable<Book>;


            if (parameters.Price == "DES")
            {
                collection = collection.OrderByDescending(b => b.Price);
            }
            else collection = collection.OrderBy(b => b.Price);


            //Fintering
            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                var category = parameters.Category.Trim().ToLower();
                collection = collection.Where(b => b.Category.ToLower() == category);
            }

            //Searching
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var searching = parameters.Search.Trim().ToLower();
                collection = collection.Where(b => b.Title.ToLower().Contains(searching) || b.Author.ToLower().Contains(searching));
            }

            return await collection.ToListAsync();

        }

        public async Task<Book?> GetBookById(int id)
        {
            return await  _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<bool> IsValidCategory(string category)
        {   
            string cat = category.Trim().ToLower();

            return await _dbContext.Categories.AnyAsync(c => c.CategoryName.ToLower() == cat);
        }

        public async Task<Categories?> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }


        public async Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Categories category)
        {   
            Random random = new Random();

            List<Book> randomBooks = await _dbContext.Books
                .Where(b => b.Category == category.CategoryName)
                .ToListAsync();

            randomBooks = randomBooks.OrderBy(b => random.Next()).Take(5).ToList();


               

            return randomBooks;
        }





        public async Task<List<CartItemDto>> GetBooksAvailableInCart(string cartId)
        {
            List<CartItemDto> cartItemList = new List<CartItemDto>();
            List<CartItems> cartItems = await _dbContext.CartItems.Where(x => x.CartId == cartId).ToListAsync();

            foreach (CartItems item in cartItems)
            {
                Book book = await GetBookData(item.ProductId);
                book.CoverFileName = _configuration["ApiUrl"] + book.CoverFileName;

                CartItemDto objCartItem = new CartItemDto
                {
                    Book = book,
                    Quantity = item.Quantity
                };

                cartItemList.Add(objCartItem);
            }
            return cartItemList;
        }


        public async Task<Book> GetBookData(int bookId)
        {
            Book book = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookId == bookId);
            if (book != null)
            {
                 _dbContext.Entry(book).State = EntityState.Detached;
                return book;
            }
            return null;
        }



        public async  Task<List<Book>> GetBooksAvailableInWishlist(string wishlistID)
        {
            List<Book> wishlist = new List<Book>();
            List<WishlistItems> cartItems = _dbContext.WishlistItems.Where(x => x.WishlistId == wishlistID).ToList();

            foreach (WishlistItems item in cartItems)
            {
                Book book = await GetBookData(item.ProductId);
                wishlist.Add(book);
            }
            return wishlist;
        }


        public async Task<List<Book>> GetSimilarBooks(int bookId)
        {   
            Random random = new Random();
            List<Book> lstBook = new List<Book>();
            Book book = await GetBookData(bookId);

            lstBook =   await _dbContext.Books.Where(x => x.Category == book.Category && x.BookId != book.BookId).ToListAsync();
            lstBook = lstBook.OrderBy(b => random.Next()).Take(5).ToList();

                
            return lstBook;
        }


        public int AddBook(Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public string DeleteBook(int bookId)
        {
            try
            {
                Book book = _dbContext.Books.Find(bookId);
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();

                return (book.CoverFileName);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateBook(Book book)
        {
            try
            {
                Book oldBookData = await GetBookData(book.BookId);

                if (oldBookData.CoverFileName != null)
                {
                    if (book.CoverFileName == null)
                    {
                        book.CoverFileName = oldBookData.CoverFileName;
                    }
                }

                _dbContext.Entry(book).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}


