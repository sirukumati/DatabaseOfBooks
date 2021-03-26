using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace LibraryApp
{
[Serializable]
public class Book {
      [Required(ErrorMessage = "Title is required field")]
      public String Title { get; set;}
      public int Id { get; set;}
      
      [Required(ErrorMessage= "Author is required field")]
      public String Author { get; set;}
      public String Description { get;  set;}
      public static List<Book> books = new List<Book>();
      public Book(String Title, int Id, String Author, String Description){
             this.Title = Title;
             this.Author = Author;
             this.Id = Id;
             this.Description = Description;
      } 
      public static void AddBook(Book book){
            if(book != null){
                  if(!books.Contains(book)){
                     books.Add(book);
                     Console.WriteLine($"Book with Title : {book.Title} and Author : {book.Author} added to catalog");                  }
                  else
                   Console.WriteLine("Book already exists in library catalog");
            }
      }
      public static void UpdateBook(Book target,Book source){
       if(!String.IsNullOrEmpty(source.Title))
        target.Title = source.Title;
      if(!String.IsNullOrEmpty(source.Author))
        target.Author = source.Author;
      if(!String.IsNullOrEmpty(source.Description))
        target.Description = source.Description;

      }
      public override bool Equals(Object obj)
        {
            try{
            Book book = (Book) obj;
            
            return book is Book                                                                                                                                                   && book.Title.ToLower() == this.Title.ToLower() && book.Author.ToLower() == this.Author.ToLower() ;
            }
            catch(Exception ex){
                  Console.WriteLine($"Error occurred in equals method of book class {ex.Message}");
                  return false;
            }
        }
        public override int GetHashCode()
        {
            return this.Title.ToLower().GetHashCode() ^ this.Author.ToLower().GetHashCode();
        }


    
    
      
}
}