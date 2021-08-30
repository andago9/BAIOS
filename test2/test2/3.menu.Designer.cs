
namespace test2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Nodo3");
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("Nodo4");
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("Nodo5");
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Nodo6");
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("Nodo7");
            System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("Nodo8");
            System.Windows.Forms.TreeNode treeNode61 = new System.Windows.Forms.TreeNode("Nodo9");
            System.Windows.Forms.TreeNode treeNode62 = new System.Windows.Forms.TreeNode("Nodo10");
            System.Windows.Forms.TreeNode treeNode63 = new System.Windows.Forms.TreeNode("Portables", new System.Windows.Forms.TreeNode[] {
            treeNode55,
            treeNode56,
            treeNode57,
            treeNode58,
            treeNode59,
            treeNode60,
            treeNode61,
            treeNode62});
            System.Windows.Forms.TreeNode treeNode64 = new System.Windows.Forms.TreeNode("Nodo11");
            System.Windows.Forms.TreeNode treeNode65 = new System.Windows.Forms.TreeNode("Nodo12");
            System.Windows.Forms.TreeNode treeNode66 = new System.Windows.Forms.TreeNode("Nodo13");
            System.Windows.Forms.TreeNode treeNode67 = new System.Windows.Forms.TreeNode("Nodo14");
            System.Windows.Forms.TreeNode treeNode68 = new System.Windows.Forms.TreeNode("Nodo15");
            System.Windows.Forms.TreeNode treeNode69 = new System.Windows.Forms.TreeNode("Ejecutables", new System.Windows.Forms.TreeNode[] {
            treeNode64,
            treeNode65,
            treeNode66,
            treeNode67,
            treeNode68});
            System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("Nodo16");
            System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("Nodo17");
            System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("Online", new System.Windows.Forms.TreeNode[] {
            treeNode70,
            treeNode71});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.SteelBlue;
            this.treeView1.Location = new System.Drawing.Point(264, 66);
            this.treeView1.Name = "treeView1";
            treeNode55.Name = "Nodo3";
            treeNode55.Text = "Nodo3";
            treeNode56.Name = "Nodo4";
            treeNode56.Text = "Nodo4";
            treeNode57.Name = "Nodo5";
            treeNode57.Text = "Nodo5";
            treeNode58.Name = "Nodo6";
            treeNode58.Text = "Nodo6";
            treeNode59.Name = "Nodo7";
            treeNode59.Text = "Nodo7";
            treeNode60.Name = "Nodo8";
            treeNode60.Text = "Nodo8";
            treeNode61.Name = "Nodo9";
            treeNode61.Text = "Nodo9";
            treeNode62.Name = "Nodo10";
            treeNode62.Text = "Nodo10";
            treeNode63.Name = "Portables";
            treeNode63.Text = "Portables";
            treeNode64.Name = "Nodo11";
            treeNode64.Text = "Nodo11";
            treeNode65.Name = "Nodo12";
            treeNode65.Text = "Nodo12";
            treeNode66.Name = "Nodo13";
            treeNode66.Text = "Nodo13";
            treeNode67.Name = "Nodo14";
            treeNode67.Text = "Nodo14";
            treeNode68.Name = "Nodo15";
            treeNode68.Text = "Nodo15";
            treeNode69.Name = "Ejecutables";
            treeNode69.Text = "Ejecutables";
            treeNode70.Name = "Nodo16";
            treeNode70.Text = "Nodo16";
            treeNode71.Name = "Nodo17";
            treeNode71.Text = "Nodo17";
            treeNode72.Name = "OnLine";
            treeNode72.Text = "Online";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode63,
            treeNode69,
            treeNode72});
            this.treeView1.Size = new System.Drawing.Size(275, 215);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "MENU";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::test2.Properties.Resources.Blinter;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(647, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 122);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::test2.Properties.Resources.txt;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(668, 297);
            this.button1.MaximumSize = new System.Drawing.Size(69, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 72);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::test2.Properties.Resources.zip;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(71, 297);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 72);
            this.button4.TabIndex = 10;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::test2.Properties.Resources.html;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(71, 200);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 72);
            this.button3.TabIndex = 9;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::test2.Properties.Resources.exe;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(71, 101);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 74);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BAIOS MENU";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}