using System;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryClient.LoadFromDisk();
            displayMenu();
           
            while(true){
            if(int.TryParse(Console.ReadLine(),out int option)){
            if(option >5 || option < 1)
              Console.WriteLine($"Choose an integer between 1 and 5");
            else{
              try{
            switch(option){
                case 1: 
                  LibraryClient.ViewBooks();
                  break;
                case 2:
                  Console.WriteLine("==========Add a Book=========");
                  LibraryClient.InsertBook();
                  displayMenu();
                  break;
                case 3:
                  LibraryClient.SelectBook();
                 // LibraryClient.EditBook(bookid);
                  break;
                case 4:
                  LibraryClient.Search();
                  break;
                case 5: 
                  Console.WriteLine("Save and exit");
                  LibraryClient.SaveToDisk();
                  Environment.Exit(0);
                  break;
                default:
                 displayMenu();
                break;
              
              }
            }catch(Exception ex){
                   Console.WriteLine($"Error Occurred in library app with message : {ex.Message}");
                   displayMenu();
            }
            }
            }
            else{
                Console.Clear();
                displayMenu();
            }
            }
           
        }
        private static void displayMenu(){
                      
          var menu = "1) View all books \n" + "2) Add a book \n" +  "3) Edit a book \n" +  "4) Search for a book \n" + "5) Save and exit";

          Console.WriteLine($"{Environment.NewLine}=========== Book Manager ==========");
          Console.WriteLine($"{Environment.NewLine}{menu}");
          Console.Write($"Choose[1-5] :");
        }
    }
}
