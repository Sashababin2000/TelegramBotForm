using TelegramBot;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace TelegramBotForm
{
    public partial class Form1 : Form
    {
        List<UserChat> users = new List<UserChat>();
        TgBot tgBot = new TgBot();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Файл Excel|*.XLSX;*.XLS";
            openDialog.ShowDialog();
            try
            {

                XSSFWorkbook hssfwb;
                ISheet sheet1;


                hssfwb = new XSSFWorkbook(openDialog.FileName);

                sheet1 = hssfwb.GetSheetAt(0);
                int row = 1;
                while (row <= sheet1.LastRowNum)
                {
                    try
                    {
                        UserChat userChat = new UserChat();

                        sheet1.GetRow(row).GetCell(0).SetCellType(CellType.Numeric);
                        userChat.ChatId = (long)sheet1.GetRow(row).GetCell(0).NumericCellValue;
                        if (userChat.ChatId != null && userChat.ChatId != 0) users.Add(userChat);

                        row++;
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, "Ошибка при считывании excel файла", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                // Закрытие приложения Excel.
                GC.Collect();
            }
            StartBot.Visible = true;
            Export.Visible = true;

        }

        private void StartBot_Click(object sender, EventArgs e)
        {
            tgBot.Notify += WriteRichTextBox;
            tgBot.Start();
            tgBot.StartQuestionnaire(users);
        }
        public void WriteRichTextBox(string text)
        {


            if (richTextBox1.InvokeRequired)
            {
                System.Action safeWrite = delegate { WriteRichTextBox(text); };
                richTextBox1.Invoke(safeWrite);
            }
            else
                richTextBox1.AppendText(text + "\n");

        }

        private void Export_ClickOld(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (FileStream stream = new FileStream(@"C:\Working\FieldedAddresses.xlsx", FileMode.Create, FileAccess.Write))
            {
                IWorkbook wb = new XSSFWorkbook();
                ISheet sheet = wb.CreateSheet("Sheet1");
                ICreationHelper cH = wb.GetCreationHelper();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i);
                    for (int j = 0; j < 9; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(cH.CreateRichTextString(dt.Rows[i].ItemArray[j].ToString()));
                    }
                }
                wb.Write(stream);
            }
        }
        private void Export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(folderBrowserDialog1.SelectedPath + @"\UsersExport.xlsx", FileMode.Create, FileAccess.Write))
                {
                    IWorkbook wb = new XSSFWorkbook();
                    ISheet sheet = wb.CreateSheet("Sheet1");
                    ICreationHelper cH = wb.GetCreationHelper();

                    IRow row0 = sheet.CreateRow(0);
                    ICell cell0 = row0.CreateCell(0);
                    cell0.SetCellValue("ID");
                    ICell cell10 = row0.CreateCell(1);
                    cell10.SetCellValue("STATUS");
                    ICell cell20 = row0.CreateCell(2);
                    cell20.SetCellValue("NAME");
                    ICell cell30 = row0.CreateCell(3);
                    cell30.SetCellValue("Сallsign");
                    ICell cell40 = row0.CreateCell(4);
                    cell40.SetCellValue("Question_1");
                    ICell cell50 = row0.CreateCell(5);
                    cell50.SetCellValue("Question_2");
                    ICell cell60 = row0.CreateCell(6);
                    cell60.SetCellValue("Question_3");
                    ICell cell70 = row0.CreateCell(7);
                    cell70.SetCellValue("Question_4");
                    ICell cell80 = row0.CreateCell(8);
                    cell80.SetCellValue("Question_5");

                    int i = 1;
                    foreach(UserChat use in tgBot.users)
                    {
                        IRow row = sheet.CreateRow(i);
                        ICell cell = row.CreateCell(0);
                        cell.SetCellValue(use.ChatId);
                        ICell cell1 = row.CreateCell(1);
                        cell1.SetCellValue(use.Status);
                        ICell cell2 = row.CreateCell(2);
                        cell2.SetCellValue(use.Username);
                        ICell cell3 = row.CreateCell(3);
                        cell3.SetCellValue(use.Сallsign);
                        ICell cell4 = row.CreateCell(4);
                        cell4.SetCellValue(use.Question_1);
                        ICell cell5 = row.CreateCell(5);
                        cell5.SetCellValue(use.Question_2);
                        ICell cell6 = row.CreateCell(6);
                        cell6.SetCellValue(use.Question_3);
                        ICell cell7 = row.CreateCell(7);
                        cell7.SetCellValue(use.Question_4);
                        ICell cell8 = row.CreateCell(8);
                        cell8.SetCellValue(use.Question_5);
                        i++;
                    }
                    wb.Write(stream);

                }
                
            }
        }
    }
}