using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOTWModdingHelper {
    public partial class ModdingManager : Form {
        public ModdingManager() {
            InitializeComponent();
        }
        List<string> workspacePaths = new List<string>();
        List<WorkspaceType> workspaces = new List<WorkspaceType>();
        List<GameConsole> workspaceConsoles = new List<GameConsole>();
        enum WorkspaceType {
            SBFRESManager
        }
        enum GameConsole {
            WiiU,
            Switch
        }
        private void wiiUToolStripMenuItem_Click(object sender, EventArgs e) {
            createNewWorkspace(WorkspaceType.SBFRESManager, GameConsole.WiiU);
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e) {
            createNewWorkspace(WorkspaceType.SBFRESManager, GameConsole.Switch);
        }
        private void createNewWorkspace(WorkspaceType workspaceType, GameConsole gameConsole) {
            switch (workspaceType) {
                case WorkspaceType.SBFRESManager:
                    MessageBox.Show("Pick a directory for the new workspace");
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                        createTab(folderBrowserDialog.SelectedPath, workspaceType, gameConsole); ;
                    }
                    break;
            }
        }

        private void createTab(string selectedPath, WorkspaceType workspaceType, GameConsole gameConsole) {
            TabPage tabPage = new TabPage();
            BFRESManager bfresManager = new BFRESManager();
            workspaceTabs.TabPages.Add(tabPage);
            bfresManager.Parent = tabPage;
            bfresManager.Size = tabPage.Size;
            bfresManager.BaseProjectPath = selectedPath;
            workspacePaths.Add(selectedPath);
            workspaces.Add(workspaceType);
            workspaceConsoles.Add(gameConsole);
            switch (gameConsole) {
                case GameConsole.WiiU:
                    tabPage.Text = "BFRES Manager (Wii U)";
                    bfresManager.NintendoSwitchMode = false;
                    break;
                case GameConsole.Switch:
                    tabPage.Text = "BFRES Manager (Switch)";
                    bfresManager.NintendoSwitchMode = true;
                    break;
            }
        }

        private void SaveConfig(string path) {
            using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
                using (BinaryWriter writer = new BinaryWriter(fileStream)) {
                    writer.Write(workspacePaths.Count);
                    for (int i = 0; i < workspacePaths.Count; i++) {
                        writer.Write(workspacePaths[i]);
                        writer.Write((int)workspaces[i]);
                        writer.Write((int)workspaceConsoles[i]);
                    }
                }
            }
        }

        private void LoadConfig(string path) {
            using (FileStream fileStream = new FileStream(path, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(fileStream)) {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++) {
                        createTab(reader.ReadString(), (WorkspaceType)reader.ReadInt32(), (GameConsole)reader.ReadInt32());
                    }
                }
            }
        }
        private void NewConfig() {
            workspacePaths.Clear();
            workspaces.Clear();
            workspaceConsoles.Clear();
            workspaceTabs.TabPages.Clear();
        }
        private void SetLastConfig(string path) {
            using (FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lastConfigPath.cfg"), FileMode.Create)) {
                using (BinaryWriter writer = new BinaryWriter(fileStream)) {
                    writer.Write(path);
                }
            }
        }
        private string GetLastConfig() {
            using (FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lastConfigPath.cfg"), FileMode.OpenOrCreate)) {
                using (BinaryReader reader = new BinaryReader(fileStream)) {
                    try {
                        return reader.ReadString();
                    } catch {
                        return "";
                    }
                }
            }
        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".modconfig files (*.modconfig)|*.modconfig";
            if (dialog.ShowDialog() == DialogResult.OK) {
                LoadConfig(dialog.FileName);
                SetLastConfig(dialog.FileName);
            }
        }

        private void saveConfigToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = ".modconfig files (*.modconfig)|*.modconfig";
            if (dialog.ShowDialog() == DialogResult.OK) {
                SaveConfig(dialog.FileName);
                SetLastConfig(dialog.FileName);
            }
        }

        private void clearConfigToolStripMenuItem_Click(object sender, EventArgs e) {
            NewConfig();
        }

        private void workspaceTabs_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void ModdingManager_Load(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(GetLastConfig())) {
                LoadConfig(GetLastConfig());
            }
        }
    }
}
