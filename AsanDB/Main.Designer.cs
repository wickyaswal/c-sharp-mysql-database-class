namespace AsanDB
{
    partial class Main
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
            this.dgv_persons = new System.Windows.Forms.DataGridView();
            this.cb_column = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_persons)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_persons
            // 
            this.dgv_persons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_persons.Location = new System.Drawing.Point(37, 63);
            this.dgv_persons.Name = "dgv_persons";
            this.dgv_persons.Size = new System.Drawing.Size(744, 143);
            this.dgv_persons.TabIndex = 1;
            // 
            // cb_column
            // 
            this.cb_column.FormattingEnabled = true;
            this.cb_column.Location = new System.Drawing.Point(37, 241);
            this.cb_column.Name = "cb_column";
            this.cb_column.Size = new System.Drawing.Size(302, 21);
            this.cb_column.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Persons";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 308);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_column);
            this.Controls.Add(this.dgv_persons);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "AsanDB";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_persons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_persons;
        private System.Windows.Forms.ComboBox cb_column;
        private System.Windows.Forms.Label label1;
    }
}

