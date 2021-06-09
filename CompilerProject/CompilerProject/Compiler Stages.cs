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

namespace CompilerProject
{
    public partial class Form1 : Form
    {
        public string[] type;
        public string[] idt;

        List<Var> var = new List<Var>();

        string[] array1, array2, array3;

        string[] code;

        public class Var
        {
            public string variable;
            public string type;
            public string value;

            public Var()
            {
                this.variable = "";
                this.type = "";
                this.value = "";
            }
        }
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();

            type = new string[]
            { "Identifier", "Identifier", "Identifier", "Identifier", "Identifier", "Identifier",
                "Operator", "Operator", "Operator", "Operator", "Operator",
                "Open Bracket", "Close Bracket", "Open Curly Bracket", "Close Curly Bracket", "Comma", "Semicolon", "And", "Or", "Less Than", "Greater Than", "Equal", "Not",
                "Reserved Word", "Reserved Word" ,"Reserved Word" ,"Reserved Word" ,"Reserved Word" ,"Reserved Word" ,"Reserved Word" ,"Reserved Word"
            };

            idt = new string[]
            { "int", "float", "string", "double", "bool", "char",
                "+", "-", "/", "%", "*",
                "(", ")", "{", "}", ",", ";", "&&", "||", "<", ">", "=", "!",
                "for", "while", "if", "do", "return", "break", "continue", "end"
            };

            code = new string[] 
            { "a","a","a","a","a","a",
                "b","b","b","b","b",
                "(",")","{","}",",",";","c","d","<",">","=","!",
                "e","e","e","e","e","e","e","e"
            };

            for (int i = 0; i < idt.Length; i++)
            {
                Console.WriteLine(idt[i] + " " + type[i] + " " + code[i]);
            }

            array1 = new string[] { "a", "#", ";=", "#0", ";b" };
            array2 = new string[] { "#", ";=", "#0", ";b" };
            array3 = new string[] { "e", "(", "#", ">= <= != == ", "0#", "&& || )", "{", "", "}" };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < richTextBox2.Lines.Length; i++)
            {
                string s = richTextBox2.Lines[i];
                string[] line = s.Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int pos = Array.IndexOf(idt, line[j]);
                    if (pos != -1)
                    {
                        richTextBox3.AppendText(line[j] + " " + " -> " + type[pos] + "\n");
                    }
                    else
                    {
                        int n;
                        bool isNum;
                        isNum = int.TryParse(line[j], out n);
                        if (isNum == true)
                        {
                            richTextBox3.AppendText(line[j] + " " + " -> Number" + "\n");
                        }
                        else
                        {
                            richTextBox3.AppendText(line[j] + " " + " -> Variable" + "\n");
                        }
                    }
                }

                richTextBox3.AppendText("--------------------------------" + "\n");
            }

            richTextBox3.AppendText("Lexical Stage is Done" + "\n");
            richTextBox3.AppendText("--------------------------------" + "\n");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < richTextBox2.Lines.Length; i++)
            {
                int cond = -1;
                string s = richTextBox2.Lines[i];
                string[] line = s.Split(' ');
                int count = 0;
                int loc = -1;
                string operation = "";

                for (int j = 0; j < line.Length; j++)
                {
                    int pos = Array.IndexOf(idt, line[j]);
                    if (j == 0)
                    {
                        if (pos <= 5 && pos > -1)
                        {
                            cond = 1;
                        }
                        else if (pos == -1)
                        {
                            cond = 2;
                        }
                        else if (pos == 25)
                        {
                            cond = 3;
                        }
                        else
                        {
                            richTextBox3.AppendText("wrong statement" + "\n");
                            richTextBox3.AppendText("---------------------------------" + "\n");
                            break;
                        }
                    }
                    if (cond == 1)
                    {
                        Console.WriteLine(operation);
                        int n;
                        float n1;
                        bool isNumeric = int.TryParse(line[j], out n);

                        if (isNumeric == false)
                        {
                            isNumeric = float.TryParse(line[j], out n1);
                        }

                        if (!line[line.Length - 1].Equals(";"))
                        {
                            richTextBox3.AppendText(" ; is missing at end of line \n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                            break;
                        }

                        if (j == line.Length - 1)
                        {
                            if (var[var.Count - 1].type.Equals("int"))
                            {
                                int x = Convert.ToInt32(new DataTable().Compute(operation, null));
                                var[var.Count - 1].value = x.ToString();
                            }
                            else if (var[var.Count - 1].type.Equals("float"))
                            {
                                float x = (float)Convert.ToDouble(new DataTable().Compute(operation, null));
                                var[var.Count - 1].value = x.ToString();
                            }
                            else if (var[var.Count - 1].type.Equals("double"))
                            {
                                double x = Convert.ToDouble(new DataTable().Compute(operation, null));
                                x = Math.Round(x, 2);
                                var[var.Count - 1].value = x.ToString();
                            }

                            richTextBox3.AppendText(" No Syntax or Semantic Error" + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                            richTextBox1.AppendText(var[var.Count - 1].variable + " = " + var[var.Count - 1].value + "\n");
                        }

                        if (pos == -1 && isNumeric == true && (var[var.Count - 1].type.Equals("int") || var[var.Count - 1].type.Equals("float") || var[var.Count - 1].type.Equals("double")))
                        {
                            if (count == 3)
                            {
                                operation += line[j];
                            }
                            else
                            {
                                richTextBox3.AppendText(line[j] + " position is wrong" + "\n");
                                richTextBox3.AppendText("--------------------------------" + "\n");
                                break;
                            }
                        }
                        else if (pos == -1)
                        {
                            if (count == 1)
                            {
                                bool flag = false;
                                for (int k = 0; k < var.Count; k++)
                                {
                                    if (line[j] == var[k].variable)
                                    {
                                        flag = true;
                                    }
                                }
                                if (flag == false)
                                {
                                    Var pnn = new Var();
                                    pnn.type = line[j - 1];
                                    pnn.variable = line[j];
                                    var.Add(pnn);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else if (count == 3)
                            {
                                if (var[var.Count - 1].type.Equals("char"))
                                {
                                    if (line[j].Length > 1)
                                    {
                                        richTextBox3.AppendText(" Char cannont take more than one value" + "\n");
                                        richTextBox3.AppendText("--------------------------------" + "\n");
                                    }
                                    else
                                    {
                                        var[var.Count - 1].value = line[j];
                                    }
                                }
                                else if (var[var.Count - 1].type.Equals("string"))
                                {
                                    var[var.Count - 1].value = line[j];
                                }
                                else
                                {
                                    bool flag = false;
                                    for (int k = 0; k < var.Count; k++)
                                    {
                                        if (line[j] == var[k].variable)
                                        {
                                            flag = true;
                                            operation += var[k].value;
                                        }
                                    }
                                    if (flag == false)
                                    {
                                        richTextBox3.AppendText(line[j] + " is not defined" + "\n");
                                        richTextBox3.AppendText("--------------------------------" + "\n");
                                        break;
                                    }
                                }
                            }
                        }
                        else if (array1[count].Contains(code[pos]))
                        {
                            if (count == 4 && code[pos].Equals("b"))
                            {
                                count = 2;
                                operation += idt[pos];
                            }
                        }
                        else
                        {
                            richTextBox3.AppendText(array1[count] + " should be placed instead of " + line[j] + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                        }
                    }
                    if (cond == 2)
                    {
                        Console.WriteLine(operation);
                        int n;

                        bool isNumeric = int.TryParse(line[j], out n);

                        if (!line[line.Length - 1].Equals(";"))
                        {
                            richTextBox3.AppendText(" ; is missing at end of line" + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                            break;
                        }
                        if (j == line.Length - 1)
                        {
                            if (var[loc].type.Equals("int"))
                            {
                                int x = Convert.ToInt32(new DataTable().Compute(operation, null));
                                var[loc].value = x.ToString();
                            }
                            else if (var[loc].type.Equals("float"))
                            {
                                float x = (float)Convert.ToDouble(new DataTable().Compute(operation, null));
                                var[loc].value = x.ToString();
                            }
                            else if (var[loc].type.Equals("double"))
                            {
                                double x = Convert.ToDouble(new DataTable().Compute(operation, null));
                                x = Math.Round(x, 2);
                                var[loc].value = x.ToString();
                            }
                            richTextBox3.AppendText(" No Syntax or Semantic Error" + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                            richTextBox1.AppendText(var[loc].variable + " = " + var[loc].value + "\n");
                        }

                        if (pos == -1 && isNumeric == true && (var[loc].type.Equals("int") || var[loc].type.Equals("float") || var[loc].type.Equals("double")))
                        {
                            if (count == 2)
                            {
                                operation += line[j];
                            }
                            else
                            {
                                richTextBox3.AppendText(line[j] + " position is wrong" + "\n");
                                richTextBox3.AppendText("--------------------------------" + "\n");
                                break;
                            }
                        }
                        else if (pos == -1)
                        {
                            if (count == 0)
                            {
                                bool flag = false;
                                for (int k = 0; k < var.Count; k++)
                                {
                                    if (line[j] == var[k].variable)
                                    {
                                        flag = true;
                                        loc = k;
                                    }
                                }
                                if (flag == false)
                                {
                                    richTextBox3.AppendText(line[j] + " is not defined" + "\n");
                                    richTextBox3.AppendText("--------------------------------" + "\n");
                                    break;
                                }
                            }
                            else if (count == 2)
                            {
                                if (var[loc].type.Equals("char"))
                                {
                                    if (line[j].Length > 1)
                                    {
                                        richTextBox3.AppendText("Char cannont take more than one value" + "\n");
                                        richTextBox3.AppendText("--------------------------------" + "\n");
                                    }
                                    else
                                    {
                                        var[loc].value = line[j];
                                    }
                                }
                                else if (var[loc].type.Equals("string"))
                                {
                                    var[loc].value = line[j];
                                }
                                else
                                {
                                    bool flag = false;
                                    for (int k = 0; k < var.Count; k++)
                                    {
                                        if (line[j] == var[k].variable)
                                        {
                                            flag = true;
                                            operation += var[k].value;
                                        }
                                    }
                                    if (flag == false)
                                    {
                                        richTextBox3.AppendText(line[j] + " is not defined " + "\n");
                                        richTextBox3.AppendText("--------------------------------" + "\n");
                                        break;
                                    }
                                }
                            }
                        }
                        else if (array2[count].Contains(code[pos]))
                        {
                            if (count == 3 && code[pos].Equals("b"))
                            {
                                count = 1;
                                operation += idt[pos];
                            }
                        }
                        else
                        {
                            richTextBox3.AppendText(array2[count] + "" + line[j] + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                        }
                    }

                    if (cond == 3)
                    {
                        int n;
                        bool isNumeric = int.TryParse(line[j], out n);
                        if (!line[line.Length - 1].Equals(")"))
                        {
                            richTextBox3.AppendText(" ) is missing at end of line" + "\n");
                            richTextBox3.AppendText("--------------------------------" + "\n");
                            break;

                        }
                        else if (pos == -1 && (count == 2 || count == 3 || count == 4 || count == 7))
                        {
                            int flag = 0;
                            if (count == 2 && isNumeric == false)
                            {
                                for (int h = 0; h < var.Count; h++)
                                {
                                    if (var[h].variable.Equals(line[j]))
                                    {
                                        flag = 1;
                                    }
                                }
                                if (flag == 1)
                                {
                                    richTextBox3.AppendText("Var not available" + "\n");
                                    richTextBox3.AppendText("--------------------------------" + "\n");
                                }
                            }
                            else if (count == 4)
                            {
                                if (isNumeric == false)
                                {
                                    for (int h = 0; h < var.Count; h++)
                                    {
                                        if (var[h].variable.Equals(line[j]))
                                        {
                                            flag = 1;
                                        }
                                    }
                                    if (flag == 1)
                                    {
                                        richTextBox3.AppendText("Var not available" + "\n");
                                        richTextBox3.AppendText("--------------------------------" + "\n");
                                    }
                                }
                            }
                            else if (count == 5 && (line[j].Equals("&&") || line[j].Equals("||")))
                            {
                                count = 1;
                            }
                        }
                    }
                    count++;
                }
            }
            richTextBox3.AppendText("Syntax and Semantic Stage is Done" + "\n");
            richTextBox3.AppendText("--------------------------------" + "\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            button4_Click(sender, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            Application.Restart();
        }
    }
}
