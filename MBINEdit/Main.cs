using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MBINEdit
{
    public partial class Main : Form
    {
        MBINFile file;
        object fileData;

        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "MBIN Files (*.mbin)|*.mbin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            file = new MBINFile(ofd.FileName);
            file.Load();
            fileData = file.GetData();

            if(fileData == null)
            {
                MessageBox.Show("Unsupported MBIN template \"" + file.Header.TemplateName + "\"!");
                return;
            }

            Text = "MBINEdit - " + ofd.FileName;
            tsmiSave.Enabled = true;

            PopulateEditorPanel();
        }

        private void PopulateEditorPanel()
        {
            var type = fileData.GetType();
            var fields = type.GetFields();

            pnlEditor.Controls.Clear();

            var lblTemplate = new Label();
            lblTemplate.Text = "Template: " + file.Header.GetXMLTemplateName();
            lblTemplate.Location = new Point(12, 12);
            lblTemplate.AutoSize = true;
            pnlEditor.Controls.Add(lblTemplate);
            
            int ypos = 0;

            // todo: change txtValue X position to accommodate large lblName.Text values

            foreach (var field in fields)
            {
                if (field.Name.StartsWith("Padding"))
                    continue;
                var fieldName = field.Name;
                var fieldType = field.FieldType.Name;

                var lblName = new Label();
                lblName.Text = fieldName;
                lblName.Location = new Point(24, 38 + ypos);
                lblName.AutoSize = true;
                lblName.Tag = field;
                pnlEditor.Controls.Add(lblName);

                switch(fieldType)
                {
                    case "String":
                    case "Single":
                        var txtValue = new TextBox();
                        txtValue.Location = new Point(230, 36 + ypos);
                        txtValue.Size = new Size(270, 22);
                        txtValue.Tag = field;
                        txtValue.Text = field.GetValue(fileData).ToString();
                        pnlEditor.Controls.Add(txtValue);
                        break;

                    case "Boolean":
                        var chkValue = new CheckBox();
                        chkValue.Location = new Point(230, 36 + ypos);
                        chkValue.Size = new Size(270, 22);
                        chkValue.Tag = field;
                        chkValue.Text = "True";
                        chkValue.Checked = (bool)field.GetValue(fileData);
                        pnlEditor.Controls.Add(chkValue);
                        break;

                    case "Int16":
                    case "Int32":
                        var valuesMethod = type.GetMethod(field.Name + "Values"); // if we have an "xxxValues()" method in the struct, use that to give the user a dropdown selection
                        if (valuesMethod != null)
                        {
                            string[] values = (string[])valuesMethod.Invoke(fileData, null);
                            var cmbValues = new ComboBox();
                            cmbValues.Location = new Point(230, 36 + ypos);
                            cmbValues.Size = new Size(270, 22);
                            cmbValues.Tag = field;
                            cmbValues.DropDownStyle = ComboBoxStyle.DropDownList;
                            foreach (var str in values)
                                cmbValues.Items.Add(str);
                            cmbValues.SelectedIndex = (int)field.GetValue(fileData);
                            pnlEditor.Controls.Add(cmbValues);
                        }
                        else
                        {
                            var intValue = new NumericUpDown();
                            intValue.Location = new Point(230, 36 + ypos);
                            intValue.Size = new Size(270, 22);
                            intValue.Tag = field;
                            intValue.Minimum = (fieldType == "Int16" ? Int16.MinValue : Int32.MinValue);
                            intValue.Maximum = (fieldType == "Int16" ? Int16.MaxValue : Int32.MaxValue);
                            var value = field.GetValue(fileData); ;
                            intValue.Value = (fieldType == "Int16" ? (short)value : (int)value);
                            pnlEditor.Controls.Add(intValue);
                        }
                        break;
                }

                ypos += 30;
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (file == null || fileData == null)
                return;

            foreach(var control in pnlEditor.Controls)
            {
                var controlType = control.GetType().Name;
                if (controlType == "Label")
                    continue;

                var field = (FieldInfo)((Control)control).Tag;
                var fieldName = field.Name;
                var fieldType = field.FieldType.Name;

                switch(fieldType)
                {
                    case "String":
                    case "Single":
                        var txtValue = (TextBox)control;
                        if (fieldType == "Single")
                            field.SetValue(fileData, float.Parse(txtValue.Text));
                        else
                            field.SetValue(fileData, txtValue.Text);

                        break;

                    case "Boolean":
                        var chkValue = (CheckBox)control;
                        field.SetValue(fileData, chkValue.Checked);

                        break;

                    case "Int16":
                    case "Int32":
                        if (controlType == "ComboBox")
                        {
                            var cmbValues = (ComboBox)control;
                            field.SetValue(fileData, cmbValues.SelectedIndex);
                            break;
                        }
                        
                        var intValue = (NumericUpDown)control;
                        if (fieldType == "Int16")
                            field.SetValue(fileData, (short)intValue.Value);
                        else
                            field.SetValue(fileData, (int)intValue.Value);

                        break;
                }
            }

            file.SetData(fileData);
            file.Save();
        }
    }
}
