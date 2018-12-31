using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BOTWModdingHelper {
    public partial class CEMURulesGenerator : UserControl {
        public CEMURulesGenerator() {
            InitializeComponent();
        }
        string template = @"[Definition]
titleIds = 00050000101C9300,00050000101C9400,00050000101C9500
name = Mods
path = ""{0}/Mods/{1}""
description = {2}
version = 3";
        private void modText_TextChanged(object sender, EventArgs e) {
            previewTextBox.Text = string.Format(template, gameNameTextBox.Text, modNameTextBox.Text, descriptiionTextBox.Text);
        }

        private void CEMURulesGenerator_Load(object sender, EventArgs e) {
            previewTextBox.Text = string.Format(template, gameNameTextBox.Text, modNameTextBox.Text, descriptiionTextBox.Text);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveDialog.ShowDialog() == DialogResult.OK) {
                SaveRules(saveDialog.FileName);
            }
        }

        private void SaveRules(string path) {
            using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(fileStream)) {
                    writer.WriteLine(previewTextBox.Text);
                }
            }
        }
    }
}
