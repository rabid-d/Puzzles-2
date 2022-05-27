
namespace Puzzle2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.piecesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.solveButton = new System.Windows.Forms.Button();
            this.solvedDataGridView = new System.Windows.Forms.DataGridView();
            this.nextButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sizeTextBox = new System.Windows.Forms.TextBox();
            this.sizeOkButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solvedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(403, 403);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(12, 457);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(89, 23);
            this.colorButton.TabIndex = 1;
            this.colorButton.Text = "Select color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // colorPanel
            // 
            this.colorPanel.BackColor = System.Drawing.Color.Black;
            this.colorPanel.Location = new System.Drawing.Point(12, 421);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(89, 30);
            this.colorPanel.TabIndex = 2;
            // 
            // piecesRichTextBox
            // 
            this.piecesRichTextBox.Location = new System.Drawing.Point(516, 424);
            this.piecesRichTextBox.Name = "piecesRichTextBox";
            this.piecesRichTextBox.Size = new System.Drawing.Size(243, 229);
            this.piecesRichTextBox.TabIndex = 4;
            this.piecesRichTextBox.Text = "";
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(340, 421);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(75, 46);
            this.solveButton.TabIndex = 5;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // solvedDataGridView
            // 
            this.solvedDataGridView.AllowUserToAddRows = false;
            this.solvedDataGridView.AllowUserToDeleteRows = false;
            this.solvedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.solvedDataGridView.Location = new System.Drawing.Point(421, 12);
            this.solvedDataGridView.Name = "solvedDataGridView";
            this.solvedDataGridView.ReadOnly = true;
            this.solvedDataGridView.RowTemplate.Height = 25;
            this.solvedDataGridView.Size = new System.Drawing.Size(403, 403);
            this.solvedDataGridView.TabIndex = 6;
            this.solvedDataGridView.SelectionChanged += new System.EventHandler(this.solvedDataGridView_SelectionChanged);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(421, 421);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 424);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Size:";
            // 
            // sizeTextBox
            // 
            this.sizeTextBox.Location = new System.Drawing.Point(204, 421);
            this.sizeTextBox.Name = "sizeTextBox";
            this.sizeTextBox.Size = new System.Drawing.Size(43, 23);
            this.sizeTextBox.TabIndex = 12;
            this.sizeTextBox.Text = "8";
            // 
            // sizeOkButton
            // 
            this.sizeOkButton.Location = new System.Drawing.Point(253, 421);
            this.sizeOkButton.Name = "sizeOkButton";
            this.sizeOkButton.Size = new System.Drawing.Size(43, 23);
            this.sizeOkButton.TabIndex = 13;
            this.sizeOkButton.Text = "Ok";
            this.sizeOkButton.UseVisualStyleBackColor = true;
            this.sizeOkButton.Click += new System.EventHandler(this.sizeOkButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(107, 457);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 17;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 665);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sizeOkButton);
            this.Controls.Add(this.sizeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.solvedDataGridView);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.piecesRichTextBox);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solvedDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.RichTextBox piecesRichTextBox;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.DataGridView solvedDataGridView;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sizeTextBox;
        private System.Windows.Forms.Button sizeOkButton;
        private System.Windows.Forms.Button clearButton;
    }
}

