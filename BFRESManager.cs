using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOTWModdingHelper {
    public partial class BFRESManager : UserControl {
        public BFRESManager() {
            InitializeComponent();
            executablePath = AppDomain.CurrentDomain.BaseDirectory;
        }
        List<MemoryStream> originalTexData = new List<MemoryStream>();
        List<MemoryStream> originalModelData = new List<MemoryStream>();
        private MemoryStream originalPackData = new MemoryStream();
        private OpenFileDialog openDialogue;
        private string baseProjectPath = "";
        private List<string> tempTexturePaths = new List<string>();
        private List<string> tempModelPaths = new List<string>();
        private string tempPackPath;
        private string executablePath;

        public string BaseProjectPath {
            get {
                return baseProjectPath;
            }

            set {
                baseProjectPath = value;
            }
        }
        public bool NintendoSwitchMode {
            get {
                return nintendoSwitchCheckbox.Checked;
            }
            set {
                nintendoSwitchCheckbox.Checked = value;
                if (value) {
                    injectModelCSVToolStripMenuItem.Enabled = false;
                    mipMapsToolStripMenuItem.Enabled = false;
                    injectBNTXToolStripMenuItem.Enabled = true;
                } else {
                    injectModelCSVToolStripMenuItem.Enabled = true;
                    mipMapsToolStripMenuItem.Enabled = true;
                    injectBNTXToolStripMenuItem.Enabled = false;
                }
                RefreshLists();
            }
        }

        private void browseButton_Click(object sender, EventArgs e) {
            RefreshLists();
            openDialogue = new OpenFileDialog();
            openDialogue.Multiselect = true;
            openDialogue.Filter = ".sbfres files (*.sbfres)|*.sbfres";
            if (openDialogue.ShowDialog() == DialogResult.OK) {
                tempTexturePaths = new List<string>();
                tempModelPaths = new List<string>();
                List<string> modelDataPaths = new List<string>();
                List<string> texDataPaths = new List<string>();
                string packDataPath = null;
                foreach (string name in openDialogue.FileNames) {
                    if (name.Contains(".Tex")) {
                        texDataPaths.Add(name);
                    } else if (name.Contains(".pack")) {
                        packDataPath = name;
                    } else {
                        modelDataPaths.Add(name);
                    }
                }
                foreach (string path in texDataPaths) {
                    string newPath = "";
                    bool addedPath = false;
                    if (!string.IsNullOrEmpty(path)) {
                        MemoryStream backupStream = new MemoryStream();
                        originalTexData.Add(backupStream);
                        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                            fileStream.Position = 0;
                            fileStream.CopyTo(backupStream);
                            newPath = Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\" + Path.GetFileName(path));
                            if (!tempTexturePaths.Contains(newPath)) {
                                tempTexturePaths.Add(newPath);
                                addedPath = true;
                            }
                        }
                        if (addedPath) {
                            using (FileStream fileStream = new FileStream(newPath, FileMode.Create, FileAccess.Write)) {
                                backupStream.Position = 0;
                                backupStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
                foreach (string path in modelDataPaths) {
                    string newPath = "";
                    bool addedPath = false;
                    if (!string.IsNullOrEmpty(path)) {
                        MemoryStream backupStream = new MemoryStream();
                        originalModelData.Add(backupStream);
                        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                            fileStream.Position = 0;
                            fileStream.CopyTo(backupStream);
                            newPath = Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\" + Path.GetFileName(path));
                            if (!tempModelPaths.Contains(newPath)) {
                                tempModelPaths.Add(newPath);
                                addedPath = true;
                            }
                        }
                        if (addedPath) {
                            using (FileStream fileStream = new FileStream(newPath, FileMode.Create, FileAccess.Write)) {
                                backupStream.Position = 0;
                                backupStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
            RefreshLists();
        }

        private void unpackButton_Click(object sender, EventArgs e) {
            List<string> tempFiles = new List<string>();
            string[] files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"));
            tempFiles.AddRange(files);
            //tempFiles.AddRange(tempFiles);
            if (MessageBox.Show("Warning: Any .bfres files left in the working folder may get overwritten", "BOTW Texture Helper", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
                //try {
                foreach (string tempFile in tempFiles) {
                    if (tempFile.Contains(".sbfres")) {
                        if (!string.IsNullOrEmpty(tempFile)) {
                            Process.Start(Path.Combine(executablePath, "yaz0dec.exe"), @"""" + tempFile + @"""");
                        }
                    }
                }
                int count = 0;
                unpackButton.Enabled = false;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (count < tempFiles.Count) {
                    if (stopwatch.ElapsedMilliseconds >= 10000) {
                        break;
                    }
                    files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"));
                    foreach (string file in files) {
                        if (file.Contains(".sbfres 0.rarc")) {
                            try {
                                string newPath = Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\") + Path.GetFileNameWithoutExtension(file).Replace(".sbfres 0", null) + ".bfres";
                                if (File.Exists(newPath)) {
                                    try {
                                        File.Delete(newPath);
                                    } catch {

                                    }
                                }
                                File.Move(file, newPath);
                                File.Delete(file);
                                tempModelPaths.Remove(file);
                                tempTexturePaths.Remove(file);
                                count++;
                            } catch {

                            }
                        }
                    }
                }
                unpackButton.Enabled = true;
                if (tempTexturePaths.Count > 0) {
                    bfresButton.Enabled = true;
                }
            }
            RefreshLists();
        }
        void RefreshLists() {
            originalSBFRESList.Items.Clear();
            extractedBFRESList.Items.Clear();
            packedSBFRESList.Items.Clear();
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"));
            }
            string[] files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"), "*.sbfres");
            foreach (string file in files) {
                originalSBFRESList.Items.Add(Path.GetFileName(file));
            }
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"));
            }
            files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"), "*.bfres");
            int count = 0;
            foreach (string file in files) {
                extractedBFRESList.Items.Add(count++ + "", Path.GetFileName(file), file);
            }
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"));
            }
            files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"), "*.sbfres");
            foreach (string file in files) {
                packedSBFRESList.Items.Add(Path.GetFileName(file));
            }
        }
        private void bfresButton_Click(object sender, EventArgs e) {
            try {
                Process.Start(Path.Combine(executablePath, "bfres_tool.exe"), @"""" + baseProjectPath.Replace("sbfres", "bfres") + @"""");
            } catch {
                MessageBox.Show("Error bfres_tool.exe is missing, make sure its in the root folder alongside this executable.");
            }
        }

        private void packButton_Click(object sender, EventArgs e) {
            List<string> tempFiles = new List<string>();
            packButton.Enabled = false;
            if (MessageBox.Show("Warning: Any .sbfres files left in the working folder will be deleted or overwritten", "BOTW Texture Helper", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
                string[] files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES"), "*.bfres");
                tempFiles.AddRange(files);
                try {
                    foreach (string path in tempFiles) {
                        if (!string.IsNullOrEmpty(path)) {
                            Process.Start(Path.Combine(executablePath, "yaz0fastx64.exe"), @"""" + path + @"""");
                        }
                    }
                } catch {
                    MessageBox.Show("Error yaz0fastx64.exe is missing, make sure its in the root folder alongside this executable.");
                }
                int fileCount = 1;
                int count = 0;
                foreach (string path in Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES"))) {
                    File.Delete(path);
                }
                while (count < tempFiles.Count) {
                    files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES"));
                    foreach (string file in files) {
                        if (file.Contains(".bfres.szs")) {
                            try {
                                File.Move(file, Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\") + Path.GetFileNameWithoutExtension(file).Replace(".bfres", null) + ".sbfres");
                                File.Delete(file);
                                count++;
                            } catch {

                            }
                        }
                    }
                }
                //Process.Start("explorer.exe", Path.Combine(executablePath, @"TempEdit\"));
                unpackButton.Enabled = true;
                if (tempModelPaths.Count <= 0) {
                    // mipmapsButton.Enabled = false;
                }
                if (tempTexturePaths.Count <= 0) {
                    // bfresButton.Enabled = false;
                }
                packButton.Enabled = true;
            }
            RefreshLists();
        }

        private void mipmapsButton_Click(object sender, EventArgs e) {
        }

        private void sarcUnpackButton_Click(object sender, EventArgs e) {
            MessageBox.Show("Select package to unpack");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FolderSelectDialog saveFileDialog = new FolderSelectDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                MessageBox.Show("Select destination");
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData).Replace(@"\Roaming", null), @"Local\Programs\Python\Python37\Scripts\sarctool.exe");
                    try {
                        Process.Start("sarc", "extract " + @"""" + openFileDialog.FileName + @"""" + @" """ + saveFileDialog.SelectedPath + @"""");
                    } catch {
                        MessageBox.Show("Sarc tool can't be found, make sure its installed and properly configured.");
                    }
                }
            }
        }

        private void sarcPackButton_Click(object sender, EventArgs e) {
            MessageBox.Show("Select folder to re-pack");
            FolderSelectDialog openFileDialog = new FolderSelectDialog();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                MessageBox.Show("Set file name and location");
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData).Replace(@"\Roaming", null), @"Local\Programs\Python\Python37\Scripts\sarctool.exe");
                    string directoryName = openFileDialog.SelectedPath;
                    string finalPath = @"""" + directoryName + @"""" + " " + @"""" + saveFileDialog.FileName + @"""";
                    try {
                        Process.Start(path, (!nintendoSwitchCheckbox.Checked ? "create -b " : "create ") + finalPath);
                    } catch {
                        MessageBox.Show("Sarc tool can't be found, make sure its installed and properly configured.");
                    }
                }
            }

        }

        private void workingFolderButton_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", Path.Combine(baseProjectPath, @"TempEdit\"));
        }

        private void legalStuffButton_Click(object sender, EventArgs e) {
            MessageBox.Show(@"MIT License: 
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void mipMapsToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                foreach (ListViewItem item in extractedBFRESList.SelectedItems) {
                    if (!item.ImageKey.Contains("Tex")) {
                        Process.Start(Path.Combine(executablePath, "ZBOTWAutoMips.py"), @"""" + item.ImageKey + @"""");
                    }
                }
            } catch {
                MessageBox.Show("Error mipmap script is missing, make sure its in the root folder alongside this executable.");
            }
        }

        private void injectBNTXToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void injectModelCSVToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (ListViewItem selectedItem in extractedBFRESList.SelectedItems) {
                MessageBox.Show("Select file to inject into " + Path.GetFileName(selectedItem.ImageKey));
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = ".csv files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"LauchBFRESInjector(NoGUI).py");
                    string directoryName = selectedItem.ImageKey;
                    string finalPath = @"""" + directoryName + @"""" + " " + @"""" + openFileDialog.FileName + @"""";
                    if (File.Exists(path)) {
                        Process.Start(path, finalPath);
                    } else {
                        MessageBox.Show("BFRESInjector couldn't be found.");
                        break;
                    }
                }
            }
        }

        private void clearFilesButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("This will delete all files in the work area, are you sure?", "BoTW Helper", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                if (MessageBox.Show("Files will now be deleted", "BoTW Helper", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    DeleteFiles();
                    RefreshLists();
                }
            }
        }

        private void DeleteFiles() {
            originalSBFRESList.Items.Clear();
            extractedBFRESList.Items.Clear();
            packedSBFRESList.Items.Clear();
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"));
            }
            string[] files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\OriginalSBFRES\"));
            foreach (string file in files) {
                File.Delete(file);
            }
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"));
            }
            files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\UnpackedBFRES\"));
            int count = 0;
            foreach (string file in files) {
                File.Delete(file);
            }
            if (!Directory.Exists(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"))) {
                Directory.CreateDirectory(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"));
            }
            files = Directory.GetFiles(Path.Combine(baseProjectPath, @"TempEdit\RepackagedSBFRES\"));
            foreach (string file in files) {
                File.Delete(file);
            }
        }
    }
}