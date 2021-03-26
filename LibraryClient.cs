using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace LibraryApp{
    public class LibraryClient {
    public static void InsertBook(){
                  Console.WriteLine("Please enter the following information");
                  var title = ValidateInput("Title");
                  var author = ValidateInput("Author");
                  Console.Write("Description : ");
                  var desc = Console.ReadLine();
                  Book.AddBook(new Book(title, Book.books.Count+ 1, author, desc));
                  Console.WriteLine("Catalog count is :" + Book.books.Count);
                  
    }
    public static void SelectBook(){
        if(Book.books.Count > 0){
        listBooks();
        Console.WriteLine("Enter the book ID of the book you want to edit; to return press <Enter>.");
        Console.Write("BookID : ");
        while(int.TryParse(Console.ReadLine(),out int id)){
          EditBook(id);
          Console.WriteLine("Enter the book ID of the book you want to edit; to return press <Enter>.");
          Console.WriteLine("BookID : ");
         }
        }
        else
          Console.WriteLine("There are no books available in Catalog");
    }
    public static void EditBook(int bookID){
         if(bookID >0)
            {
                var book = Book.books.FirstOrDefault(val => val.Id == bookID);
                
                if(book != null)
                {
                  Console.WriteLine("Input the following information. To leave a field unchanged, hit <Enter>");
                  Console.WriteLine($"Title [{book.Title}] : ");
                  var title = Console.ReadLine();
                  Console.WriteLine($"Author [{book.Author}] : ");
                  var author = Console.ReadLine();
                  Console.WriteLine($"Description [{book.Description}");
                  var desc = Console.ReadLine();
                  Book.UpdateBook(book, new Book(title, 1, author, desc));
                  Console.WriteLine("Book Saved.");

                 }

            }
            else
             Console.WriteLine("Enter a valid book id");

    }

    public static void Search(){
        Console.WriteLine("==== Search ====");
        Console.WriteLine("Type in one or more keywords to search for");
        Console.Write("Search : ");
        var words = Console.ReadLine();
        List<string> keywords = words.Split(" ").ToList<string>();
        var query = new List<Book>();
    foreach(string keyword in keywords){
       query = query.Concat((from book in Book.books 
                    where book.Title.ToLower().Contains(keyword.ToLower()) ||
                          book.Author.ToLower().Contains(keyword.ToLower()) 
                    select book)).ToList();
    };
    Console.WriteLine("The following books matched your query. Enter the book ID to see more details, or <Enter> to return.");
    if(query.Count > 0){
    foreach(Book book in query)
       Console.WriteLine($"[{book.Id}] {book.Title}");

    Console.Write("Book ID: ");
    while(int.TryParse(Console.ReadLine(),out int id)){
       var book = query.Find(book => book.Id == id);
        displayDetails(book);
        Console.Write("Book ID: ");
    }
    }
    else
       Console.WriteLine("No Books found in the database with the search criteria");

    }
    
    public static void SaveToDisk(){
        try {
          // Console.WriteLine($"{Environment.CurrentDirectory}");
           var filePath = Path.Combine(Environment.CurrentDirectory,"LibraryCatalog.json");

           using(StreamWriter stream = File.CreateText(filePath)){
               var options = new JsonSerializerOptions{
                     WriteIndented = true
            };  
             stream.Write(JsonSerializer.Serialize(Book.books,options));
           }
        Console.Write("Library Saved");
        }
        catch(Exception ex){
            Console.WriteLine("Error Occurred in saving the books to file :" + ex.Message);
        }
           
    }

    public static void LoadFromDisk(){
    try{
        var filePath = Path.Combine(Environment.CurrentDirectory,"LibraryCatalog.json");
        if(File.Exists(filePath)){
            var options = new JsonSerializerOptions{
                  PropertyNameCaseInsensitive = true,
            };
           var jsonString = File.ReadAllText(filePath);
           Book.books = JsonSerializer.Deserialize<List<Book>>(jsonString,options);
           Console.WriteLine($"Loaded {Book.books.Count} into the Library");
        }

    }
    catch(Exception ex){
               Console.WriteLine("Error Occurred in Loading file from Disk :" + ex.Message);
        }

    }
    public static void ViewBooks(){
        Console.WriteLine("==== View Books ====");
        foreach(Book book in Book.books){
            Console.WriteLine($"[{book.Id}] {book.Title}");
        }
        if(Book.books.Count > 0){
            Console.WriteLine("To view details enter the book ID, to return press <Enter>.");
            Console.Write("BookID : ");
            while(int.TryParse(Console.ReadLine(),out int id)){
                if(Book.books.Exists(val => val.Id == id)){
                displayDetails(Book.books.First(val => val.Id == id));
                }
                else
                    Console.WriteLine($"The book with ID : {id} doesn't exist ");
                Console.WriteLine();
                Console.Write("Book ID: ");

            }
        }
    }
    private static void displayDetails(Book book){
       
        if(book != null){
               Console.WriteLine($"ID: {book.Id}");
               Console.WriteLine($"Title : {book.Title}");
               Console.WriteLine($"Author : {book.Author}");
               Console.WriteLine($"Description : {book.Description}");
          }
       
    }
    public static void listBooks(){
        Console.WriteLine("=========Edit a Book==========");
        foreach(Book book in Book.books)
        Console.WriteLine($"[{book.Id}] {book.Title}");
    }

    private static string ValidateInput(string field){
        Console.Write($"{field} : ");
        bool _IsInvalidInput = true;
        while(_IsInvalidInput){
        Regex reg = new Regex(@"^[a-zA-Z][a-zA-Z\s]*$");
        var input = Console.ReadLine();
        if(reg.IsMatch(input)){
            if(field.ToLower() == "author")
            {
               if(IsValidName(input))
                   return input;
            }                                                                                                                                        
            else {
                 _IsInvalidInput = false;
                 return input;
            }
        
        }
        else 
           Console.WriteLine($"{field} can't be null or empty");
        }
       return "";
    }
    private static bool IsValidName(string name){
         foreach(char c in name)
                 {
                     if(! (Char.IsLetter(c) || Char.IsWhiteSpace(c)))
                      {
                          Console.WriteLine("Author name field should not contain digits");
                           return false;
                      }
                 }
        return true;
    }

    }
     
}                                                                                                                                                                                                                                                                         