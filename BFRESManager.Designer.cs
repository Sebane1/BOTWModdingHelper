namespace BOTWModdingHelper {
    partial class BFRESManager {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("sdadas");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("asdsaas");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("asdsa");
            this.unpackButton = new System.Windows.Forms.Button();
            this.bfresButton = new System.Windows.Forms.Button();
            this.packButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.sarcUnpackButton = new System.Windows.Forms.Button();
            this.sarcPackButton = new System.Windows.Forms.Button();
            this.workingFolderButton = new System.Windows.Forms.Button();
            this.nintendoSwitchCheckbox = new System.Windows.Forms.CheckBox();
            this.legalStuffButton = new System.Windows.Forms.Button();
            this.originalSBFRESList = new System.Windows.Forms.ListView();
            this.extractedBFRESList = new System.Windows.Forms.ListView();
            this.bfresEditingStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mipMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectBNTXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectModelCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packedSBFRESList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clearFilesButton = new System.Windows.Forms.Button();
            this.bfresEditingStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // unpackButton
            // 
            this.unpackButton.Location = new System.Drawing.Point(105, 12);
            this.unpackButton.Name = "unpackButton";
            this.unpackButton.Size = new System.Drawing.Size(87, 24);
            this.unpackButton.TabIndex = 0;
            this.unpackButton.Text = "Unpack";
            this.unpackButton.UseVisualStyleBackColor = true;
            this.unpackButton.Click += new System.EventHandler(this.unpackButton_Click);
            // 
            // bfresButton
            // 
            this.bfresButton.Location = new System.Drawing.Point(198, 12);
            this.bfresButton.Name = "bfresButton";
            this.bfresButton.Size = new System.Drawing.Size(87, 24);
            this.bfresButton.TabIndex = 1;
            this.bfresButton.Text = "BFRES Tool";
            this.bfresButton.UseVisualStyleBackColor = true;
            this.bfresButton.Click += new System.EventHandler(this.bfresButton_Click);
            // 
            // packButton
            // 
            this.packButton.Location = new System.Drawing.Point(291, 12);
            this.packButton.Name = "packButton";
            this.packButton.Size = new System.Drawing.Size(180, 24);
            this.packButton.TabIndex = 2;
            this.packButton.Text = "Pack";
            this.packButton.UseVisualStyleBackColor = true;
            this.packButton.Click += new System.EventHandler(this.packButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(12, 12);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(87, 24);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // sarcUnpackButton
            // 
            this.sarcUnpackButton.Location = new System.Drawing.Point(12, 42);
            this.sarcUnpackButton.Name = "sarcUnpackButton";
            this.sarcUnpackButton.Size = new System.Drawing.Size(87, 24);
            this.sarcUnpackButton.TabIndex = 6;
            this.sarcUnpackButton.Text = "Sarc Unpack";
            this.sarcUnpackButton.UseVisualStyleBackColor = true;
            this.sarcUnpackButton.Click += new System.EventHandler(this.sarcUnpackButton_Click);
            // 
            // sarcPackButton
            // 
            this.sarcPackButton.Location = new System.Drawing.Point(105, 42);
            this.sarcPackButton.Name = "sarcPackButton";
            this.sarcPackButton.Size = new System.Drawing.Size(87, 24);
            this.sarcPackButton.TabIndex = 7;
            this.sarcPackButton.Text = "Sarc Pack";
            this.sarcPackButton.UseVisualStyleBackColor = true;
            this.sarcPackButton.Click += new System.EventHandler(this.sarcPackButton_Click);
            // 
            // workingFolderButton
            // 
            this.workingFolderButton.Location = new System.Drawing.Point(291, 42);
            this.workingFolderButton.Name = "workingFolderButton";
            this.workingFolderButton.Size = new System.Drawing.Size(180, 24);
            this.workingFolderButton.TabIndex = 8;
            this.workingFolderButton.Text = "Open Working Folder";
            this.workingFolderButton.UseVisualStyleBackColor = true;
            this.workingFolderButton.Click += new System.EventHandler(this.workingFolderButton_Click);
            // 
            // nintendoSwitchCheckbox
            // 
            this.nintendoSwitchCheckbox.AutoSize = true;
            this.nintendoSwitchCheckbox.Enabled = false;
            this.nintendoSwitchCheckbox.Location = new System.Drawing.Point(12, 72);
            this.nintendoSwitchCheckbox.Name = "nintendoSwitchCheckbox";
            this.nintendoSwitchCheckbox.Size = new System.Drawing.Size(134, 17);
            this.nintendoSwitchCheckbox.TabIndex = 9;
            this.nintendoSwitchCheckbox.Text = "Nintendo Switch Mode";
            this.nintendoSwitchCheckbox.UseVisualStyleBackColor = true;
            // 
            // legalStuffButton
            // 
            this.legalStuffButton.Location = new System.Drawing.Point(396, 72);
            this.legalStuffButton.Name = "legalStuffButton";
            this.legalStuffButton.Size = new System.Drawing.Size(75, 23);
            this.legalStuffButton.TabIndex = 10;
            this.legalStuffButton.Text = "Legal Stuff";
            this.legalStuffButton.UseVisualStyleBackColor = true;
            this.legalStuffButton.Click += new System.EventHandler(this.legalStuffButton_Click);
            // 
            // originalSBFRESList
            // 
            this.originalSBFRESList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.originalSBFRESList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.originalSBFRESList.LabelWrap = false;
            this.originalSBFRESList.Location = new System.Drawing.Point(21, 116);
            this.originalSBFRESList.Name = "originalSBFRESList";
            this.originalSBFRESList.Size = new System.Drawing.Size(141, 365);
            this.originalSBFRESList.TabIndex = 11;
            this.originalSBFRESList.UseCompatibleStateImageBehavior = false;
            this.originalSBFRESList.View = System.Windows.Forms.View.List;
            // 
            // extractedBFRESList
            // 
            this.extractedBFRESList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.extractedBFRESList.ContextMenuStrip = this.bfresEditingStrip;
            this.extractedBFRESList.Location = new System.Drawing.Point(168, 116);
            this.extractedBFRESList.Name = "extractedBFRESList";
            this.extractedBFRESList.Size = new System.Drawing.Size(142, 365);
            this.extractedBFRESList.TabIndex = 12;
            this.extractedBFRESList.UseCompatibleStateImageBehavior = false;
            this.extractedBFRESList.View = System.Windows.Forms.View.List;
            // 
            // bfresEditingStrip
            // 
            this.bfresEditingStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mipMapsToolStripMenuItem,
            this.injectBNTXToolStripMenuItem,
            this.injectModelCSVToolStripMenuItem});
            this.bfresEditingStrip.Name = "bfresEditingStrip";
            this.bfresEditingStrip.Size = new System.Drawing.Size(165, 70);
            // 
            // mipMapsToolStripMenuItem
            // 
            this.mipMapsToolStripMenuItem.Name = "mipMapsToolStripMenuItem";
            this.mipMapsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.mipMapsToolStripMenuItem.Text = "MipMaps";
            this.mipMapsToolStripMenuItem.Click += new System.EventHandler(this.mipMapsToolStripMenuItem_Click);
            // 
            // injectBNTXToolStripMenuItem
            // 
            this.injectBNTXToolStripMenuItem.Name = "injectBNTXToolStripMenuItem";
            this.injectBNTXToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.injectBNTXToolStripMenuItem.Text = "Inject BNTX";
            this.injectBNTXToolStripMenuItem.Click += new System.EventHandler(this.injectBNTXToolStripMenuItem_Click);
            // 
            // injectModelCSVToolStripMenuItem
            // 
            this.injectModelCSVToolStripMenuItem.Name = "injectModelCSVToolStripMenuItem";
            this.injectModelCSVToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.injectModelCSVToolStripMenuItem.Text = "Inject Model CSV";
            this.injectModelCSVToolStripMenuItem.Click += new System.EventHandler(this.injectModelCSVToolStripMenuItem_Click);
            // 
            // packedSBFRESList
            // 
            this.packedSBFRESList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.packedSBFRESList.Location = new System.Drawing.Point(316, 116);
            this.packedSBFRESList.Name = "packedSBFRESList";
            this.packedSBFRESList.Size = new System.Drawing.Size(144, 365);
            this.packedSBFRESList.TabIndex = 13;
            this.packedSBFRESList.UseCompatibleStateImageBehavior = false;
            this.packedSBFRESList.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Original SBFRES";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Extracted BFRES";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Repackaged SBFRES";
            // 
            // clearFilesButton
            // 
            this.clearFilesButton.Location = new System.Drawing.Point(198, 42);
            this.clearFilesButton.Name = "clearFilesButton";
            this.clearFilesButton.Size = new System.Drawing.Size(87, 24);
            this.clearFilesButton.TabIndex = 17;
            this.clearFilesButton.Text = "Clear Files";
            this.clearFilesButton.UseVisualStyleBackColor = true;
            this.clearFilesButton.Click += new System.EventHandler(this.clearFilesButton_Click);
            // 
            // BFRESManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clearFilesButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.packedSBFRESList);
            this.Controls.Add(this.extractedBFRESList);
            this.Controls.Add(this.originalSBFRESList);
            this.Controls.Add(this.legalStuffButton);
            this.Controls.Add(this.nintendoSwitchCheckbox);
            this.Controls.Add(this.workingFolderButton);
            this.Controls.Add(this.sarcPackButton);
            this.Controls.Add(this.sarcUnpackButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.packButton);
            this.Controls.Add(this.bfresButton);
            this.Controls.Add(this.unpackButton);
            this.Name = "BFRESManager";
            this.Size = new System.Drawing.Size(483, 491);
            this.bfresEditingStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button unpackButton;
        private System.Windows.Forms.Button bfresButton;
        private System.Windows.Forms.Button packButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button sarcUnpackButton;
        private System.Windows.Forms.Button sarcPackButton;
        private System.Windows.Forms.Button workingFolderButton;
        private System.Windows.Forms.CheckBox nintendoSwitchCheckbox;
        private System.Windows.Forms.Button legalStuffButton;
        private System.Windows.Forms.ListView originalSBFRESList;
        private System.Windows.Forms.ListView extractedBFRESList;
        private System.Windows.Forms.ListView packedSBFRESList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip bfresEditingStrip;
        private System.Windows.Forms.ToolStripMenuItem mipMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectBNTXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectModelCSVToolStripMenuItem;
        private System.Windows.Forms.Button clearFilesButton;
    }
}

