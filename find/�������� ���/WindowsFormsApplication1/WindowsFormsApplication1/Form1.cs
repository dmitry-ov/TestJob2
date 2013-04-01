using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Finisar.SQLite;

namespace WindowsFormsApplication1
{

    public partial class SearchForm : Form
    {
        
        private int numdoc;
        

        public SearchForm()
        {
            InitializeComponent();
            numdoc = 0;
        }


        private void buttonNewDoc_Click(object sender, EventArgs e)
        {
            //buttonNewDoc.Enabled = false;
            ++numdoc;
            listdoc.Items.Add(numdoc.ToString());
            // разбить текст на слова
            string docContent = editNewDoc.Text;
            docContent = System.Text.RegularExpressions.Regex.Replace(docContent, "\r\n", " ");
            string[] words = docContent.Split(' ');
            // записать данные в таблицу " слово - название документа "
            // записать документ в таблицу " название документа - содержимое документа "
            SaveToDataBase(words, editNewDoc.Text);
            buttonNewDoc.Enabled = true;
        }

        
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            textSearchResult.Clear();
            // разбить словосочетание на слова (по количеству пробелов)
            string searchString = editSearch.Text;
            // searchString = System.Text.RegularExpressions.Regex.Replace(searchString, "\r\n", " ");
            if (searchString == "")
            {
                return ;
            }
            // получим список слов в поисковом запросе, используем в качестве разделителя пробел
            string[] words = searchString.Split(' ');
            
            // Получим список документов в которых содержатся слова из поискового запроса.
            ArrayList NumDocsWithWords = FindDocsList(words);
            
            // если не найдено ни одного документа со словами из поискового запроса
            if (NumDocsWithWords.Count == 0)
            {
                textSearchResult.Text += "По вашему запросу ничего не найдено ";
                return; 
            }

            // создадим массив для оценок
            ArrayList Valuation = new ArrayList();

            // Теперь у нас есть  :
            //  список слов из запроса -  words
            //  список документов в которых *все* эти слова встречаються - DocsWithWords
            
            // Получаем документы. 
            ArrayList DocsContent = GetDocsContent( NumDocsWithWords );
            
                        
            // Ищем в документах слова. Выставлем коэфициенты релевантности.

            /*  -- Принцып работы алгоритма -- 
             * Перебираем все документы и выставляем им  *оценки*.  
             * Находим индексы вхождения искомых слов в файле,
             * считаем расстояния между словами (в символах) и складываем их.
             * Чем сумма этих расстояний выше(чем больше разнесены слова в тексте) тем ниже
             * ранжирование текущего документа. Т.о. наивысшую оценку получит тот документ в
             * содержании которого встретяться все искомые слова поискового запроса, 
             * записанные подряд. Так как слова могут повторяться в тексте, 
             * необходимо собрать все индексы для каждого слова и рассмотреть все 
             * варианты сумм расстояний. Самая маленькая сумма будет оценкой всего документа.
             */

            /*  -- Оценка документа --
             * Для каждого слова из запроса, составляем массив индексов вхождения 
             * в текст документа.
             * Получаем список массивов(массив массивов).
             * Перебираем варианты сочетаний элементов, с целью выявления наименьшей суммы 
             * расстояний между словами запроса (т.е. наилучшей оценки коэфициента релевантности)
             * Запоминаем самую лучшую оценку и индексы в массиве с помощью которых она была получена.
             * (эти индексы помогут выделить слова в тексте)
             */

            foreach (string DocsItem in DocsContent)
            {
                // массив списков индексов для конкретного документа
                ArrayList indexes = new ArrayList();
                
                // перебираем слова
                foreach (string word in words)
                {
                    // для каждого слова создаем список индексов
                    ArrayList IndexesWordFromDocument = GetWordIndexesFromDocument(word, DocsItem);
                
                    /* Дописываем список индексов в конец списка списков indexes.
                     * Порядок следования списков индексов, соответствует порядку 
                     * следования слов в запросе.
                     */
                    indexes.Add(IndexesWordFromDocument);
                }

                /*
                 * Перебраем комбинации сумм индексов 
                 * всех элементов списка списков.
                 * Ищем наилучшую оценку для документа.
                 * 
                 */
                int sum = 0; // оценка документа
                foreach (ArrayList a in indexes)
                {
                    foreach (ArrayList b in indexes)
                    {
                        foreach (int i in a)
                        {
                            foreach (int j in b)
                            {
                                if ( (i+j) > sum )
                                {
                                    sum = i + j; 
                                }

                            }
                        }
                    }
                }
              Valuation.Add(sum); // оценку текущего документа, помещаем в конец списка оценок
            } //   -- foreach (string DocsItem in DocsContent)

            // NumDocsWithWords  - массив номеров документов в БД
            // Valuation - массив оценок

            /* эти 2 списка могут выглядеть так 
             *
             *   Документ 1 | Документ 2 | Документ 3 | Документ 4 | Документ 5            
             *   Оценка 5   |  Оценка 1  |  Оценка 3  | Оценка 2   | Оценка 4         
             * 
             *   Необходимо отсортировать оценки документов.
             *   У каких оценка меньше, те должны иметь более высокий 
             *   коэфициент релевантности.
             *   Составим из этих двух списков двумерный массив и отсортируем его.
             *   Сортировать массив будем по второй строке - по оценкам.
             * 
             */

            // NumDocsWithWords  - список номеров документов в БД
            // Valuation - список оценок

            int[,] array2D = new int[2, NumDocsWithWords.Count];

            // Запишем в массив список номеров документов
            int InCount = 0;
            foreach (string i in NumDocsWithWords)
            {
                array2D[0, InCount] = Convert.ToInt32(i);
                ++InCount;
            }
            
            // Запишем в массив список оценок документов
            InCount = 0;
            foreach (int i in Valuation)
            {
                array2D[1, InCount] = i;
                ++InCount;
            }

            int backup;
            /* отсортируем массив по оценкам, т.е. по второй строке.
             * при перестановках во второй строке массива, переставим 
             * элементы и в первой строке.
             */
            for (int k = 0; k < NumDocsWithWords.Count; ++k)
            {
                for (int j=0; j < Valuation.Count; ++j)
                {
                    if  ( array2D[1,k] < array2D[1,j] )
                    {
                        for (int p = 0; p < 2; ++p)
                        {
                            backup = array2D[p, k];
                            array2D[p, k] = array2D[p, j];
                            array2D[p, j] = backup;
                        }
                        //backup = array2D[0, k];
                        //array2D[0, k] = array2D[0, j];
                        //array2D[0, j] = backup;
                    }
                }
            }
            
            // Выведем релевантный результат поиска
            for (int k = 0; k < NumDocsWithWords.Count; ++k)
            {
                int NumberDoc = array2D[0, k];
                int index = NumDocsWithWords.IndexOf(NumberDoc.ToString());
                textSearchResult.Text += DocsContent[index].ToString() + "\r\n";
                textSearchResult.Text += "----------\r\n";
            }
           
        } 
        


        // Возвращает список индексов в документе
        private ArrayList GetWordIndexesFromDocument(string word, string document)
        {
            ArrayList List = new ArrayList();
            string str = document;
            bool i = true;

            int index = str.IndexOf(word); // первый индекс
            List.Add(index);
            while (i == true)
            {
                index = str.IndexOf(word, index + 1);
                if (index != -1)
                {
                    List.Add(index);
                }
                else i = false;
            }
            return List;
        }




        private ArrayList GetDocsContent(ArrayList List)
        {
            ArrayList DocsContent = new ArrayList();


            SQLiteConnection a = new SQLiteConnection();
            a.ConnectionString = @"Data Source=search.sqlite;New=false;Version=3";
            try
            {
                a.Open();

                int IntCouneter = List.Count;
                StringBuilder Query = new StringBuilder("SELECT content FROM content WHERE ");
                foreach (string name in List)
                {
                    --IntCouneter;
                    Query.Append("( numdoc ='");
                    Query.Append(name);
                    Query.Append("')");
                    if (IntCouneter != 0) Query.Append(" OR ");
                }

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = a;
                cmd.CommandText = Query.ToString();

                SQLiteDataReader reader = (SQLiteDataReader)cmd.ExecuteReader();
                while (reader.Read())
                {
                    DocsContent.Add((string)reader["content"]);
                }
            }
            catch (Exception ex)
            {
                textSearchResult.Clear();
                textSearchResult.Text += ex.Message;
            }
            a.Close();

            return DocsContent;
        }
        

        // составляет список списков документов в которых встречаються слова 
        // из поискового запроса
        private ArrayList FindDocsList(string[] words)
        {
            //  Список документов в которых присутствуют все слова из поискового запроса
            ArrayList WordSearch = new ArrayList();
            
            // получим списки документов для слов
                SQLiteConnection a = new SQLiteConnection();
                a.ConnectionString = @"Data Source=search.sqlite;New=false;Version=3";
                try
                {
                    a.Open();
                    // создадим запрос который позволит получить
                    // пересечение списков документов, содержащих слова из запроса
                    StringBuilder query = new StringBuilder();
                    int ItemsCounts = words.Length;
                    foreach (string word in words)
                    {
                        --ItemsCounts;
                        query.Append("SELECT numdoc FROM DICT WHERE ( word = ");
                        query.Append("'");
                        query.Append(word);
                        query.Append("')");
                        if (ItemsCounts != 0) query.Append(" INTERSECT ");
                    }
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = a;
                    cmd.CommandText = query.ToString();
                    SQLiteDataReader reader = (SQLiteDataReader)cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        WordSearch.Add(Convert.ToString(reader["numdoc"]));
                    }
                }
                catch (Exception ex)
                {
                    textSearchResult.Clear();
                    textSearchResult.Text += ex.Message;
                }
                a.Close();
          return WordSearch;
        }


        private void SearchForm_Shown(object sender, EventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo("search.sqlite");

            if (!file.Exists)
            {
                MessageBox.Show("Нет файла базы данных");
                CreateDataBase();
            }
            //   numdoc = 0;
            GetDataFromBaseToListDocuments();
        }


        private void CreateDataBase()
        {
            ArrayList Querys = new ArrayList();
            Querys.Add("CREATE TABLE 'content' ('numdoc' INTEGER NOT NULL , 'content' TEXT NOT NULL )");
            Querys.Add("CREATE TABLE 'dict' ('word' VARCHAR(44) NOT NULL , 'numdoc' INTEGER NOT NULL) ");

            SQLiteConnection b = new SQLiteConnection();
            b.ConnectionString = @"Data Source=search.sqlite;New=True;Version=3";
            try
            {
                b.Open();
                foreach (string str in Querys)
                {
                    StringBuilder Query = new StringBuilder();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = b;
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                textSearchResult.Clear();
                textSearchResult.Text += ex.Message;
            }
            b.Close();
        }

        //  Сохраняет документ в БД. 
        //  Производит записи в таблицу Dict и сохраняет тект документа в таблицу CONTENT
        private void SaveToDataBase(string[] words, string document)
        {

            SQLiteConnection a = new SQLiteConnection();
            a.ConnectionString = @"Data Source=search.sqlite;New=false;Version=3";
            try
            {
                a.Open();

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = a;

                foreach (string str in words)
                {
                    StringBuilder query = new StringBuilder();

                    query.Append("INSERT INTO DICT(word,numdoc) VALUES('");
                    query.Append(str);
                    query.Append("','");
                    query.Append(numdoc.ToString());
                    query.Append("')");

                    cmd.CommandText = query.ToString();
                    cmd.ExecuteNonQuery();
                }

                StringBuilder QueryInsert = new StringBuilder();

                QueryInsert.Append("INSERT INTO CONTENT(numdoc,content) VALUES('");
                QueryInsert.Append(numdoc.ToString());
                QueryInsert.Append("','");
                QueryInsert.Append(document);
                QueryInsert.Append("')");

                cmd.CommandText = QueryInsert.ToString();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                textSearchResult.Clear();
                textSearchResult.Text += ex.Message;
            }
            a.Close();
        }


        private void GetDataFromBaseToListDocuments()
        {
            SQLiteConnection a = new SQLiteConnection();
            a.ConnectionString = @"Data Source=search.sqlite;New=false;Version=3";
            try
            {
                a.Open();

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = a;
                cmd.CommandText = "SELECT * FROM CONTENT";
                SQLiteDataReader reader = (SQLiteDataReader)cmd.ExecuteReader();

                listdoc.Items.Clear();

                numdoc = 0;

                while (reader.Read())
                {
                    ++numdoc;
                    listdoc.Items.Add((int)reader["numdoc"]);
                    //int q = (int)reader["numdoc"];
                    //string fas1 = (String)reader["content"];
                }
            }
            catch (Exception ex)
            {
                textSearchResult.Clear();
                textSearchResult.Text += ex.Message;
            }
            a.Close();
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            editNewDoc.Clear();
        }


        private void listdoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            listdoc.SelectedIndex.ToString();

            SQLiteConnection a = new SQLiteConnection();
            a.ConnectionString = @"Data Source=search.sqlite;New=false;Version=3";
            try
            {
                a.Open();

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = a;
                cmd.CommandText = "SELECT content FROM CONTENT where numdoc="
                                 + (listdoc.SelectedIndex + 1).ToString();

                SQLiteDataReader reader = (SQLiteDataReader)cmd.ExecuteReader();

                while (reader.Read())
                {
                    textBox.Clear();
                    textBox.Text = (String)reader["content"];
                }
            }
            catch (Exception ex)
            {
                textSearchResult.Clear();
                textSearchResult.Text += ex.Message;
            }
            a.Close();
        }
                

    }




}
