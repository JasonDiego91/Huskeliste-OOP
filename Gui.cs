using System.Net.Sockets;

namespace Huskeliste
{
    #region MAIN BODY section and start up Menu
    internal class Gui
    {
        private Lists data = new Lists();
        private SharedData Note = new SharedData();
        private string path = @"c:\HuskelisteData.json";
        public Gui()
        {
            
            while (true)
            {
                Menu();
            }
        }
        // MAIN MENU FOR WHOLE PROGRAM
        private void Menu()
        {
            Console.WriteLine("\nMENU\n1 to view StudyNoteMenu\n2to view WorkNoteMenu\n3to view ShoppingNoteMenu\n4 to view HomeNoteMenu");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    StudyNoteMenu();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    WorkNoteMenu();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    ShoppingNoteMenu();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    HomeNoteMenu();
                    break;
                default:
                    break;
            }
        }
        #endregion



        #region SAVE & LOAD section

        // SAVE DATA & LOAD DATA
        private void SaveDataStudy()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path + "/StudyNoteList.json", json);
            Console.WriteLine("File saved succesfully at " + path);
        }

        private void LoadDataStudy()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(path + "/StudyNoteList.json");
            data = System.Text.Json.JsonSerializer.Deserialize<Lists>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }
        private void SaveDataWork()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path + "/WorkNoteList.json", json);
            Console.WriteLine("File saved succesfully at " + path);
        }

        private void LoadDataWork()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(path + "/WorkNoteList.json");
            data = System.Text.Json.JsonSerializer.Deserialize<Lists>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }

        private void SaveDataShopping()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path + "/ShoppingNoteList.json", json);
            Console.WriteLine("File saved succesfully at " + path);
        }

        private void LoadDataShopping()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(path + "/ShoppingNoteList.json");
            data = System.Text.Json.JsonSerializer.Deserialize<Lists>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }

        private void SaveDataHome()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path + "/HomeNoteList.json", json);
            Console.WriteLine("File saved succesfully at " + path);
        }

        private void LoadDataHome()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(path + "/HomeNoteList.json");
            data = System.Text.Json.JsonSerializer.Deserialize<Lists>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }
        #endregion


        #region  GET INPUT DATA
        //------------------------------SHARED CALCULATIONS-------------------\\


        // LENGTH CALCULATION
        private DateTime GetLength()
        {
            DateTime time;
            do
            {
                Console.Write("Length (hh:mm:ss): ");
            }
            while (!DateTime.TryParse("0001-01-01 " + Console.ReadLine(), out time));
            return time;
        }


        //RELEASE DATE CALCULATION
        private DateTime GetReleaseDate()
        {
            DateTime date;
            string input = "";
            do
            {
                Console.Write("Release Date (dd/mm/yyyy): ");
                input = Console.ReadLine();
                if (input == "") return DateTime.Today;
            }
            while (!DateTime.TryParse(input, out date));
            return date;
        }


        // GET INDPUT
        private string GetString(string type)
        {
            string? input;
            do
            {
                Console.Write(type);
                input = Console.ReadLine();
                if (input == "") return "Unknown";
            }
            while (input == null);
            return input;
        }
    
        /*private int GetInt(string request)
        {
            int i;
            do
            {
                Console.Write(request);
            }
            while (!int.TryParse(Console.ReadLine(), out i));
            return i;
        }
     */
        #endregion


        #region STUDY SECTION
        //STUDY MENU
        private void StudyNoteMenu()
        {
            Console.WriteLine("\nSTUDY MENU\n1 to see list of Study Notes\n2 to search Study Note\n3 to add new Study Note\n4 to save Study Note\n5 to load Study Note");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowStudyList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchStudyNote();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddStudyNote();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveDataStudy();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadDataStudy();
                    break;

            }
        }


        // ADD STUDY NOTE
        private void AddStudyNote()
        {
            StudyNote studynote = new StudyNote();
            studynote.Title = GetString("Title: ");
            studynote.Creator = GetString("Creator: ");
            studynote.Description = GetString("Description: ");
            studynote.Length = GetLength();
            studynote.Getdate = GetReleaseDate();


            ShowStudyNote(studynote);
            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.StudyNoteList.Add(studynote);
        }


        //SEARCH STUDY NOTE
        private void SearchStudyNote()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine().ToLower();
            foreach (StudyNote studynote in data.StudyNoteList)
            {
                if (search != null)
                {

                    if (studynote.Title.ToLower().Contains(search) || studynote.Description.ToLower().Contains(search))
                        ShowStudyNote(studynote);
                }
            }
        }


        //SHOW STUDY NOTE
        private void ShowStudyNote(StudyNote s)
        {
            Console.WriteLine($"{s.Title} {s.Creator} {s.GetLengthOfJob()} {s.Description} {s.GetDate()} ");
        }


        //SHOW STUDY NOTE LIST
        private void ShowStudyList()
        {
            if (data.StudyNoteList == null || data.StudyNoteList.Count == 0) return;
            foreach (StudyNote m in data.StudyNoteList)
            {
                ShowStudyNote(m);
            }
        }
        #endregion


        #region WORKNOTE SECTION
        //WORKNOTE MENU
        private void WorkNoteMenu()
        {
            Console.WriteLine("\nSERIES MENU\n1 for list of WorkNote\n2 to search for WorkNote\n3 to add new WorkNote\n4 to save WorkNote\n5 to load WorkNote");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowWorkNoteList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchWorkNote();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddWorkNote();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveDataWork();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadDataWork();
                    break;

            }
        }



        // ADD WORKNOTE
        private void AddWorkNote()
        {

            WorkNote worknote = new WorkNote();
            worknote.Title = GetString("Title: ");
            worknote.Creator = GetString("Creator: ");
            worknote.Description = GetString("Description: ");
            worknote.Length = GetLength();
            worknote.Getdate = GetReleaseDate();

            ShowWorkNote(worknote);
            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.WorkNoteList.Add(worknote);
        }


        //SEARCH WORKNOTE
        private void SearchWorkNote()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine().ToLower();
            foreach (WorkNote worknote in data.WorkNoteList)
            {
                if (search != null)
                {
                    if (worknote.Title.ToLower().Contains(search) || worknote.Description.ToLower().Contains(search))
                        ShowWorkNote(worknote);
                }
            }
        }


        //SHOW WORKNOTE
        private void ShowWorkNote(WorkNote s)
        {
            Console.WriteLine($"{s.Title} {s.Creator} {s.GetLength()} {s.Description} {s.GetDate()}");

        }


        //SHOW WORKNOTE LIST
        private void ShowWorkNoteList()
        {
            foreach (WorkNote s in data.WorkNoteList)
            {
                ShowWorkNote(s);
            }
        }

        #endregion


        #region SHOPPINGNOTE SECTION
        //  SHOPPINGNOTE MENU

        private void ShoppingNoteMenu()
        {
            Console.WriteLine("\nMusic MENU\n1 for list of ShoppingNote\n2 to search for ShoppingNote \n3 to add new ShoppingNote\n4 to save ShoppingNote\n5 to load ShoppingNote");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowShoppingNoteList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchShoppingNote();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddShoppingNote();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveDataShopping();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadDataShopping();
                    break;
            }
        }


        //ADD SHOPPINGNOTE
        private void AddShoppingNote()
        {
            ShoppingNote shoppingnote = new ShoppingNote();
            shoppingnote.Title = GetString("Title: ");
            shoppingnote.Creator = GetString("Creator: ");
            shoppingnote.Description = GetString("Description: ");
            shoppingnote.Length = GetLength();
            shoppingnote.Getdate = GetReleaseDate();

            ShowShoppingNote(shoppingnote);
            Console.WriteLine("Add ShoppingNote to list? Confirm adding to list (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) data.ShoppingNoteList.Add(shoppingnote);

        }

        //SEARCH SHOPPINGNOTE
        private void SearchShoppingNote()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine().ToLower();
            foreach (ShoppingNote shoppingnote in data.ShoppingNoteList)
            {
                if (search != null)
                {
                    if (shoppingnote.Title.ToLower().Contains(search) || shoppingnote.Description.ToLower().Contains(search))
                        ShowShoppingNote(shoppingnote);
                }
            }
        }

        // SHOW SHOPPINGNOTE
        private void ShowShoppingNote(ShoppingNote s)
        {
            Console.WriteLine($"{s.Title} {s.Creator} {s.GetLength()} {s.Description} {s.GetDate()} ");
        }

        // SHOW SHOPPINGNOTE LIST
        private void ShowShoppingNoteList()
        {
            foreach (ShoppingNote s in data.ShoppingNoteList)
            {
                ShowShoppingNote(s);
            }
        }

        #endregion

        #region HOMENOTE SECTION
        //  HOMENOTE MENU

        private void HomeNoteMenu()
        {
            Console.WriteLine("\nHOMENOTE MENU\n1 for list of HomeNote\n2 to search for HomeNote\n3 to add new HomeNote\n4 to save HomeNote\n5 to load HomeNote");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowHomeNoteList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchHomeNote();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddHomeNote();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveDataHome();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadDataHome();
                    break;
            }
        }


        //ADD HOMENOTE
        private void AddHomeNote()
        {
            HomeNote homenote = new HomeNote();
            homenote.Title = GetString("Title: ");
            homenote.Creator = GetString("Creator: ");
            homenote.Description = GetString("Description: ");
            homenote.Length = GetLength();
            homenote.Getdate = GetReleaseDate();

            ShowHomeNote(homenote);
            Console.WriteLine("Add HomeNote to list? Confirm adding to list (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) data.HomeNoteList.Add(homenote);

        }

        //SEARCH HOMENOTE
        private void SearchHomeNote()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine().ToLower();
            foreach (HomeNote homenote in data.HomeNoteList)
            {
                if (search != null)
                {
                    if (homenote.Title.ToLower().Contains(search) || homenote.Description.ToLower().Contains(search))
                        ShowHomeNote(homenote);
                }
            }
        }

        // SHOW HOMENOTE
        private void ShowHomeNote(HomeNote s)
        {
            Console.WriteLine($"{s.Title} {s.Creator} {s.GetLength()} {s.Description} {s.GetDate()} ");
        }

        // SHOW HOMENOTE LIST
        private void ShowHomeNoteList()
        {
            foreach (HomeNote s in data.HomeNoteList)
            {
                ShowHomeNote(s);
            }
        }

        #endregion

    }
}
