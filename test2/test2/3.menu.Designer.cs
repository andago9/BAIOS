
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Nodo3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Nodo4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Nodo5");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nodo6");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Nodo7");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Nodo8");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Nodo9");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nodo10");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Portables", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Nodo11");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Nodo12");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Nodo13");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Nodo14");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Nodo15");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Ejecutables", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Nodo16");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Nodo17");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Online", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
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
            treeNode1.Name = "Nodo3";
            treeNode1.Text = "Nodo3";
            treeNode2.Name = "Nodo4";
            treeNode2.Text = "Nodo4";
            treeNode3.Name = "Nodo5";
            treeNode3.Text = "Nodo5";
            treeNode4.Name = "Nodo6";
            treeNode4.Text = "Nodo6";
            treeNode5.Name = "Nodo7";
            treeNode5.Text = "Nodo7";
            treeNode6.Name = "Nodo8";
            treeNode6.Text = "Nodo8";
            treeNode7.Name = "Nodo9";
            treeNode7.Text = "Nodo9";
            treeNode8.Name = "Nodo10";
            treeNode8.Text = "Nodo10";
            treeNode9.Name = "Portables";
            treeNode9.Text = "Portables";
            treeNode10.Name = "Nodo11";
            treeNode10.Text = "Nodo11";
            treeNode11.Name = "Nodo12";
            treeNode11.Text = "Nodo12";
            treeNode12.Name = "Nodo13";
            treeNode12.Text = "Nodo13";
            treeNode13.Name = "Nodo14";
            treeNode13.Text = "Nodo14";
            treeNode14.Name = "Nodo15";
            treeNode14.Text = "Nodo15";
            treeNode15.Name = "Ejecutables";
            treeNode15.Text = "Ejecutables";
            treeNode16.Name = "Nodo16";
            treeNode16.Text = "Nodo16";
            treeNode17.Name = "Nodo17";
            treeNode17.Text = "Nodo17";
            treeNode18.Name = "OnLine";
            treeNode18.Text = "Online";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode15,
            treeNode18});
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
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
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