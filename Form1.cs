using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication4
{
    public partial class CTM : Form
    {
        public CTM()
        {
            InitializeComponent();
        }
        public void Compare(string sourceId, string CompareTerm)
        {
            int row = 0;
            do
            {
                try
                {
                    if ((string.Compare(CompareTerm, termDataGridView[1, row].Value.ToString(), false)) == 0)
                    {
                        connectDataGridView.Rows.Add(
                            (connectDataGridView.Rows.Count - 1),
                            sourceId,
                            termDataGridView[0, row].Value);
                        return;
                    }
                    else
                    {
                        row++;
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Term is not in the table:" + CompareTerm);
                    return;
                }
            }
            while (row < termDataGridView.Rows.Count);
            }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                               
                            {
                                connectDataGridView.Visible = true;
                                sentanceDataGridView.Visible = false;
                                termDataGridView.Visible = false;
                                int row = 0;
                                string line;
                                while ((line = sr.ReadLine())!=null)
                                {
                                    string[]columns = line.Split('\t');
                                    connectDataGridView.Rows.Add();
                                    for (int i=0; i <columns.Length; i++)
                                    {
                                        connectDataGridView[i,row].Value=columns[i];
                                    }
                                    row++;
                                }
                        }

                    }
                }}
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Orighinal error:" + ex.Message);
                }
            }
        }

        private void termsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DataSet PojemDataSet = new DataSet();
            
            
            
          
           
           
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                               
                            {
                                connectDataGridView.Visible = false;
                                sentanceDataGridView.Visible = false;
                                termDataGridView.Visible = true;

                                if (sentanceDataGridView.RowCount > 1)
                                {
                                    connectToolStripMenuItem.Enabled = true;
                                }
                                

                                int row = 0;
                                string line;
                                while ((line = sr.ReadLine())!=null)
                                {
                                    string[]columns = line.Split('\t');
                                    termDataGridView.Rows.Add();
                                    for (int i=0; i <columns.Length; i++)
                                    {
                                        termDataGridView[i,row].Value=columns[i];
                                    }
                                    row++;
                                }
                        }

                    }
                }}
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Orighinal error:" + ex.Message);
                    connectToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void sentancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DataSet PojemDataSet = new DataSet();



            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FilterIndex == 1)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                                {
                                    connectDataGridView.Visible = false;
                                    sentanceDataGridView.Visible = true;
                                    termDataGridView.Visible = false;
                                    if (termDataGridView.RowCount > 1)
                                    {
                                        connectToolStripMenuItem.Enabled = true;
                                    }

                                    int row = 0;
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        string[] columns = line.Split('\t');
                                        sentanceDataGridView.Rows.Add();
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            sentanceDataGridView[i, row].Value = columns[i];
                                        }
                                        row++;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Orighinal error:" + ex.Message);
                        connectToolStripMenuItem.Enabled = false;
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Enabled = false;
            termDataGridView.Rows.Clear();
            sentanceDataGridView.Rows.Clear();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            connectDataGridView.Visible = true;
            sentanceDataGridView.Visible = false;
            termDataGridView.Visible = false;
            
            for (int i = 0; i<(sentanceDataGridView.Rows.Count - 1);i++)
            {

                if (sentanceDataGridView[2, i].Value.ToString() != string.Empty)
                {
                    string termCell = sentanceDataGridView[2, i].Value.ToString();

                    if (termCell[0].ToString().Equals("\"".ToString()))
                    {
                        
                        
                        int j = 1;
                        do
                        {
                            if (termCell[j].ToString().Equals(";".ToString()))
                            {
                                string trimCell = termCell.Substring(1, j-1);
                                Compare(sentanceDataGridView[0, i].Value.ToString(), trimCell);
                                termCell = termCell.Substring(j+1, termCell.Length - (j+1));
                                j = 0;
                            }
                            j++;
                        }
                        while (j < termCell.Length);
                        Compare(sentanceDataGridView[0, i].Value.ToString(), termCell.Substring(1, termCell.Length - 2));

                    }
                    else
                    {
                        if (termCell.Length > 0)
                        {
                            Compare(sentanceDataGridView[0, i].Value.ToString(), termCell);
                        }
                    }



                }
            }
        
          
               
             
                
                
            }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {




            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File|*.txt";
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            StringBuilder builder = new StringBuilder();
            int rowcount = connectDataGridView.Rows.Count;
            int columncount = connectDataGridView.Columns.Count;

            for (int i = 0; i < rowcount - 1; i++)
            {
                List<string> cols = new List<string>();
                for (int j = 0; j < columncount; j++)
                {
                    cols.Add(connectDataGridView.Rows[i].Cells[j].Value.ToString());
                }
                builder.AppendLine(string.Join("\t", cols.ToArray()));
            }
            System.IO.File.WriteAllText(dialog.FileName, builder.ToString());
            MessageBox.Show(@"Text file was created.");
        }          
            
           }
            


        }
    

