using System;
using System.IO;
using System.Text.RegularExpressions;
using SomeMetodyForFile;

namespace SomeMetodyForFile
{
    public static class ClassStringPlus
    {
        
        public static string[] Reverse(this String[] str)
        {
            int i = str.Length - 1;
            string[] str1= new string[i+1];
            str1.Initialize();
            foreach (string word in str)
            {
                str1[i] = word;
                i--;
            }
            return str1;
        }

        public static string[] Reverse(this String[] str, int StartIndex)
        {
            int i = str.Length - 1;
            string[] str1 = new string[i + 1];
            str1.Initialize();
            for(int count=0;i>0;count++)
            {
                if(StartIndex < count)
                {
                    if (!str[count].Contains('.'))
                    {
                        str1[i] = str[count];
                    }
                    else {
                        str1[i] = str[count].Remove(str[count].IndexOf('.')+1);
                        return str1; 
                    }
                    i--;
                }
            }
            return str1;
        }
    }

    public class SomeClassForFile
    {
        private String[] m_Text;
        public String[] Text {  get {return m_Text;} }

        public SomeClassForFile(string text)
        {
            m_Text = text.Split(new char[] { ' '});
        }

        public void ReadText()
        {
            foreach (string text in m_Text)
            {
                if (!(text + " ").Contains("  "))
                {
                    Console.Write(text + " ");
                }
            }    
            Console.WriteLine();
        }
        public string GetText(string str="")
        {
            foreach (string text in m_Text)
            {
                if (!(text + " ").Contains("  "))
                {
                    str+=text + " ";
                }
            }
            return str;
        }
        public void Delete(String str, int count=0, bool isExist=false)
        {
            if(str.Length==1) //Symbols
            {
                foreach (string words in m_Text)
                {
                    if (words.Contains(str))
                    {
                        isExist = true;
                        m_Text[count] = words.Replace(str, "");
                    }
                    count++;
                }
                if(!isExist)
                {
                    Console.WriteLine("This file does not have this \"{0}\"", str);
                }
                return;
            }
            foreach(string words in m_Text) //words
            {
                if(words.Contains(str))
                {
                    isExist = true;
                    m_Text[count] = words.Remove(words.IndexOf(str), str.Length)+" ";
                }
                count++;
            }

            if (!isExist)
            {
                Console.WriteLine("This file does not have \"{0}\"",str);
            }
        }

        public void CountWordsAndTenWords(int count=1)
        {
            Console.WriteLine("\nCount words: {0}", Regex.Matches(GetText(),@"[\S]+").Count);
            foreach(string word in m_Text)
            {
                if (count%10==0 & count!=1)
                {
                    Console.Write(word);
                    if (count < m_Text.Length - 9) Console.Write(", ");
                }
                    count++;
            }
            Console.WriteLine();
        }

        public void ThirdSentenceReverse(int count=0)
        {
            Console.WriteLine("\nReverse: ");
            for (int i= 0; i<m_Text.Length-1;i++)
            {
                if (m_Text[i].Contains('.'))
                {
                    count++;
                }
                if (count == 2)
                {
                    foreach (string words in m_Text.Reverse(StartIndex:i))
                    {
                        if(words!=null) Console.Write(words + " ");
                    }
                    Console.WriteLine();
                    return;
                }
            }
        }
    }
}

namespace Task1
{

    class Program
    {
       // public class ConsolManager { } v2.0 
     
        interface IReadTxt
        {
            public String ReadTxt();
            public void CreateFile(string text);
        }
        private class Reader : IReadTxt //IRreadPDF and other
        {
            private string path;
            private string m_fileName;
            public string FileName { get { return m_fileName; } }
            public Reader(string path)
            {
                this.path = path;
                m_fileName = path.Substring(path.LastIndexOf('\\')).Trim('\\');
            }
            public String ReadTxt()
            {
                try
                {
                    using (FileStream fstream = File.OpenRead(path))
                    {
                        // преобразуем строку в байты
                        byte[] array = new byte[fstream.Length];
                        // считываем данные
                        fstream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        return System.Text.Encoding.Default.GetString(array);
                    }
                }
                catch (FileNotFoundException e)
                {
                    return "No file with this name";
                }    
            }

            public string CutFileName(string path)
            {
                for (int i = path.Length - 1; i > 0; i--)  //обрезаем от пути название файла
                {
                    if (path[i] != '\\')
                    {
                        path = path.TrimEnd(path[i]);
                    }
                    else
                    {
                        return path;
                    }
                }
                return path;
            }

            public void CreateFile(string text)
            {
                using (FileStream fstream = new FileStream(CutFileName(path) + "~" + m_fileName, FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(text);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Текст записан в файл ~" + m_fileName);
                }
            }
        }

        private class WalkerDirectories
        {
            private string temp_path; //backup path
            private static string m_path;
            public static readonly DriveInfo[] drivers = DriveInfo.GetDrives();
            private static string[] dirs;
    
            private Reader reader;
            private SomeClassForFile filetext;
            public string path { get { return m_path; }}

            public WalkerDirectories()
            {
                ChangeDisk(out String str);
                dirs = Directory.GetDirectories(m_path);
            }
            public void ChangeDisk(out String str)
            {
                Console.WriteLine("Choose disk: ");
                foreach (var drive in drivers)
                {
                    Console.Write(drive.Name + " ");
                }
                Console.WriteLine();
                str = Console.ReadLine();
                foreach (var drive in drivers)
                {
                    if (str == drive.Name)
                    {
                        if(drive.IsReady)
                        {
                            m_path = str;
                            dirs = Directory.GetDirectories(m_path);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Disk is not ready. Try again.");
                            str = "";
                            ChangeDisk(out str);
                            return;
                        }
                        
                    }
                }
                Console.WriteLine("Mistake, try again!");
                ChangeDisk(out str);
            }
             
            public void Walk(out String str)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full path: \"fp\", Open File: \"open\", Exit: \"bye\"",m_path); //toolbar

                    Console.WriteLine("Directories :");
                    foreach (string nameDir in dirs)
                        Console.WriteLine("\t"+nameDir);

                    Console.WriteLine("Files :");
                    foreach (string nameFile in Directory.GetFiles(m_path))
                        Console.WriteLine("\t" + nameFile);


                    Console.WriteLine("Next directory :");
                    str = Console.ReadLine();
                    if (str == "..") //back
                    {
                        for (int i = m_path.Length - 2; i > 0; i--)
                        {
                            if (m_path[i] == '\\')
                            {
                                m_path = m_path.Remove(i + 1, m_path.Length - i - 1);
                                dirs = Directory.GetDirectories(m_path);
                                Walk(out str);
                                return;
                            }
                        }
                    } else //back
                    if (str == "cd") //Change disk
                    {
                        ChangeDisk(out str);
                    }
                    else //Change disk
                    if (str == "fp")   // Full path
                    {
                        Console.Write("Full path directory: ");
                        str = Console.ReadLine();
                        temp_path = m_path;
                        if(!str.EndsWith(".txt"))
                        {
                            try
                            {
                                m_path = str;
                                dirs = Directory.GetDirectories(str);
                                Walk(out str);
                                return;
                            }
                            catch (DirectoryNotFoundException e)
                            {
                                Console.WriteLine("Wrong path... Try again. \t*enter*");
                                Console.ReadLine();
                                m_path = temp_path;
                                Walk(out str);
                                return;
                            }
                        }
                        else
                        {
                            if (reader == null) reader = new Reader(str);
                            m_path=reader.CutFileName(str); //обрезаем от пути название файла

                            Console.Clear();
                            OpenFile((IReadTxt)reader,out str);
                            return;
                        }
                        
                    }
                    else // Full path
                    if (str=="bye") //Exit
                    {
                        if (isExite(out str)) Environment.Exit(0);
                    }else //Exit
                    if(str == "open") //Open File
                    {
                        Console.Write("File: ");
                        str = Console.ReadLine();
                        Console.WriteLine();
                        Console.Clear();

                        if (!str.Contains("\\"))
                        {
                            if (str.EndsWith(".txt"))
                            {
                                if (reader == null) reader = new Reader(m_path  + str);
                                OpenFile((IReadTxt)reader, out str);
                                return;
                            }
                        }
                        else
                        {
                            if (str.EndsWith(".txt"))
                            {
                                if (reader == null) reader = new Reader(str);
                                for (int i = str.Length - 1; i > 0; i--)   //cut from path name file
                                {
                                    if (str[i] != '\\')
                                    {
                                        str = str.TrimEnd(str[i]);
                                    }
                                    else { break; }
                                }
                                m_path = str;
                                Console.Clear();
                                OpenFile((IReadTxt)reader, out str);
                                return;
                            }
                        }
                    }           //Open File

                    foreach (string nameDir in dirs)
                    {
                        if (m_path+str == nameDir)
                        {
                            temp_path = m_path;
                            m_path += str+"\\";
                            
                            try
                            {

                                dirs = Directory.GetDirectories(m_path);
                                Walk(out str);
                                return;
                            }
                            catch (UnauthorizedAccessException e)
                            {
                                Console.WriteLine("Access is denied... Try again. \t*enter*");
                                Console.ReadLine();
                                m_path = temp_path;
                                Walk(out str);
                                return;
                            }
                        };
                    }
                    //("Mistake, try again")
                    Walk(out str);
                }
            }
            private void OpenFile(IReadTxt read, out string str)
            {
                filetext = new SomeClassForFile(read.ReadTxt());
                
                if (filetext.Text[0] == "No" & filetext.Text[1]=="file" & filetext.Text[4]=="name") 
                {
                    filetext.ReadText();
                    reader = null; Console.ReadKey(); str = ""; return; 
                }

               while(true)
                {
                    filetext.ReadText();
                    Console.Write(
               "1. Delete symbol or word.\n" +
               "2. Count words and every tenth word.\n" +
               "3. Backward third sentence.\n" +
               "4. Close file.\n" +
               "(number) :"
               );
                    str = Console.ReadLine();
                    if (int.TryParse(str, out int result))
                    {
                        switch (Int32.Parse(str))
                        {
                            case 1:
                                Console.WriteLine("Symbol or word: ");
                                filetext.Delete(Console.ReadLine());
                                break;
                            case 2:
                                filetext.CountWordsAndTenWords();
                                break;
                            case 3:
                                filetext.ThirdSentenceReverse();
                                break;
                            case 4:
                                Console.WriteLine("Save changes ?(yes,no)");
                                str = Console.ReadLine();
                                if(str=="yes"|str=="y"|str=="Y"|str=="Yes"|str=="YES")
                                {
                                    read.CreateFile(filetext.GetText()); //Creat backup
                                }
                                reader = null; // delete )
                                return;
                            default:
                                break;
                        };
                    }
                    Console.WriteLine("\t\t *enter*");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static bool isExite(out String str)
        {
            Console.Write("Close console? (yes,no): ");
            str = Console.ReadLine();
            if (str == "yes" | str == "Yes" | str == "y" | str == "Y")
            {
                Console.WriteLine("Have a nice day!");
                return true;
            }
            else if (str == "no" | str == "No" | str == "n" | str == "N")
            {
                return false;
            }
            Console.WriteLine("Mistake, try again");
            return isExite(out str);
        }

        
        static void Main(string[] args)
        {
            WalkerDirectories r = new WalkerDirectories();
            while (true)
            {
                r.Walk(out String str);

                if (isExite(out str)) return;
            }
        }
    }
}